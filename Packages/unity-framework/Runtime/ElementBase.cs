using System;
using System.Collections;
using System.Collections.Generic;
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

		public virtual void Dispose()
		{
			foreach (var child in Children)
			{
				child.Dispose();
			}
			
			if (Parent != null)
			{
				if (Parent.Children.Contains(this))
				{
					Parent.Children.Remove(this);
				}
			}
		}

		protected virtual void OnDestroy()
		{
			Dispose();
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
			foreach (var child in Children)
			{
				child.Dispose();
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