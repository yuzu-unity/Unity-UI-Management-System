using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework
{
	public class ContextManager
	{
		public ContextManager(IContext context)
		{
			RootContext = context;
		}
		public IContext RootContext { get; }
	}
}