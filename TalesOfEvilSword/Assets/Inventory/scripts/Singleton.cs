using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour {

	public static Inventory2 inventory;//背包
	public static Equip_Manager equip;//装备栏
	public static Store store;//商店
	public static Key key;//快捷键
	public static Skill_UI skillUI;//技能框
	public static Money money;//金钱
	public static UI_Manager UI;//UI管理

	void Awake()
	{
		inventory=GameObject .Find ("Inventory").GetComponent<Inventory2> ();
		equip =GameObject .Find("Equipment").GetComponent<Equip_Manager>();
		store =GameObject .Find("store").GetComponent<Store>();
		key = GameObject .Find ("short cut/Key").GetComponent <Key> ();
		skillUI = GameObject .Find ("skill_UI").GetComponent <Skill_UI> ();
		money = GameObject .Find ("inventory/Win/money").GetComponent <Money> ();
		UI = GameObject .Find ("UI Root").GetComponent <UI_Manager> ();
	}
}
