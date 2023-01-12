using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace UnityPageManager
{

    public abstract class PageResourcesProviderBase<T> : IPageProvider where T : IPage
    {
        protected abstract string _rootPath { get; }

        public async UniTask<PageData> LoadPageAsync(Transform root,CancellationToken cancellationToken = default)
        {
            var path = _rootPath + typeof(T);
            var obj = (GameObject)Resources.Load(path);
            var instance =Object.Instantiate(obj,root);
            var page =instance.GetComponent<T>();
            var dispose= Disposable.Create(() =>
            {
                Object.Destroy(instance);
            });
            
            return new PageData(page,dispose);
        }
    }
    
    public abstract class PageScriptableObjectProviderBase<T,TScriptableObject> : IPageProvider where T : IPage where TScriptableObject:PageScriptableObject<TScriptableObject>
    {
        /// <summary>
        /// 基本はInstanceをStaticで取得
        /// </summary>
        protected abstract TScriptableObject _scriptableObject { get; }

        public async UniTask<PageData> LoadPageAsync(Transform root,CancellationToken cancellationToken = default)
        {
            var id = typeof(T).ToString();
            var obj = _scriptableObject.GetObject(id);
            var instance =Object.Instantiate(obj,root);
            var page =instance.GetComponent<T>();
            var dispose= Disposable.Create(() =>
            {
                Object.Destroy(instance);
            });
            
            return new PageData(page,dispose);
        }
    }
    
    public abstract class PageAddressableProviderBase<T> : IPageProvider where T : IPage
    {
        protected abstract string _rootPath { get; }
        
        
        public async UniTask<PageData> LoadPageAsync(Transform root,CancellationToken cancellationToken = default)
        {
            var path = _rootPath + typeof(T);
            var handle = Addressables.InstantiateAsync(path, root, false, false);
            try
            {
                var instance = await handle.ToUniTask(cancellationToken: cancellationToken);
                var page = instance.GetComponent<T>();
                var dispose= Disposable.Create(() =>
                {
                    if (handle.IsValid())
                    {
                        Addressables.Release(handle);
                    }
                });
                return new PageData(page,dispose);
            }
            catch (OperationCanceledException)
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
                throw;
            }
        }
    }

    public readonly struct Disposable : IDisposable
    {
        private readonly Action _action;

        private Disposable(Action action)
        {
            _action = action;
        }

        public static IDisposable Create(Action action)
        {
            return new Disposable(action);
        }
        
        public void Dispose()
        {
            _action?.Invoke();
        }
    }
    
}