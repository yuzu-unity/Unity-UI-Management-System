using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityFramework
{
    public abstract class MaterialAppWidget : WidgetBase
    {
        protected virtual IWidget Main => null;
        
        private IEnumerable<IWidget> ChildrenWidgets => Children.OfType<IWidget>();

        public override void Build(IContext context)
        {
            base.Build(context);
            Main.Build(this);
        }

        public override async UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            await base.InitializeAsync(cancellationToken);
            await ChildrenWidgets.Select(x=>x.InitializeAsync(cancellationToken));
        }
    }
}
