using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Codice.Utils;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace UnityNavigator
{
	public interface IContext
	{
		IContext Parent { get; }

		IContext Of<T>();

	
	}

}