using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityNavigator
{
	public class Navigator
	{
		public Navigator(IContext context)
		{
			RootContext = context;
		}
		public IContext RootContext { get; }
	}
}