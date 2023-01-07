using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace UnityNavigator
{
	public class NavigatorController<TPageContext,TPushContext,TModalContext> 
		where TPageContext : PageContext<TPushContext,TModalContext>, new() 
		where TPushContext : Context, new() 
		where TModalContext : Context, new()
	{
		public TPageContext PageContext { get; } = new ();
	}
}