using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityFramework
{
	public interface IContextManager
	{
		IContext RootContext { get; }
		T ChildOrParentOf<T>();
	}
	
	public class ContextManager : IContextManager
	{
		public ContextManager(IContext context)
		{
			RootContext = context;
		}
		
		public IContext RootContext { get; }
		
		public T ChildOrParentOf<T>()
		{
			if (RootContext is T root)
			{
				return root;
			}

			foreach (var child in RootContext.Children)
			{
				if (child is T value)
				{
					return value;
				}
			}

			throw new NullReferenceException();
		}
	}
}