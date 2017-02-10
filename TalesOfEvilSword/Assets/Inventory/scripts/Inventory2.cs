using UnityEngine;
using System.Collections;
using System .Collections .Generic ;

/// <summary>
/// 背包管理
/// </summary>
public class Inventory2 : MonoBehaviour {
	public int 	count;//背包格子数量
	public GameObject InventoryUI;//背包UI
	public GameObject Temp;//被拖拽的临时UI
	public List<item> inventory=new List<item>();//定义背包物品线性表
	public ItemDatebase datebase;//物品数据
	private bool showInventory=false;//是否显示背包
	public bool draggingItem;//物品是否被拖拽
	public item dragedItem;//被拖拽的物品
	public int dragedID;//被拖拽物品的ID号
	public bool showTooltip;//是否显示物品提示框
	public GameObject Tooltip;//物品提示框


	void Start () {
		//添count个物品
		for(int i=0;i<count;i++)
		{
			inventory .Add(new item());
		}
		//获取物品信息
		datebase = GameObject .FindGameObjectWithTag ("item_datebase").GetComponent <ItemDatebase>();

		AddItem (0);
		AddItem (0);
		AddItem (1);
		AddItem (2);
		AddItem (3);
		AddItem (4);
		AddItem (5);
		AddItem (6);

		//初始化背包
		InitInventory ();
		draggingItem=false;
		dragedItem = new item ();

	}

	void Update()
	{
		//是否显示背包
		if(Input .GetKeyDown(KeyCode.I))
		{ 
			Show();
		}
		//是否显示物品提示
		if(showTooltip)
			Tooltip .transform .position = UICamera .currentCamera .ScreenToWorldPoint (Input .mousePosition);
		else 
			Tooltip .transform .position =new Vector3(0,10000,0);

		//是否被拖拽
		if(draggingItem || Singleton .key .draggingKey)
		{
			Temp .transform .position = UICamera .currentCamera .ScreenToWorldPoint (Input .mousePosition); 
			Temp.GetComponent<UITexture>().mainTexture=dragedItem.itemIcon;
		}
		//判断物品被拖拽时按下右键返回物品
		if(Input .GetMouseButtonDown(1))
		{
			BackItem ();
		}
	}

	//交换物品
	public void ChangeItem(ref item Item1,ref item Item2)
	{
		item t=new item();
		t = Item1;
		Item1 = Item2;
		Item2 = t;
	}

	//清空拖拽
	public void Clear_dragedItem()
	{
		dragedItem = new item ();
		draggingItem = false;
		Singleton .key .draggingKey = false;
		Temp.GetComponent<UITexture> ().mainTexture = null;

	}

	//物品被拖拽时按下右键返回物品
	public void BackItem()
	{
		if(draggingItem)
		{
			if(Singleton.equip .is_draged)
			{
				Singleton .equip .Equip[dragedID]=dragedItem;
				Singleton .equip .is_draged =false ;
			}
			else
				inventory[dragedID]=dragedItem ;
			//清空临时数据
			Clear_dragedItem();
		}
	}

	//显示背包
	void Show()
	{
		showInventory=!showInventory;
		//如果背包隐藏那么提示框也隐藏
		if(!showInventory)
			showTooltip =false ;
		//还原窗口位置
		if(showInventory)
		{
			InventoryUI.transform.FindChild("Win").position =InventoryUI.transform .position ;
		}

		InventoryUI .SetActive(showInventory);

		//置顶窗口
		Singleton .UI .UI_Top (InventoryUI.transform);
	}

	void OnGUI()
	{
		if(GUI .Button(new Rect(40,400,100,40),"背包(快捷键 i )"))
		{
			Show();
		}
	}

