using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityFramework
{
	public interface IElement: IContext
	{
	}
	
	public abstract class ElementMonoBase: MonoBehaviour, IElement
	{
		private Transform __cachedTransform;

		protected Transform _cachedTransform
		{
			get
			{
				if (__cachedTransform == null)
				{
					__cachedTransform = transform;
				}
				return __cachedTransform;
			}
		}
		
		public IContext Parent { get; private set; }
		
		public List<IContext> Children { get; } = new List<IContext>();

		public virtual void Build(IContext context)
		{
			Children.Clear();
			
			Parent = context;
			Parent?.Children.Add(this);
		}

		protected virtual void DisposeImpl()
		{
			while (Children.Any())
			{
				Children.First()?.Dispose();
			}
			
			if (Parent != null)
			{
				if (Parent.Children.Contains(this))
				{
					Parent.Children.Remove(this);
				}
			}
		}
		
		public virtual void Dispose()
		{
			DisposeImpl();
			Destroy(this);
		}

		/// <summary>
		/// シーン移動時などDestroy時も同様に処理をしておく(ライフサイクル考慮)
		/// </summary>
		protected virtual void OnDestroy()
		{
			DisposeImpl();
		}
	}
	
	public abstract class ElementBase: IElement
	{
		public IContext Parent { get; private set; }
		public List<IContext> Children { get; } = new List<IContext>();

		
		public virtual void Build(IContext context)
		{
			Children.Clear();
			Parent = context;
			Parent?.Children.Add(this);
		}

		public virtual void Dispose()
		{
			while (Children.Any())
			{
				Children.First()?.Dispose();
			}
			
			if (Parent != null)
			{
				if (Parent.Children.Contains(this))
				{
					Parent.Children.Remove(this);
				}
			}
		}

	}
}