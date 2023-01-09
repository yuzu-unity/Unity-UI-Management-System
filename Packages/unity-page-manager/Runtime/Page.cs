using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityPageManager
{
	public class Page : MonoBehaviour, IPage
	{
		/// <summary>
		/// Pageのフックは型で
		/// </summary>
		protected virtual void OnValidate()
		{
			gameObject.name = GetType().ToString();
		}

		public virtual async UniTask InitializeAsync(CancellationToken cancellationToken = default)
		{
			this.gameObject.SetActive(false);
		}

		public virtual async UniTask SuspendAsync(CancellationToken cancellationToken = default)
		{
			this.gameObject.SetActive(false);
		}

		public virtual async UniTask ResumeAsync(CancellationToken cancellationToken = default)
		{
			this.gameObject.SetActive(true);
		}
		
		public virtual void Dispose()
		{
		}
	}
}