using System;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace WellFired.Shared
{
	public class UnityEditorHelper : IUnityEditorHelper
	{
		public void AddUpdateListener(Action listener)
		{
			#if UNITY_EDITOR
			listeners += listener;
			
			// We always remove and the re add it, so we can ensure we never add it twice.
			EditorApplication.update -= Update;
			EditorApplication.update += Update;
			#endif
		}
		
		public void RemoveUpdateListener(Action listener)
		{
			#if UNITY_EDITOR
			listeners -= listener;
			#endif
		}
		
		private Action listeners = delegate { };
		
		private void Update()
		{
			listeners();
		}
		
		public bool IsPrefab(GameObject testObject)
		{
			#if UNITY_EDITOR
			return PrefabUtility.GetPrefabParent(testObject) == null && PrefabUtility.GetPrefabObject(testObject) != null;
			#else
			return false;
			#endif
		}
	}
}