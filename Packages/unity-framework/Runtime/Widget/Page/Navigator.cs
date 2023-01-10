using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityPageManager;

namespace UnityFramework
{
    public static class Navigator 
    {
	    public static IStatefulWidgetWidget Of(IContext context)
	    {

		    var c = context;

		    while (c.Parent != null)
		    {

			    if (c.Parent is IStatefulWidgetWidget result)
			    {
				    return result;
			    }

			    c = c.Parent;
		    }

		    return null;
	    }
    }
}