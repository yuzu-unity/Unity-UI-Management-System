using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityNavigator
{
	public interface IElement: IContext
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
		
		public T Of<T>() where T: IContext
		{
			if (Parent == null)
			{
				throw new Exception("Context Not Found");
			}

			if (Parent is T parent)
			{
				return parent;
			}

			return Parent.Of<T>();
		}
		
		public void Build(IContext context)
		{
			Parent = context;
		}

	}
	
	public abstract class ElementBase: IElement
	{
		public IContext Parent { get; private set; }
		
		public T Of<T>() where T: IContext
		{
			if (Parent == null)
			{
				throw new Exception("Context Not Found");
			}

			if (Parent is T parent)
			{
				return parent;
			}

			return Parent.Of<T>();
		}
		
		public void Build(IContext context)
		{
			Parent = context;
		}
	}
}