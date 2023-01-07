using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace UnityNavigator
{

	public abstract class Context
	{
		private readonly StringReactiveProperty _name = new StringReactiveProperty();
		public IReactiveProperty<string> Name => _name;

		private readonly List<string> _history = new List<string>();

		public async UniTask ReplaceAsync(string name = default)
		{
			_history.Clear();
			await PushAsync(name);
		}

		protected abstract UniTask _ReplaceAsync(string name = default);

		public async UniTask PushAsync(string name = default,bool isStack=true)
		{
			Name.Value = name;
			if (isStack)
			{
				_history.Add(name);
			}

			await _PushAsync(name);
		}

		protected abstract UniTask _PushAsync(string name);
		
		public async UniTask PopAsync()
		{
			if (_history.Count == 0)
			{
				throw new Exception();
			}
			_history.RemoveAt(_history.Count-1);
			_name.Value = _history.Count == 0 ? string.Empty : _history[^1];

			await _PopAsync();
		}

		protected abstract UniTask _PopAsync();

	}

	public abstract class PageContext<TPushContext,TModalContext> where TPushContext : Context, new() where TModalContext : Context, new()
	{
		private readonly StringReactiveProperty _name = new StringReactiveProperty();
		public IReactiveProperty<string> Name => _name;

		public TPushContext PushContext { get; } = new ();
		
		public TModalContext ModalContext{ get; } = new ();

		public async UniTask ShowAsync(string name,string pushName = default,string modalName=default)
		{
			_name.Value = name;

			await _ShowAsync();
			
			var task1 = PushContext.ReplaceAsync(pushName);
			var task2 = ModalContext.ReplaceAsync(pushName);
			await UniTask.WhenAll(task1,task2);
		}

		protected abstract UniTask _ShowAsync();

	}
}