using UnityEngine;
using System.Collections;

[System .Serializable]
public class item {

	public string itemName;//物品名称
	public int itemID;//物品ID
	public string itemNameCN;//物品中文名
	public string itemDesc;//物品说明
	public Texture2D itemIcon;//物品图标
	public int itemNum;//物品数量
	public int itemMaxNum;//物品数量上限
	public ItemType itemType;//物品类型
	public int itemPrice;//物品价格

	//物品类型
	public enum ItemType{
		//武器
		Weapon,
		//头盔
		Casque,
		//衣服
		Clothing,
		//戒指
		Ring,
		//鞋子
		Shoes,
		//裤子
		Trousers,
		//药剂
		Potion,
	}

	//构造函数
	public item(string name,int id,string nameCN,string desc,int max_num,ItemType type,int price)
	{
		itemName = name;
		itemID = id;
		itemNameCN = nameCN;
		itemDesc = desc;
		itemIcon = Resources .Load<Texture2D>("Item Icon/"+name);
		itemNum = 1;
		itemMaxNum = max_num;
		itemType = type;
		itemPrice = price;
	}
	//空构造函数
	public item()
	{
		itemID = -1;
	}

	//深拷贝函数
	public item Clone()       
	{            
		return this.MemberwiseClone()as item;        
	}

	//功能
	public void gongneng()
	{
		Debug .Log("您使用了"+itemNameCN);
	}
}
