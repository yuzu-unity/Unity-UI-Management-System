using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityPageManager
{
    public abstract class PageScriptableObject<T> : ScriptableObject where T :PageScriptableObject<T>
    {
	    private static T _instance;

	    public static T Instace
	    {
		    get
		    {
			    if (_instance == null)
			    {
				    _instance = Resources.Load<T>(typeof(T).ToString());
			    }
			    return _instance;
		    }
	    }
	    [SerializeField]
	    private List<GameObject> _gameObjects;

	    [SerializeField,Header("自動生成 名前辞書に使用")]
	    private List<string> _gameObjectNames;
	    
	    
	    public GameObject GetObject(string id)
	    {
		    for (int i = 0; i < _gameObjectNames.Count; i++)
		    {
			    if (_gameObjectNames[i] == id)
			    {
				    return _gameObjects[i];
			    }
		    }

		    throw new Exception($"NotFound GameObject id:{id}");
	    }

#if UNITY_EDITOR
	    private void OnValidate()
	    {
		    _gameObjectNames = _gameObjects
			    .Select(x =>
			    {
				    if (x == null)
				    {
					    return string.Empty;
				    }

				    return x.name;
			    }).ToList();
	    }
	    
	    private void Save()
	    {
		    EditorUtility.SetDirty(this);

		    AssetDatabase.SaveAssets();
	    }
#endif
	    
    }
}