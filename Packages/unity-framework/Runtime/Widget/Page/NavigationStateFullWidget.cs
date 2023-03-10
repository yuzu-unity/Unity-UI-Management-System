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
    public interface INavigationStateFullWidget: IStateFullWidget
    {
        
        UniTask PopAsync(CancellationToken cancellationToken = default);

        UniTask RemoveAllAsync(CancellationToken cancellationToken);
        
        UniTask PushAsync(IPageProvider provider,Action<IPageWidget> setParameter = null, CancellationToken cancellationToken = default);

        UniTask ReplaceAsync(IPageProvider provider, Action<IPageWidget> setParameter = null,
            CancellationToken cancellationToken = default);

        UniTask ReplaceAllAsync(IPageProvider provider, Action<IPageWidget> setParameter = null,
            CancellationToken cancellationToken = default);
        
        string CurrentRoute { get; }

        UniTask PushNamedAsync(string route,Action<IPageWidget> setParameter = null, CancellationToken cancellationToken = default);

        UniTask ReplaceNamedAsync(string route,Action<IPageWidget> setParameter = null, CancellationToken cancellationToken = default);

        UniTask ReplaceAllNamedAsync(string route,Action<IPageWidget> setParameter = null,
            CancellationToken cancellationToken = default);
    }
    
    public class NavigationStateFullWidget : StateFullWidget ,INavigationStateFullWidget
    {
        private IPageManager _pageManager;

        protected string _currentRoute;
        public string CurrentRoute => _currentRoute;

        public override void InitState()
        {
            base.InitState();
            
            _pageManager = new PageManager(_cachedTransform);

            if (!string.IsNullOrEmpty(CurrentRoute))
            {
                ReplaceAllNamedAsync(CurrentRoute).Forget();
            }
        }
        
        private IPageProvider GetProviderFromRoute(string route)
        {
            if (!string.IsNullOrEmpty(CurrentRoute))
            {
                throw new Exception("????????????Route???????????????????????????RemoveAll???????????????????????????");
            }

            return GetProviderFromRouteImpl(route);
        }
        
        /// <summary>
        /// Route??????????????????overwrite route
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        protected virtual IPageProvider GetProviderFromRouteImpl(string route)
        {
            throw new NullReferenceException();
        }

        public UniTask PopAsync(CancellationToken cancellationToken = default)
        {
            return _pageManager.PopAsync(cancellationToken);
        }
        
        public UniTask RemoveAllAsync(CancellationToken cancellationToken)
        {
            return _pageManager.RemoveAllAsync(cancellationToken);
        }
        
        public UniTask PushAsync(IPageProvider provider, Action<IPageWidget> setParameter = null, CancellationToken cancellationToken = default)
        {
            return _pageManager.PushAsync(provider, (x)=>
            {
                setParameter?.Invoke((IPageWidget)x);
            }, cancellationToken);
        }

        public UniTask ReplaceAsync(IPageProvider provider, Action<IPageWidget> setParameter = null, CancellationToken cancellationToken = default)
        {
            return _pageManager.ReplaceAsync(provider,(x)=>
            {
                setParameter?.Invoke((IPageWidget)x);
            }, cancellationToken);
        }

        public UniTask ReplaceAllAsync(IPageProvider provider, Action<IPageWidget> setParameter = null,
            CancellationToken cancellationToken = default)
        {
            return _pageManager.ReplaceAllAsync(provider,(x)=>
            {
                setParameter?.Invoke((IPageWidget)x);
            }, cancellationToken);
        }

        public async UniTask PushNamedAsync(string route, Action<IPageWidget> setParameter = null, CancellationToken cancellationToken = default)
        {
            var provider = GetProviderFromRoute(route);
            await _pageManager.PushAsync(provider, (x)=>
            {
                setParameter?.Invoke((IPageWidget)x);
            }, cancellationToken);
            _currentRoute = route;
        }

        public async UniTask ReplaceNamedAsync(string route, Action<IPageWidget> setParameter = null, CancellationToken cancellationToken = default)
        {
            var provider = GetProviderFromRoute(route);
            await _pageManager.ReplaceAsync(provider,(x)=>
            {
                setParameter?.Invoke((IPageWidget)x);
            }, cancellationToken);
            _currentRoute = route;
        }

        public async UniTask ReplaceAllNamedAsync(string route, Action<IPageWidget> setParameter = null, CancellationToken cancellationToken = default)
        {
            var provider = GetProviderFromRoute(route);
            await _pageManager.ReplaceAllAsync(provider,(x)=>
            {
                setParameter?.Invoke((IPageWidget)x);
            }, cancellationToken);
        }

        protected override void DisposeImpl()
        {
            _pageManager?.Dispose();
            base.DisposeImpl();
        }
    }
}