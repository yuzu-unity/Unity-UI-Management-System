using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityFramework
{
    public abstract class MaterialAppWidget : NavigationStateFullWidget
    {
        protected virtual IWidget Main => null;
        
        private IEnumerable<IWidget> ChildrenWidgets => Children.OfType<IWidget>();


        protected override void BuildImpl()
        {
            Main.Build(this);
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
