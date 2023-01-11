using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityFramework
{
    public interface IWidget : IElement
    {
        bool AutoBuildInitialize { get; }
        
        UniTask InitializeAsync(CancellationToken cancellationToken);
    }

    /// <summary>
    /// UIはMono
    /// </summary>
    public abstract class WidgetBase : ElementMonoBase, IWidget
    {
        
        public override void Build(IContext context)
        {
            base.Build(context);
            
            if (AutoBuildInitialize)
            {
                InitializeAsync(this.GetCancellationTokenOnDestroy()).Forget();
            }
        }

        public virtual bool AutoBuildInitialize => true;

        public virtual UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            this.gameObject.SetActive(true);
            return new UniTask();
        }
        
    }
}