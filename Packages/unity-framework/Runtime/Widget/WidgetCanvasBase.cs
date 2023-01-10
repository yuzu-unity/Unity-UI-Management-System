using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityFramework
{
	
	[RequireComponent(typeof(CanvasGroup))]
	public class WidgetCanvasBase : WidgetBase
    {
      		private CanvasGroup __canvasGroup;
      
      		protected CanvasGroup _canvasGroup
      		{
      			get
      			{
      				if (__canvasGroup == null)
      				{
      					__canvasGroup = GetComponent<CanvasGroup>();
      				}
      
      				return __canvasGroup;
      			}
      		}
      
      		[SerializeField]
      		protected bool _suspendHide = false;
      
      
      		public override UniTask InitializeAsync(CancellationToken cancellationToken = default)
      		{
      			_canvasGroup.alpha = 0;
      			_canvasGroup.blocksRaycasts = false;
      			this.gameObject.SetActive(false);
                return new UniTask();
            }
      
      		public override UniTask SuspendAsync(CancellationToken cancellationToken = default)
      		{
      			if (_suspendHide)
      			{
      				_canvasGroup.alpha = 0;
      
      				this.gameObject.SetActive(false);
      			}
      
      			_canvasGroup.blocksRaycasts = false;
                return new UniTask();

      		}
      
      		public override UniTask ResumeAsync(CancellationToken cancellationToken = default)
      		{
      			_canvasGroup.alpha = 1;
      
      			_canvasGroup.blocksRaycasts = true;
      			this.gameObject.SetActive(true);
                return new UniTask();

      		}
      
      		
    }
}