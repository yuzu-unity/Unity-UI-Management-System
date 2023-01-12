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
        

        public override void Build(IContext context)
        {
            base.Build(context);
            AppBar?.Build(this);
            Body?.Build(this);
            Bottom?.Build(this);
            FloatingActionButton?.Build(this);
        }
    }
}