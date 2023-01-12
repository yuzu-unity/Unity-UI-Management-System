using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityPageManager;

namespace UnityFramework
{
    public interface ITabStateFullWidget: IStateFullWidget
    {
        IReadOnlyReactiveProperty<int> State { get; }

        UniTask SetStateAsync(int state,Action<IPageWidget> setParameter = null, CancellationToken cancellationToken = default);
    }
    
    public abstract class TabStateFullWidget : StateFullWidget,ITabStateFullWidget
    {
        private readonly IntReactiveProperty _state = new IntReactiveProperty();
        public IReadOnlyReactiveProperty<int> State => _state;

        private IPageManager _pageManager;

        protected abstract IPageProvider[] GetProviders { get; }
        
        public override  UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            _pageManager = new PageManager(_cachedTransform);
            return new UniTask();
        }

        public override void InitState()
        {
            base.InitState();
            SetStateAsync(_state.Value).Forget();
        }

        public UniTask SetStateAsync(int state, Action<IPageWidget> setParameter = null, CancellationToken cancellationToken = default)
        {
            var provider = GetProviders[state];
            return _pageManager.PushAsync(provider, (x)=>
            {
                setParameter?.Invoke((IPageWidget)x);
            }, cancellationToken);
        }
    }
}