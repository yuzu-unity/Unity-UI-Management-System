using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityPageManager;

namespace UnityFramework
{
    public static class ContextExtension
    {
	    public static T Of<T>(this IContext context)
	    {

		    var c = context;

		    while (c.Parent != null)
		    {

			    if (c.Parent is T result)
			    {
				    return result;
			    }

			    c = c.Parent;
		    }

		    throw new NullReferenceException();
	    }
    }
}