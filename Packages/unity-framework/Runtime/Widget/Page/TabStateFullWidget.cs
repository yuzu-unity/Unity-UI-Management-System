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

        protected abstract IPageProvider GetProvider(int state);
        
        public override void InitState()
        {
            base.InitState();
            _pageManager = new PageManager(_cachedTransform);
            SetStateAsync(_state.Value,cancellationToken:this.GetCancellationTokenOnDestroy()).Forget();
        }

        public UniTask SetStateAsync(int state, Action<IPageWidget> setParameter = null, CancellationToken cancellationToken = default)
        {
            var provider = GetProvider(state);
            return _pageManager.ReplaceAllAsync(provider, (x)=>
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