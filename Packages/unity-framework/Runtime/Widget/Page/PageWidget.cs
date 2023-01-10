using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityPageManager;

namespace UnityFramework
{
    public interface IPageWidget : IWidget, IPage
    {
        
    }
    
    public class PageWidget : WidgetCanvasBase ,IPageWidget
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