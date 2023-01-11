using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace UnityFramework
{
    public interface IStateFullWidget : IWidget
    {
        void InitState();
    }

    public class StateFullWidget : WidgetBase , IStateFullWidget
    {
        public override async UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            await base.InitializeAsync(cancellationToken);
            InitState();
        }

        public virtual void InitState()
        {
            
        }
    }
}