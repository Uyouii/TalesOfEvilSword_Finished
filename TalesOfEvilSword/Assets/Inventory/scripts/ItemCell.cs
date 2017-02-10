using UnityEngine;
using System.Collections;
/// <summary>
/// 背包物品按钮
/// </summary>
public class ItemCell : MonoBehaviour {

	public GameObject Icon;//物品图标物体
	public GameObject Num;//物品数量
	private item Item=new item();//该物品对应的item
	void Start()
	{
		//初始化Item引用
		Item = Singleton.inventory .inventory [int.Parse (name)];

	}
	void Update()
	{
		//更新背包物品图标
		Icon.GetComponent<UITexture>().mainTexture =Singleton.inventory .inventory [int.Parse (name)].itemIcon; 

		//更新背包物品数量
		if(Singleton.inventory .inventory [int.Parse (name)].itemNum>1)
			Num .GetComponent<UILabel> ().text = Singleton.inventory .inventory [int.Parse (name)].itemNum.ToString();
		else 
			Num .GetComponent<UILabel> ().text ="";
	}
	//鼠标按下背包物品
	void OnPress()
	{
		Item = Singleton.inventory .inventory [int.Parse (name)];

		//背包物品右击被使用
		if(Input .GetMouseButtonDown(1) && Item.itemName !=null && !Singleton .inventory .draggingItem)
		{
			//将背包id传递过去
			Singleton.inventory .UseItem(int.Parse (name));
		}

		if(Input .GetMouseButtonDown(0))
		{
			//拖拽物不为快捷键
			if(!Singleton .key .draggingKey)
			{
				if(Item.itemName !=null)
				{
					if(!Singleton.inventory .draggingItem)
					{
						//记录被拖拽物的ID，方便返回物品
						Singleton .inventory .dragedID=int.Parse(name);
						//打开拖拽开关
						Singleton.inventory.draggingItem=true;
						//交换物品
						Singleton .inventory .dragedItem =Singleton.inventory .inventory [int.Parse (name)];
						//设置物品为空
						Singleton.inventory .inventory [int.Parse (name)]=new item();
					}
					//装备物品被拖拽时按下,并且拖拽物品和该物品类型不一致时
					else if(Singleton .equip .is_draged && Singleton .inventory .dragedItem .itemType !=Item .itemType)
					{
						for(int i=0;i<Singleton .inventory .inventory .Count ;i++)
						{
							if(Singleton .inventory.inventory[i].itemID ==-1)
							{
								Singleton .inventory.inventory[i]=Singleton .inventory .dragedItem ;
								Singleton .inventory .Clear_dragedItem();
								Singleton .equip .is_draged=false;
							}
						}
					}
					else 
					{
						//交换物品
						Singleton.inventory .ChangeItem(ref Item,ref Singleton.inventory .dragedItem);
						//设置物品
						Singleton.inventory .inventory [int.Parse (name)]=Item;
					}
				
				}
				else 
				{
					if(Singleton.inventory .draggingItem )
					{
						//交换物品
						Singleton.inventory .ChangeItem(ref Item,ref Singleton.inventory .dragedItem);
						Singleton.inventory .inventory [int.Parse (name)]=Item;
						Singleton.inventory.Temp.GetComponent<UITexture>().mainTexture=Singleton.inventory.dragedItem.itemIcon;
						Singleton.inventory.draggingItem=false;
						Singleton.equip .is_draged=false;
					}
				}
			}
			else 
			{
				//如果拖拽物为快捷键，那就清空拖拽物
				Singleton .inventory .Clear_dragedItem();
			}
		}
	}

	//鼠标经过背包物品
	void OnHover(bool isOver)
	{
		if(isOver && Singleton.inventory .inventory [int.Parse (name)].itemName!=null)
		{
			//设置显示文本信息
			Singleton.inventory.Show_Tooltip(Singleton.inventory .inventory [int.Parse (name)]);
			//打开显示文本开关
			Singleton.inventory.showTooltip =true;
		}
		else 
			Singleton.inventory.showTooltip =false;
	}
}
