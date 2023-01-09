using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityPageManager
{

	[RequireComponent(typeof(CanvasGroup))]
	public class PageCanvas : Page
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

		protected CancellationTokenSource _canvasGroupCancellationTokenSource;

		public override async UniTask InitializeAsync(CancellationToken cancellationToken = default)
		{
			_canvasGroup.alpha = 0;
			_canvasGroup.blocksRaycasts = false;
			this.gameObject.SetActive(false);
		}

		public override async UniTask SuspendAsync(CancellationToken cancellationToken = default)
		{
			_canvasGroupCancellationTokenSource?.Cancel();
			_canvasGroupCancellationTokenSource = null;
			if (_suspendHide)
			{
				_canvasGroupCancellationTokenSource =
					CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

				cancellationToken = _canvasGroupCancellationTokenSource.Token;


				_canvasGroup.alpha = 0;

				this.gameObject.SetActive(false);
			}

			_canvasGroup.blocksRaycasts = false;
		}

		public override async UniTask ResumeAsync(CancellationToken cancellationToken = default)
		{
			_canvasGroupCancellationTokenSource?.Cancel();
			_canvasGroupCancellationTokenSource =
				CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

			cancellationToken = _canvasGroupCancellationTokenSource.Token;

			_canvasGroup.alpha = 1;

			_canvasGroup.blocksRaycasts = true;
			this.gameObject.SetActive(true);

		}

		public override void Dispose()
		{
			_canvasGroupCancellationTokenSource?.Cancel();
			_canvasGroupCancellationTokenSource?.Dispose();
		}

	}
}
