using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityPageManager;

namespace UnityNavigator
{
   public interface IPageManager : IWidget
   {
      UniTask PushAsync<T>(IPageProvider<T> provider,Action<T> setParameter = null, CancellationToken cancellationToken = default) where T : IPage;
		
      UniTask PopAsync(CancellationToken cancellationToken = default);

      UniTask ReplaceAsync<T>(IPageProvider<T> provider,Action<T> setParameter = null, CancellationToken cancellationToken = default)
         where T : IPage;

      UniTask ReplaceAllAsync<T>(IPageProvider<T> provider,Action<T> setParameter = null, CancellationToken cancellationToken = default)
         where T : IPage;

      UniTask RemoveAllAsync(CancellationToken cancellationToken = default);
   }
}