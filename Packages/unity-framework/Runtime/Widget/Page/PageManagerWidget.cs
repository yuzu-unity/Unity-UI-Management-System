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
    public interface IPageManagerWidget: IWidget
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
    
    public class PageManagerWidget : WidgetBase ,IPageManagerWidget
    {
        private IPageManager _pageManager;

        private string _currentRoute;

        public string CurrentRoute => _currentRoute;

        
        protected virtual IPageProvider GetProviderFromRoute(string route)
        {
            return null;
        }

        public override  UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            _pageManager = new PageManager(_cachedTransform);
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
    }
}