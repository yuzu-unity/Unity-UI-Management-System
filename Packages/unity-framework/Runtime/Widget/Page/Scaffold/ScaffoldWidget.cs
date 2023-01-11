using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityPageManager;
using System.Linq;

namespace UnityFramework
{
    public abstract class ScaffoldWidget : NavigationStateFullWidget
    {
        protected virtual IWidget AppBar=> null;
        protected virtual IWidget Body => null;
        protected virtual IWidget BottomNavigationBar => null;
        protected virtual IWidget FloatingActionButton => null;
        
        private IEnumerable<IWidget> ChildrenWidgets => Children.OfType<IWidget>();


        protected override void BuildImpl()
        {
            AppBar?.Build(this);
            Body?.Build(this);
            BottomNavigationBar?.Build(this);
            FloatingActionButton?.Build(this);
        }

        public override async UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            await base.InitializeAsync(cancellationToken);
            await ChildrenWidgets.Select(x=>x.InitializeAsync(cancellationToken));
        }

        public override async UniTask ResumeAsync(CancellationToken cancellationToken = default)
        {
            await base.ResumeAsync(cancellationToken);
            await ChildrenWidgets.Select(x=>x.ResumeAsync(cancellationToken));
        }

        public override async UniTask SuspendAsync(CancellationToken cancellationToken = default)
        {
            await base.SuspendAsync(cancellationToken);
            await ChildrenWidgets.Select(x=>x.SuspendAsync(cancellationToken));
        }
    }
}