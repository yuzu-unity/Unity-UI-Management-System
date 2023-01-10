using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityPageManager;

namespace UnityFramework
{
    public interface IStatefulWidgetWidget: IWidget ,IPageManager
    {
        
    }
    
    public class StatefulWidgetWidgetBase : WidgetBase ,IStatefulWidgetWidget
    {
        private IPageManager _pageManager;
        public override  UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            _pageManager = new WidgetPageManager(_cachedTransform);
            return new UniTask();
        }

        public override UniTask SuspendAsync(CancellationToken cancellationToken = default)
        {
            return new UniTask();
        }

        public override UniTask ResumeAsync(CancellationToken cancellationToken = default)
        {
            return new UniTask();
        }

        
        

        public UniTask PushAsync<T>(IPageProvider<T> provider, Action<T> setParameter = null, CancellationToken cancellationToken = default) where T : IPage
        {
            return _pageManager.PushAsync(provider, setParameter, cancellationToken);
        }

        public UniTask PopAsync(CancellationToken cancellationToken = default)
        {
            return _pageManager.PopAsync(cancellationToken);
        }

        public UniTask ReplaceAsync<T>(IPageProvider<T> provider, Action<T> setParameter = null,
            CancellationToken cancellationToken = default) where T : IPage
        {
            return _pageManager.ReplaceAsync(provider, setParameter, cancellationToken);
        }

        public UniTask ReplaceAllAsync<T>(IPageProvider<T> provider, Action<T> setParameter = null,
            CancellationToken cancellationToken = default) where T : IPage
        {
            return _pageManager.ReplaceAsync(provider, setParameter, cancellationToken);
        }

        public UniTask RemoveAllAsync(CancellationToken cancellationToken = default)
        {
            return _pageManager.RemoveAllAsync(cancellationToken);
        }
    }

    public sealed class WidgetPageManager : PageManager
    {
        public WidgetPageManager(Transform root)
        {
            _root = root;
        }
        protected override Transform _root { get; }
    }
}