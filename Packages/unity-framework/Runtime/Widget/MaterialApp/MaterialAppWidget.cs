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
        
        public override void Build(IContext context)
        {
            base.Build(context);
            Main.Build(this);
        }
    }
}
