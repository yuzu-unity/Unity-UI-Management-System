using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityNavigator
{
    public interface IWidget : IElement
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
        public abstract UniTask InitializeAsync(CancellationToken cancellationToken);

        public abstract UniTask SuspendAsync(CancellationToken cancellationToken = default);

        public abstract UniTask ResumeAsync(CancellationToken cancellationToken = default);
    }
}