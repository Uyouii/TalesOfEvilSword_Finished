using UnityEngine;
using System.Collections;
/// <summary>
/// 装备栏槽
/// </summary>
public class EquipCell : MonoBehaviour {

	public GameObject Icon;//物品图标物体
	public GameObject Num;//物品数量
	private item Item=new item();//该装备对应的item

	void Start(){
		//初始化item引用
		Item = Singleton.equip.Equip [Singleton.equip.GetEquipID (name)];

	}

	void Update()
	{
		//更新装备栏图标
		Icon.GetComponent<UITexture> ().mainTexture = Singleton.equip.Equip [Singleton.equip.GetEquipID (name)].itemIcon;
	}

	void OnPress()
	{
		Item = Singleton.equip.Equip [Singleton.equip.GetEquipID (name)];
		//当鼠标左击装备栏槽时
		if(Input .GetMouseButtonDown(0))
		{
			//当装备类型相同或者不在拖拽状态中才进入
			if(Singleton.inventory.dragedItem .itemType.ToString()==name || !Singleton .inventory .draggingItem)
			{
				//当前装备槽不为空时
				if(Item.itemName !=null)
				{
					if(!Singleton.inventory .draggingItem)
					{
						//记录拖拽前物品位置，以便右键返回物品
						Singleton .inventory.dragedID =Singleton.equip.GetEquipID (name);                               
						//打开拖拽开关
						Singleton.inventory .draggingItem=true;
						//交换物品
						Singleton.inventory .ChangeItem(ref Item,ref Singleton.inventory .dragedItem);
						//设置物品
						Singleton.equip .Equip[Singleton.equip.GetEquipID (name)]=Item;
						//装备栏物品被拖拽
						Singleton.equip .is_draged =true;
					}
					else 
					{
						//打开拖拽开关
						Singleton.inventory .draggingItem=true;
						//交换物品
						Singleton.inventory .ChangeItem(ref Item,ref Singleton.inventory .dragedItem);
						//设置物品
						Singleton.equip .Equip[Singleton.equip.GetEquipID (name)]=Item;
					}
				}
				//当前装备槽为空时
				else 
				{
					//如果正在拖拽物品
					if(Singleton.inventory .draggingItem)
					{
						//交换物品
						Singleton.inventory .ChangeItem(ref Item,ref Singleton.inventory .dragedItem);
						Singleton.equip .Equip[Singleton.equip.GetEquipID (name)]=Item;
						Singleton.inventory.Temp.GetComponent<UITexture>().mainTexture=Singleton.inventory.dragedItem.itemIcon;
						Singleton.inventory.draggingItem=false;
						Singleton.equip .is_draged=false;
					}
				}
			}

		}

		//当鼠标右击装备栏槽时,并且没有拖拽物
		if(Input .GetMouseButtonDown(1) && !Singleton .inventory .draggingItem )
		{
			if(Item.itemName !=null)
			{
				//寻找背包空格放入
				for(int i=0;i<Singleton .inventory .inventory .Count;i++)
				{
					if(Singleton .inventory .inventory[i].itemName ==null)
					{
						//交换物品
						item T=Singleton.inventory.inventory[i];
						Singleton.inventory .ChangeItem(ref Item,ref T);

						Singleton.equip .Equip[Singleton.equip.GetEquipID (name)]=Item;
						Singleton.inventory.inventory[i]=T;
						break ;
					}
				}
			}
		}
	}

	//鼠标经过装备栏
	void OnHover(bool isOver)
	{
		if(isOver && Singleton.equip .Equip[Singleton.equip.GetEquipID (name)].itemName!=null)
		{
			//设置显示文本信息
			Singleton.inventory.Show_Tooltip(Singleton.equip .Equip[Singleton.equip.GetEquipID (name)]);
			//打开显示文本开关
			Singleton.inventory.showTooltip =true;
		}
		else 
			Singleton.inventory.showTooltip =false;
	}


}
