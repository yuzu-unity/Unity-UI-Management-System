using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Linq;

namespace UnityFramework
{
    public abstract class ScaffoldWidget : NavigationStateFullWidget
    {
        protected virtual IWidget AppBar=> null;
        protected virtual IWidget Body => null;
        protected virtual IWidget Bottom => null;
        protected virtual IWidget FloatingActionButton => null;
        
        private IEnumerable<IWidget> ChildrenWidgets => Children.OfType<IWidget>();

        public override void Build(IContext context)
        {
            base.Build(context);
            AppBar?.Build(this);
            Body?.Build(this);
            Bottom?.Build(this);
            FloatingActionButton?.Build(this);
        }

        public override async UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            await base.InitializeAsync(cancellationToken);
            await ChildrenWidgets.Select(x=>x.InitializeAsync(cancellationToken));
        }
    }
}