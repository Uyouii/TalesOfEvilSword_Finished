using UnityEngine;
using System.Collections;

public class ScrollBar : MonoBehaviour {

	public UIScrollView scrollView;
	
	void OnScroll (float delta)
	{
		print ("sb");
		scrollView.Scroll(delta);
		
		transform.parent.parent.GetComponentInChildren<UIScrollBar>().value-=delta;
	}
}
