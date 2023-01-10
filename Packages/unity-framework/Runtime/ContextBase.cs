using System.Collections.Generic;

namespace UnityFramework
{
	public interface IContext
	{
		IContext Parent { get; }
		
		List<IContext> Children { get; }
	}
}