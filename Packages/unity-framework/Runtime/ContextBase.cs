using System;
using System.Collections.Generic;

namespace UnityFramework
{
	public interface IContext:IDisposable
	{
		void Build(IContext context);

		IContext Parent { get; }
		
		List<IContext> Children { get; }
	}
}