using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityPageManager;

namespace UnityNavigator
{
    public interface IPageManagerWidget: IWidget
    {
        IPageManager PageManager { get; }
    }
    
    public class PageManagerWidgetBase : WidgetBase ,IPageManagerWidget
    {
        private IPageManager _pageManager;
        public IPageManager PageManager => _pageManager;
        

        public override  UniTask InitializeAsync(CancellationToken cancellationToken)
        {
            _pageManager = new WidgetPageManager(_cachedTransform);
            return new UniTask();
        }
        

        public override UniTask SuspendAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public override UniTask ResumeAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
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