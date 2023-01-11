using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityPageManager;

namespace UnityFramework
{
    public interface IPageWidget : IWidget, IPage
    {
      
    }
    
    [RequireComponent(typeof(CanvasGroup))]
    public class PageWidget : WidgetBase ,IPageWidget
    {
        /// <summary>
        /// Pageのフックは型で
        /// </summary>
        protected virtual void OnValidate()
        {
            gameObject.name = GetType().ToString();
        }

        public override bool AutoBuildInitialize => false;

        public override UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            this.gameObject.SetActive(false);
            return new UniTask();
        }
      
        public UniTask SuspendAsync(CancellationToken cancellationToken = default)
        {
            this.gameObject.SetActive(false);

            return new UniTask();

        }
      
        public UniTask ResumeAsync(CancellationToken cancellationToken = default)
        {
            this.gameObject.SetActive(true);
            return new UniTask();
        }
    }
}