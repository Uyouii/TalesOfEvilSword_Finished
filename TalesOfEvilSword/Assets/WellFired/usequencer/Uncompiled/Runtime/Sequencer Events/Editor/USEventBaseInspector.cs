using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WellFired
{
	[CustomEditor(typeof(USEventBase), true)]
	[CanEditMultipleObjects()]
	public class USEventBaseInspector : Editor 
	{
		private static Dictionary<Type, List<Attribute>> typeMap = new Dictionary<Type, List<Attribute>>();

		public override void OnInspectorGUI()
		{
			// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
			serializedObject.Update();

			var serializedProperty = serializedObject.GetIterator();
			serializedProperty.Next(true);

			var objectType = serializedObject.targetObject.GetType();

			if(!typeMap.ContainsKey(objectType))
			{
				typeMap[objectType] = new List<Attribute>();

				var hideDurationAttribute = objectType.GetCustomAttributes(true).Where(attr => attr is USequencerEventHideDurationAttribute).Cast<USequencerEventHideDurationAttribute>().FirstOrDefault();

				typeMap[objectType].Add(hideDurationAttribute);
			}

			var attributes = typeMap[objectType];

			bool hideDuration = attributes.Any(attribute => attribute is USequencerEventHideDurationAttribute);

			using(new WellFired.Shared.GUIBeginVertical())
			{
				while(serializedProperty.NextVisible(true))
				{
					var skipProperty = false;

					if(hideDuration && serializedProperty.name == "duration")
						skipProperty = true;

					if(skipProperty)
						continue;

					EditorGUILayout.PropertyField(serializedProperty);
				}
			}

			// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
			serializedObject.ApplyModifiedProperties ();
		}
	}
}