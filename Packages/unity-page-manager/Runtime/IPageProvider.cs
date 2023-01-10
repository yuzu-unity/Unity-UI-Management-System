using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityPageManager
{
    public interface IPage: IDisposable
    {
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        UniTask InitializeAsync(CancellationToken cancellationToken = default);
        
        //非表示処理
        UniTask SuspendAsync(CancellationToken cancellationToken = default);
        
        //表示処理
        UniTask ResumeAsync(CancellationToken cancellationToken = default);
    }

    public readonly struct PageData: IDisposable
    {
        public PageData(IPage page, IDisposable disposable)
        {
            Page = page;
            _disposable = disposable;
        }

        public readonly IPage Page;
        private readonly IDisposable _disposable;
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
    
    public interface IPageProvider 
    {
        UniTask<PageData> LoadPageAsync(Transform root,CancellationToken cancellationToken = default);
        
    }
}