	//初始化背包
	void InitInventory()
	{
		for(int i=0;i<count;i++)
		{	
			GameObject Slot=(GameObject)Instantiate(Resources .Load("Slot"));
			Slot.transform .parent =GameObject .Find("inventory/Win/item").transform;
			Slot.transform .localScale =new Vector3(1,1,1);
			Slot.transform .parent.GetComponent<UIGrid>().repositionNow=true;
			Slot .name =i.ToString();
			if(inventory[i].itemName !=null)
			{
				Slot.GetComponent<ItemCell>().Icon .GetComponent <UITexture>().mainTexture=inventory[i].itemIcon;
			}
		}
		//隐藏背包
		InventoryUI .SetActive(showInventory);
	}
	//添加物品
	public void AddItem(int id)
	{	
		int i;
		for(i=0;i<inventory .Count;i++)
		{
			//该id物品在背包内
			if(InventoryContains(id))
			{
				//背包有空格时添加
				if(inventory[i].itemID ==id )
				{
					if(inventory[i].itemNum <inventory[i].itemMaxNum)
					{
						inventory[i].itemNum ++;
						break;
					}
				}
			}
			else//如果该id物品不在背包内
			{
				if(inventory[i].itemName==null)
				{
					inventory[i]=datebase .items [id].Clone();
					break ;
				}
			}
		}
		//如果没有找到未满的物品就新建一个
		if(i==inventory .Count)
		{
			for(i=0;i<inventory .Count;i++)
			{
				if(inventory[i].itemName==null)
				{
					inventory[i]=datebase .items [id].Clone();
					break ;
				}
			}
		}
	}

	//判断背包是否已满
	public bool is_Full(item Item,int num)
	{
		int i;
		bool result=true;

		{
			for(i=0;i<inventory .Count;i++)
			{
				if(inventory[i].itemID ==Item.itemID)
				{
					if(Item .itemType ==item.ItemType.Potion)
					{
						if(inventory[i].itemMaxNum-inventory[i].itemNum >=num)
						{
							result =false;
							break ;
						}
					}
					else 
					{
						if(inventory[i].itemID ==-1)
						{
							result=false ;
							break ;
						}
					}
				}
				else 
				{
					if(inventory[i].itemID ==-1)
					{
						result=false ;
						break ;
					}
				}
			}
		}
		return result;

	}
	//删除背包物品
	void RemoveItem(int id)
	{
		for(int i=0;i<inventory .Count;i++)
		{
			if(inventory[i].itemID==id)
			{
				//初始化背包格子
				inventory [i]=new item();
				break ;
			}
		}
		
	}
	//判断对应ID物品是否在背包里
	bool InventoryContains(int id)
	{
		bool result = false;
		for(int i=0;i<inventory .Count ;i++)
		{
			result=inventory[i].itemID==id;
			if(result)
			{
				break ;
			}
		}
		return result;
	}

	//显示提示信息
	public void Show_Tooltip(item Item)
	{
		Tooltip .GetComponentInChildren<UILabel> ().text ="[FF0000]名称:[-] "+Item .itemNameCN+"\n\n" +"[FF0000]说明:[-] "+ Item .itemDesc;
	}

	//保存背包物品
	public void  SaveInventory()
	{
		for(int i=0;i<inventory .Count ;i++)
		{
			PlayerPrefs .SetInt ("Inventory"+i,inventory[i].itemID);
			PlayerPrefs .SetInt ("InventoryNum"+i,inventory[i].itemNum);
		}
	}
	//加载背包物品
	public void LoadInventory()
	{
		for(int i=0;i<inventory .Count ;i++)
		{
			inventory[i]= PlayerPrefs .GetInt("Inventory"+i,-1)>=0 ? datebase .items[PlayerPrefs .GetInt("Inventory"+i)] : new item() ;

			inventory[i].itemNum = PlayerPrefs .GetInt("InventoryNum"+i,1);
		}
	}

	//背包物品使用
	public void UseItem(int id)
	{
		inventory[id].gongneng ();
		//数量为1时清空物品格
		if(inventory[id].itemNum==1)
		{
			if(inventory[id].itemType!=item.ItemType .Potion)
			{
				//初始化临时引用
				item T=Singleton.equip.Equip[Singleton.equip .GetEquipID(inventory[id].itemType.ToString())];
				item T2=inventory[id];
				//交换位置
				ChangeItem(ref T,ref T2);
				//返回数据
				Singleton.equip.Equip[Singleton.equip .GetEquipID(inventory[id].itemType.ToString())]=T;
				inventory[id]=T2;
			}
			else
			{
				//清空该物品快捷键
				Singleton .key .Clear_ItemKey(inventory[id]);
				//初始化背包空格
				inventory[id] = new item ();
			}
		}
		else
			inventory[id].itemNum --;
	}

	//获取背包剩余空格数量
	public int GetSoltNum()
	{
		int count=0;
		for(int i=0;i<inventory .Count ;i++)
		{
			if(inventory[i].itemID ==-1)
			{
				count ++;
			}
		}
		return count;
	}
}
