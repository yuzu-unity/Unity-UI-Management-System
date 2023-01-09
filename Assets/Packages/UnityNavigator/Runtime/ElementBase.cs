using System.Collections;
using System.Collections.Generic;
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
		public IContext Parent { get; private set; }
		
		public IContext Of<T>()
		{
			if (Parent == null)
			{
				return null;
			}

			if (Parent is T)
			{
				return Parent;
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
		
		public IContext Of<T>()
		{
			if (Parent == null)
			{
				return null;
			}

			if (Parent is T)
			{
				return Parent;
			}

			return Parent.Of<T>();
		}
		
		public void Build(IContext context)
		{
			Parent = context;
		}
	}
}