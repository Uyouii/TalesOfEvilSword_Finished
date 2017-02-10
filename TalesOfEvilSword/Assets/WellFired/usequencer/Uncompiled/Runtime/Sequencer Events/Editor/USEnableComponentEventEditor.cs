using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace WellFired
{
	/// <summary>
	/// A custom event editor, this renders a custom display for the USEnableObjectEvent event in the uSequencer window. 
	/// </summary>
	[CustomUSEditor(typeof(USEnableComponentEvent))]
	[CustomEditor(typeof(USEnableComponentEvent))]
	[CanEditMultipleObjects()]
	public class USEnableComponentEventEditor : USEventBaseInspector
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			serializedObject.Update();

			var currentIndex = 0;
			var allComponents = new List<string>();
			foreach(var targetObject in serializedObject.targetObjects)
			{
				var targetGameObject = (targetObject as MonoBehaviour).gameObject;
				if(targetGameObject == null)
					continue;

				var enableEvent = targetGameObject.GetComponentInChildren<USEnableComponentEvent>();
				if(enableEvent == null)
					continue;

				var components = enableEvent.AffectedObject.GetComponents<Behaviour>();
				var newComponents = components.Select(component => component.GetType().Name).Cast<string>().ToList();
				currentIndex = newComponents.IndexOf(enableEvent.ComponentName);
				allComponents.AddRange(newComponents);
			}
			allComponents = allComponents.Distinct().ToList();

			var newIndex = EditorGUILayout.Popup("Component", currentIndex, allComponents.ToArray());

			foreach(var targetObject in serializedObject.targetObjects)
			{
				var targetGameObject = (targetObject as MonoBehaviour).gameObject;
				if(targetGameObject == null)
					continue;
				
				var enableComponentEvents = targetGameObject.GetComponentInChildren<USEnableComponentEvent>();
				if(enableComponentEvents == null)
					continue;
				
				if(newIndex < allComponents.Count)
					enableComponentEvents.ComponentName = allComponents[newIndex];
			}
			
			serializedObject.ApplyModifiedProperties();
		}
	}
}