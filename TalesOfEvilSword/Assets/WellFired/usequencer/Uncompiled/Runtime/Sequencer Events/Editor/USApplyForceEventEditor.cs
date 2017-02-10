using UnityEditor;
using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event editor, this renders a custom display for the USApplyForceEvent event in the uSequencer window. 
	/// </summary>
	[CustomUSEditor(typeof(USApplyForceEvent))]
	[CustomEditor(typeof(USApplyForceEvent))]
	public class USApplyForceEventEditor : USEventBaseEditor
	{
		private float HandleLength = 1.2f;
		private float HandleSize = 0.2f;
		
		public override Rect RenderEvent(Rect myArea)
		{
			USApplyForceEvent forceEvent = TargetEvent as USApplyForceEvent;
			
			DrawDefaultBox(myArea);
			
			using(new WellFired.Shared.GUIBeginArea(myArea))
			{
				GUILayout.Label(GetReadableEventName(), DefaultLabel);
				if(forceEvent)
				{
					GUILayout.Label(forceEvent.type.ToString(), DefaultLabel);
					GUILayout.Label("Strength : " + forceEvent.strength, DefaultLabel);
				}
			}
	
			return myArea;
		}
		
		void OnSceneGUI()
		{
			USApplyForceEvent forceEvent = target as USApplyForceEvent;
	
			if (!forceEvent)
				Debug.LogWarning("Trying to render an event as a USApplyForceEvent, but it is a : " + forceEvent.GetType().ToString());
			
			if(forceEvent.AffectedObject)
				forceEvent.transform.position = forceEvent.AffectedObject.transform.position;
	
			USUndoManager.BeginChangeCheck();
			
			Vector3 vPosition 	= forceEvent.transform.position;
			
	        float width		 	= HandleUtility.GetHandleSize(vPosition) * HandleLength;
			Vector3 vEnd	 	= vPosition + (forceEvent.direction * width);
			
	        width 				= HandleUtility.GetHandleSize(vEnd) * HandleSize;
	        vEnd 				= Handles.FreeMoveHandle(vEnd, Quaternion.identity, width, Vector3.zero, Handles.CubeCap);
			
			Vector3 vDifference = vEnd - vPosition;
			vDifference.Normalize();
	
			// Undo this
			if(USUndoManager.EndChangeCheck())
			{
				USUndoManager.PropertyChange(forceEvent, "Change Force Event Direction");
				forceEvent.direction = vDifference;
			}
	
			Handles.color = Color.red;
			Handles.DrawLine(vPosition, vEnd);
	        
			if (GUI.changed)
	        	EditorUtility.SetDirty(forceEvent);
		}
	}
}