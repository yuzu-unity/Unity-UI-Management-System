using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityPageManager
{

	public interface IPageManager
	{
		UniTask PushAsync(IPageProvider provider, Action<IPage> setParameter = null,
			CancellationToken cancellationToken = default);
		
		UniTask PopAsync(CancellationToken cancellationToken = default);

		UniTask ReplaceAsync(IPageProvider provider, Action<IPage> setParameter = null,
			CancellationToken cancellationToken = default);

		UniTask ReplaceAllAsync(IPageProvider provider, Action<IPage> setParameter = null,
			CancellationToken cancellationToken = default);

		UniTask RemoveAllAsync(CancellationToken cancellationToken = default);
	}
	

	public sealed class PageManager : IPageManager , IDisposable
	{
		public PageManager(Transform root)
		{
			_root = root;
		}
		
		private Transform _root;
		
		private readonly List<PageData> _pageList = new();

		/// <summary>
		/// Push
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="setParameter"></param>Inject処理やパラメータ入力
		/// <param name="cancellationToken"></param>
		/// <typeparam name="T"></typeparam>
		public async UniTask PushAsync(IPageProvider provider,Action<IPage> setParameter = null,
			CancellationToken cancellationToken = default)
		{
			var lastPage = _pageList.LastOrDefault();
			if (lastPage.Page != null)
			{
				await lastPage.Page.SuspendAsync(cancellationToken);
			}

			var newPage = await provider.LoadPageAsync(_root, cancellationToken);
			setParameter?.Invoke(newPage.Page);
			await newPage.Page.InitializeAsync(cancellationToken);
			await newPage.Page.ResumeAsync(cancellationToken);

			_pageList.Add(newPage);
		}


		/// <summary>
		/// Pop
		/// </summary>
		/// <param name="cancellationToken"></param>
		public async UniTask PopAsync(CancellationToken cancellationToken = default)
		{
			var count = _pageList.Count;
			if (count <= 0)
			{
				return;
			}

			var lastPage = _pageList[^1];
			if (lastPage.Page != null)
			{
				await lastPage.Page.SuspendAsync(cancellationToken);
				lastPage.Dispose();
			}

			if (_pageList.Count > 1)
			{
				await _pageList[^2].Page.ResumeAsync(cancellationToken);
			}

			_pageList.Remove(lastPage);
		}

		/// <summary>
		/// Replace
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="setParameter"></param>Inject処理やパラメータ入力
		/// <param name="cancellationToken"></param>
		/// <typeparam name="T"></typeparam>
		public async UniTask ReplaceAsync(IPageProvider provider,Action<IPage> setParameter = null,
			CancellationToken cancellationToken = default)
		{
			var lastPage = _pageList.LastOrDefault();

			if (lastPage.Page != null)
			{
				await lastPage.Page.SuspendAsync(cancellationToken);
			}

			var newPage = await provider.LoadPageAsync(_root,cancellationToken);
			setParameter?.Invoke(newPage.Page);
			await newPage.Page.InitializeAsync(cancellationToken);
			await newPage.Page.ResumeAsync(cancellationToken);

			if (lastPage.Page != null)
			{
				lastPage.Dispose();
				_pageList.Remove(lastPage);
			}

			_pageList.Add(newPage);

		}

		/// <summary>
		/// ReplaceAll
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="setParameter"></param>Inject処理やパラメータ入力
		/// <param name="cancellationToken"></param>
		/// <typeparam name="T"></typeparam>
		public async UniTask ReplaceAllAsync(IPageProvider provider,Action<IPage> setParameter = null,
			CancellationToken cancellationToken = default)
		{
			var lastPage = _pageList.LastOrDefault();

			if (lastPage.Page != null)
			{
				await lastPage.Page.SuspendAsync(cancellationToken);
			}

			var newPage = await provider.LoadPageAsync(_root,cancellationToken);
			setParameter?.Invoke(newPage.Page);
			await newPage.Page.InitializeAsync(cancellationToken);
			await newPage.Page.ResumeAsync(cancellationToken);


			foreach (var page in _pageList)
			{
				if (page.Page == newPage.Page)
				{
					continue;
				}
				page.Dispose();
			}

			_pageList.Clear();
			_pageList.Add(newPage);
		}

		/// <summary>
		/// RemoveAll
		/// </summary>
		/// <param name="cancellationToken"></param>
		public async UniTask RemoveAllAsync(CancellationToken cancellationToken = default)
		{
			var lastPage = _pageList.LastOrDefault();

			if (lastPage.Page != null)
			{
				await lastPage.Page.SuspendAsync(cancellationToken);
			}

			foreach (var page in _pageList)
			{
				page.Dispose();
			}

			_pageList.Clear();
		}

		public void Dispose()
		{
			foreach (var page in _pageList)
			{
				page.Dispose();
			}
		}
	}
}