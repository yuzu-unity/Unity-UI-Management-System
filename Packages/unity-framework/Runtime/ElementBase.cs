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
		void Build(IContext context);
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

		public void Build(IContext context)
		{
			Parent = context;
			Parent?.Children.Add(this);
			BuildImpl();
		}
		
		protected virtual void BuildImpl()
		{
			
		}

		public void Dispose()
		{
			Parent?.Children.Remove(this);
			DisposeImpl();
		}

		protected virtual void DisposeImpl()
		{
			
		}
	}
	
	public abstract class ElementBase: IElement
	{
		public IContext Parent { get; private set; }
		public List<IContext> Children { get; } = new List<IContext>();

		
		public void Build(IContext context)
		{
			Parent = context;
			Parent?.Children.Add(this);
		}

		public void Dispose()
		{
			Parent?.Children.Remove(this);
			DisposeImpl();
		}

		protected virtual void DisposeImpl()
		{
			
		}
	}
}