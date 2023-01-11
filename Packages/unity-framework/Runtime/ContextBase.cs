using System.Collections.Generic;

namespace UnityFramework
{
	public interface IContext
	{
		void Build(IContext context);

		IContext Parent { get; }
		
		List<IContext> Children { get; }
	}
}