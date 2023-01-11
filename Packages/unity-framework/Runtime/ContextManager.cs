using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework
{
	public interface IContextManager
	{
		IContext RootContext { get; }
	}
	
	public class ContextManager : IContextManager
	{
		public ContextManager(IContext context)
		{
			RootContext = context;
		}
		public IContext RootContext { get; }
	}
}