using UnityEngine;
using System.Collections;
/// <summary>
/// 商品槽
/// </summary>
public class StoreCell : MonoBehaviour {

	public GameObject Icon;//图标
	public GameObject Price;//价格
	public GameObject Name;//商品名称
	public int storeID;//商品ID号

	void Update()
	{
		if(Singleton .store .store [storeID].itemID !=-1)
		{
			Icon .GetComponent<UITexture> ().mainTexture = Singleton .store .store [storeID].itemIcon;
			Price .GetComponent<UILabel> ().text = Singleton .store .store [storeID].itemPrice.ToString();
			Name .GetComponent <UILabel> ().text = Singleton .store .store [storeID].itemNameCN;
		}
	}
	//鼠标经过商店物品
	void OnHover(bool isOver)
	{
		if(isOver)
		{
			Singleton.inventory.Show_Tooltip(Singleton .store .store [storeID]);
			Singleton .inventory .showTooltip =true;
		}
		else 
			Singleton .inventory .showTooltip=false ;
	}
}
