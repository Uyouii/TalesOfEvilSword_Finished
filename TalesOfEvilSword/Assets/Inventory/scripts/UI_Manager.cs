using UnityEngine;
using System.Collections;
using System .Collections .Generic ;

public class UI_Manager : MonoBehaviour {
	public Transform equipment;//装备框
	public Transform inventory;//背包框
	public Transform short_cut;//快捷键框
	public Transform skill;//技能框
	public Transform store;//商店框
	public Transform temp;//拖拽图标
	public Transform tooltip;//信息框
	public List<Transform> UI=new List<Transform>();

	void Start()
	{
		equipment = transform .FindChild ("equipment");
		inventory = transform .FindChild ("inventory");
		short_cut = transform .FindChild ("short cut");
		skill = transform .FindChild ("skill_UI");
		store = transform .FindChild ("store");
		temp = transform .FindChild ("Temp");
		tooltip = transform .FindChild ("Tooltip");

		//初始化UI位置
		UI.Add (equipment);
		UI.Add (inventory);
		UI.Add (skill);
		UI.Add (store);

		//初始化UI顺序及UI事件
		for(int i=0;i<UI.Count;i++)
		{
			SetDepth(UI[i],i+1);
			UI_Event(UI[i]);
		}

	}

	//设置UI顺序
	void UIOrder()
	{
		for(int i=0;i<UI.Count;i++)
		{
			SetDepth(UI[i],i+1);
		}
	}

	//UI单击事件
	void UI_Click(GameObject ui,bool isPress)
	{

		if(Input .GetMouseButtonDown(0))
		{
			UI_Top (ui.transform .parent .parent);
		}
	}

	//UI置顶
	public void UI_Top(Transform ui)
	{
		for(int i=0;i<UI .Count ;i++)
		{
			if(UI[i]==ui)
			{
				UI.Add(UI[i]);
				UI.RemoveAt(i);
				UIOrder ();
				break;
			}
		}

	}
	//UI事件
	void UI_Event(Transform ui)
	{

		GameObject BG = GameObject .Find(ui.name+"/Win/BG");
		UIEventListener .Get(BG).onPress =UI_Click;
	}

	//设置UI深度
	void SetDepth(Transform ui,int depth)
	{
		if(ui.name =="store" || ui.name =="skill_UI")
			ui.Find("Win/Scroll View").GetComponent<UIPanel> ().depth = depth;
		ui.Find("Win").GetComponent<UIPanel> ().depth = depth;

	}

	//获取UI深度
	int GetDepth(Transform ui)
	{
		return ui.GetComponentInChildren <UIPanel>().depth ;
	}
}
