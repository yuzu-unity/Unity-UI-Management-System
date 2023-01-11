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
        UniTask InitializeAsync(CancellationToken cancellationToken);
    }

    /// <summary>
    /// UIはMono
    /// </summary>
    public abstract class WidgetBase : ElementMonoBase, IWidget
    {
        public virtual UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            this.gameObject.SetActive(true);
            return new UniTask();
        }
        
    }
}