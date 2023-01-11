using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityFramework
{
	public interface IElement: IContext,IDisposable
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
			Parent?.Children.Remove(this);
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
			Parent?.Children.Remove(this);
		}

	}
}