using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityNavigator
{
    public interface IWidget : IElement, IDisposable
    {
        UniTask InitializeAsync(CancellationToken cancellationToken);

        UniTask SuspendAsync(CancellationToken cancellationToken = default);

        UniTask ResumeAsync(CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// UIはMono
    /// </summary>
    public abstract class WidgetBase : ElementMonoBase, IWidget
    {
        public virtual UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            this.gameObject.SetActive(false);
            return new UniTask();
        }

        public virtual UniTask SuspendAsync(CancellationToken cancellationToken = default)
        {
            this.gameObject.SetActive(false);
            return new UniTask();
        }

        public virtual UniTask ResumeAsync(CancellationToken cancellationToken = default)
        {
            this.gameObject.SetActive(true);
            return new UniTask();
        }
		
        public virtual void Dispose()
        {
        }
    }
}