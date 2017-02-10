using UnityEngine;
using System.Collections;
/// <summary>
/// 快捷按钮
/// </summary>
public class KeyCell : MonoBehaviour {

	public GameObject Icon;//快捷键图标
	public GameObject Num;//物品数量
	public item keyItem;//快捷物品
	public Skill keySkill;//快捷技能
	public int KeyItemID;//快捷键物品背包的ID号

	void Start()
	{
		KeyItemID = -1;
	}

	void Update()
	{
		//显示
		Show_Date ();
		//快捷键操作
		UesKey ();

		//快捷键冷却图标
		Icon_CoolDown ();

	}

	//快捷键冷却图标
	void Icon_CoolDown()
	{
		if(keySkill.skill_ID !=-1)
		{
			if(keySkill .CurCD !=0)
			{
				//图标冷却显示
				Icon .GetComponentInChildren<UISprite>().fillAmount =keySkill .CurCD /keySkill .CoolDown ;
			}
			else 
			{
				Icon .GetComponentInChildren<UISprite>().fillAmount =0;
			}
		}
		else
			//清除图标冷却
			Icon .GetComponentInChildren<UISprite>().fillAmount =0;
	}

	//判断是否按下快捷键
	void UesKey()
	{
		if(Input .GetKeyDown(name.ToLower()))
		{
			if(keyItem .itemID !=-1)
			{
				Singleton .inventory .UseItem(KeyItemID);
			}
			else if(keySkill .skill_ID !=-1)
			{
				Singleton .skillUI .UseSkill(ref keySkill);
			}
		}
	}

	//显示图标及数量
	void Show_Date()
	{
		if(keyItem .itemIcon !=null)
			//显示快捷物品图标
			Icon .GetComponent<UITexture> ().mainTexture = keyItem .itemIcon;

		else if(keySkill .skill_Icon !=null)
			//显示快捷技能图标
			Icon .GetComponent<UITexture> ().mainTexture =keySkill .skill_Icon ;

		else 
			Icon .GetComponent<UITexture> ().mainTexture =null;


		if(keyItem .itemNum !=0)
			Num .GetComponent<UILabel>().text =keyItem .itemNum.ToString() ;
		else 
			Num .GetComponent<UILabel>().text =null;
	}
	//鼠标事件
	void OnPress()
	{
		if(Input .GetMouseButtonDown(0))
		{ 
			//物品正在拖拽时
			if(Singleton.inventory .draggingItem )
			{
				//拖拽物必须为消耗品才可以设置快捷键
				if(Singleton .inventory .dragedItem .itemType==item.ItemType.Potion)
				{
					//清除该物品之前的快捷键
					Singleton .key .Clear_ItemKey(Singleton .inventory .dragedItem);
					keyItem =Singleton .inventory .dragedItem;
					KeyItemID =Singleton .inventory .dragedID;
					Singleton .inventory .BackItem();

					//清空快捷技能
					keySkill =new Skill();
				}
			}
			//如果拖拽图标是技能
			else if(Singleton .skillUI .draggingSkill)
			{
				//清空该技能之前的快捷键
				Singleton .key .Clear_SkillKye(Singleton .skillUI .dragedSkill);

				keySkill =Singleton .skillUI .dragedSkill;
				//清空技能拖拽
				Singleton .skillUI .Clear_Draged();  

				//清空快捷物品
				KeyItemID =-1;
				keyItem =new item();
			}
			//如果拖拽的是快捷键
			else if(Singleton .key .draggingKey)
			{
				keyItem =Singleton .inventory .dragedItem;
				KeyItemID =Singleton .inventory .dragedID;
				//清空拖拽
				Singleton .inventory .Clear_dragedItem(); 
				keySkill =new Skill();
			}
			else 
			{

				if(keyItem.itemID !=-1)
				{
					//设置拖拽物
					Singleton .inventory .dragedItem =keyItem;
					Singleton .inventory .dragedID=KeyItemID;
					KeyItemID =-1;
					keyItem =new item();
					//快捷键被拖拽
					Singleton .key .draggingKey =true;
				}
				else if(keySkill .skill_ID !=-1 )
				{
					Singleton .skillUI .dragedSkill=keySkill;
					Singleton .skillUI .draggingSkill =true;
					keySkill=new Skill();
				}

			}
		}
	}

}
