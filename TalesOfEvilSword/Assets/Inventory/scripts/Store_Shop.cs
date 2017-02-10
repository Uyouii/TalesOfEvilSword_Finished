using UnityEngine;
using System.Collections;
/// <summary>
/// 商品购买
/// </summary>
public class Store_Shop : MonoBehaviour {

	private int id;
	void Start()
	{
		id = transform.parent .GetComponentInChildren<StoreCell> ().storeID;
	}
	void OnPress()
	{
		if(Input .GetMouseButtonDown(0))
		{
			Singleton .store .ShowNumInput(id);
		}
	}
}
