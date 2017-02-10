using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Butterfly))]
public class ButterflyInspector : Editor {

	public override void OnInspectorGUI(){
		
		Butterfly bf = (Butterfly)target;
		
		bf.zoneSize = EditorGUILayout.Vector3Field("Zone size",bf.zoneSize);

		serializedObject.Update();
		EditorGUIUtility.LookLikeInspector();
		SerializedProperty layers = serializedObject.FindProperty("butterflyObjects");
		EditorGUILayout.PropertyField( layers,true);
		serializedObject.ApplyModifiedProperties();
		EditorGUIUtility.LookLikeControls();
		
		bf.butterflyCount = EditorGUILayout.IntField("Flies count",bf.butterflyCount);
		
		bf.maxSpeed = EditorGUILayout.FloatField("Max speed",bf.maxSpeed);
		bf.arrivalRadius = EditorGUILayout.FloatField("Arrival radius",bf.arrivalRadius);
		
		if(GUI.changed){
			EditorUtility.SetDirty(bf);
		}
	}
	
	void OnSceneGUI (){
		
		Butterfly bf = (Butterfly)target;
		
		Bounds zone = new Bounds(bf.transform.position, bf.zoneSize);
		
		Color faceColor = new Color(0,0.5f,0,0.02f);
		Color edgeColor = new Color(0,1,0,0.1f);
		
		Vector3[] verts = new Vector3[4];
		
		verts[0] = new Vector3(zone.min.x, zone.min.y, zone.min.z);
		verts[1] =new  Vector3(zone.min.x, zone.min.y, zone.max.z);
		verts[2] =new  Vector3(zone.max.x, zone.min.y, zone.max.z);
		verts[3] =new  Vector3(zone.max.x, zone.min.y, zone.min.z);			
		Handles.DrawSolidRectangleWithOutline( verts,faceColor , edgeColor);
			
		// top
		verts[0] = new Vector3(zone.min.x, zone.max.y, zone.min.z);
		verts[1] =new  Vector3(zone.min.x, zone.max.y, zone.max.z);
		verts[2] =new  Vector3(zone.max.x, zone.max.y, zone.max.z);
		verts[3] =new  Vector3(zone.max.x, zone.max.y, zone.min.z);			
		Handles.DrawSolidRectangleWithOutline( verts,faceColor , edgeColor);	
		
		// left
		verts[0] = new Vector3(zone.min.x, zone.min.y, zone.min.z);
		verts[1] =new  Vector3(zone.min.x, zone.min.y, zone.max.z);
		verts[2] =new  Vector3(zone.min.x, zone.max.y, zone.max.z);
		verts[3] =new  Vector3(zone.min.x, zone.max.y, zone.min.z);			
		Handles.DrawSolidRectangleWithOutline( verts,faceColor , edgeColor);
		
		// right
		verts[0] = new Vector3(zone.max.x, zone.min.y, zone.min.z);
		verts[1] =new  Vector3(zone.max.x, zone.min.y, zone.max.z);
		verts[2] =new  Vector3(zone.max.x, zone.max.y, zone.max.z);
		verts[3] =new  Vector3(zone.max.x, zone.max.y, zone.min.z);			
		Handles.DrawSolidRectangleWithOutline( verts,faceColor , edgeColor);
		
		// front
		verts[0] = new Vector3(zone.min.x, zone.min.y, zone.min.z);
		verts[1] =new  Vector3(zone.min.x, zone.max.y, zone.min.z);
		verts[2] =new  Vector3(zone.max.x, zone.max.y, zone.min.z);
		verts[3] =new  Vector3(zone.max.x, zone.min.y, zone.min.z);			
		Handles.DrawSolidRectangleWithOutline( verts,faceColor , edgeColor);		
		
		// back
		verts[0] = new Vector3(zone.min.x, zone.min.y, zone.max.z);
		verts[1] =new  Vector3(zone.min.x, zone.max.y, zone.max.z);
		verts[2] =new  Vector3(zone.max.x, zone.max.y, zone.max.z);
		verts[3] =new  Vector3(zone.max.x, zone.min.y, zone.max.z);			
		Handles.DrawSolidRectangleWithOutline( verts,faceColor , edgeColor);
	}
}
