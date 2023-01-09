using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityPageManager;

namespace UnityNavigator
{
    public class PageWidget : WidgetCanvasBase ,IPage
    {
        /// <summary>
        /// Pageのフックは型で
        /// </summary>
        protected virtual void OnValidate()
        {
            gameObject.name = GetType().ToString();
        }
    }
}