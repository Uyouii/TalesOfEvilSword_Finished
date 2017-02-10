using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will toggle a mesh renderer. 
	/// </summary>
	[USequencerFriendlyName("Toggle Mesh Renderer")]
	[USequencerEvent("Render/Toggle Mesh Renderer")]
	[USequencerEventHideDuration()]
	public class USMeshRenderDisable : USEventBase 
	{	
		/// <summary>
		/// Enable or Disable flag.
		/// </summary>
		public bool enable = false;
		private bool previousEnable;
		
		public override void FireEvent()
		{	
			if(!AffectedObject)
				return;
			
			MeshRenderer meshRenderer = AffectedObject.GetComponent<MeshRenderer>();
			if(!meshRenderer)
			{
				Debug.LogWarning("You didn't add a Mesh Renderer to the Affected Object", AffectedObject);
				return;
			}
			
			previousEnable = meshRenderer.enabled;
			meshRenderer.enabled = enable;
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			
		}
		
		public override void EndEvent()
		{
			UndoEvent();
		}
		
		public override void StopEvent()
		{
			UndoEvent();
		}
		
		public override void UndoEvent()
		{
			if(!AffectedObject)
				return;
			
			MeshRenderer meshRenderer = AffectedObject.GetComponent<MeshRenderer>();
			if(!meshRenderer)
			{
				Debug.LogWarning("You didn't add a Mesh Renderer to the Affected Object", AffectedObject);
				return;
			}
			
			meshRenderer.enabled = previousEnable;
		}
	}
}