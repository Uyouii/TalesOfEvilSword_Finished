using UnityEngine;
using System.Collections;
using System .Collections .Generic ;


public class Inventory : MonoBehaviour {
	public int 	slotsX, slotsY;//背包数量
	public GUISkin skin; //背包皮肤
	public List<item> inventory=new List<item>();//定义背包物品线性表
	public List<item> slots = new List<item> ();//定义背包格子线性表
	private ItemDatebase datebase;//物品数据
	private bool showInventory;//是否显示背包
	private bool showTooltip;//是否显示物品提示框
	private string tooltip;//物品提示框文本

	private bool draggingItem;//物品是否被拖拽
	private item dragedItem;//被拖拽的物品
	private int prevIndex;//被拖拽物品的ID号

	void Start () {
		//添加X行Y列个物品
		for(int i=0;i<(slotsX *slotsY);i++)
		{
			slots .Add(new item());
			inventory .Add(new item());
		}
		//获取物品信息
		datebase = GameObject .FindGameObjectWithTag ("item_datebase").GetComponent <ItemDatebase>();
		AddItem (0);
		AddItem (0);
		AddItem (1);
	}
	void Update () {
		//按下背包按键显示背包
		if (Input .GetKeyDown(KeyCode.I)) {
			showInventory =!showInventory ;
		}
	}

	void OnGUI(){
		if(GUI.Button(new Rect(40,400,100,40),"Save"))
		{
			SaveInventory();
		}
		if(GUI.Button(new Rect(40,450,100,40),"Load"))
			LoadInventory();
		tooltip = "";
		//背包GUI皮肤
		GUI.skin = skin;
		//按下时显示背包
		if(showInventory)
		{
			DrawInventory();
			if(showTooltip)
			{
				//绘制提示框
				GUI .Box (new Rect(Event .current .mousePosition .x+15 ,Event .current .mousePosition .y+15 ,200,200),tooltip,skin .GetStyle("Tooltip"));
			}
		}
		//物品被拖拽
		if(draggingItem)
		{
			//物品被拖拽时跟随鼠标移动
			GUI.DrawTexture(new Rect(Event .current .mousePosition .x,Event .current .mousePosition .y ,100,100),dragedItem .itemIcon);
		}

	}
	//绘制背包
	void DrawInventory()
	{
		Event e = Event .current;
		int i = 0;


		for(int y=0;y<slotsY ;y++)
		{
			for(int x=0;x<slotsX ;x++)
			{
				Rect slotRect=new Rect(x*105,y*105,100,100);
				//绘制背包格子
				GUI.Box(slotRect,"",skin .GetStyle("Slot"));
				slots[i]=inventory[i];
				item Item=slots[i];
				if(slots[i].itemName !=null)
				{
					GUI.DrawTexture(slotRect,slots[i].itemIcon );
					//如果鼠标指向物品栏将绘制提示框
					if(slotRect.Contains(Event .current.mousePosition) )
					{
						//返回该物品提示信息
						//CreateTooltip(slots[i]);
						//showTooltip=true;

						//物品被拖拽，并且当前没有其他物品被拖拽
						if(e.isMouse && e.button==0 && e.type ==EventType.mouseDrag && !draggingItem )
						{
							draggingItem =true;
							prevIndex =i;
							dragedItem =Item;
							inventory[i]=new item();

						}
						//当物品被拖拽时在另一个物品上弹起鼠标时进行交换物品
						if(e.isMouse && e.type ==EventType .mouseUp && draggingItem )
						{
							//物品交换
							inventory[prevIndex]=Item;
							inventory[i] =dragedItem ;
							//清空临时拖拽物品
							draggingItem =false ;
							dragedItem =null ;
						}
		
						//没有拖拽物时才显示物品提示信息
						if(!draggingItem)
						{
							//返回该物品提示信息
							CreateTooltip(slots[i]);
							showTooltip=true;
						}
					}
					//说明为空时不显示
					if(tooltip =="")
					{
						showTooltip =false ;
					}
				}
				else 
				{
					if(e.isMouse && e.type ==EventType .mouseUp && draggingItem )
					{
						//交换物品为空时
						if(slotRect.Contains(Event .current.mousePosition))
						{
							slots[prevIndex]=Item;
							inventory[i] =dragedItem ;
							//清空临时拖拽物品
							draggingItem =false ;
							dragedItem =null ;
						}
					}

				}
				//说明为空时不显示
				if(tooltip =="")
				{
					showTooltip =false ;
				}
				i++;//背包格子个数
			}
		}
	}

	//返回该物品提示信息
	string CreateTooltip(item Item)
	{
		tooltip = "<color=#4DA4BF>"+Item .itemName+"</color>\n\n" + "<color=#f2f2f2>"+Item .itemDesc+"</color>";
		return tooltip;
	}
	//添加物品
	void AddItem(int id)
	{
		for(int i=0;i<inventory .Count;i++)
		{
			//背包有空格时添加
			if(inventory[i].itemName==null)
			{
				//遍历所有物品
				for(int j=0;j<datebase.items.Count;j++)
				{
					//找到对应ID的物品
					if(datebase .items[j].itemID ==id)
					{
						//将物品添加至背包
						inventory[i]=datebase .items [j];
					}
				}
				break ;
			}
		}
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

	void  SaveInventory()
	{
		for(int i=0;i<inventory .Count ;i++)
		{
			PlayerPrefs .SetInt ("Inventory"+i,inventory[i].itemID);
		}
	}

	void LoadInventory()
	{
		for(int i=0;i<inventory .Count ;i++)
			inventory[i]= PlayerPrefs .GetInt("Inventory"+i,-1)>=0 ? datebase .items[PlayerPrefs .GetInt("Inventory"+i)] : new item() ;
	}
}
