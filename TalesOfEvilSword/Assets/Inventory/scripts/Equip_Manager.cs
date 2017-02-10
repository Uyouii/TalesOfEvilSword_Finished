using UnityEngine;
using System.Collections;
using System .Collections .Generic ;
/// <summary>
/// 装备栏管理
/// </summary>
public class Equip_Manager : MonoBehaviour {

	public GameObject EquipUI;//装备栏UI
	public GameObject Temp;//被拖拽的临时UI
	private bool showEquipment=false;//是否显示装备栏
	public List<item> Equip=new List<item>();//定义装备栏线性表
	public bool is_draged;

	void Start()
	{
		is_draged = false;
		initEuip ();
	}
	void Update()
	{
		//是否按下装备按钮
		if(Input .GetKeyDown(KeyCode.E))
		{
			Show();
		}

	}

	//显示装备栏
	void Show()
	{
		showEquipment =!showEquipment;
		if(!showEquipment)
			Singleton .inventory .showTooltip=false ;
		//还原窗口位置
		if(showEquipment)
		{
			EquipUI.transform .FindChild("Win").position =EquipUI.transform .position ;
		}

		EquipUI .SetActive(showEquipment);
		//置顶窗口
		Singleton .UI .UI_Top (EquipUI.transform);
	}

	void OnGUI()
	{
		if(GUI .Button(new Rect(40,300,100,40),"装备(快捷键 E)"))
		{
			Show();
		}
	}

	//初始化装备栏
	void initEuip()
	{
		for(int i=0;i<6;i++)
		{
			Equip .Add(new item());
		}
		EquipUI .SetActive (showEquipment);
	}

	//保存装备栏
	public void SaveEquipment()
	{
		for(int i=0;i<Equip .Count ;i++)
		{
			PlayerPrefs .SetInt("Equip"+i,Equip[i].itemID);
		}
	}

	//加载装备栏
	public void LoadEquipment()
	{
		for(int i=0;i<Equip .Count ;i++)
		{
			Equip[i]=PlayerPrefs .GetInt("Equip"+i,-1)>=0 ?  Singleton .inventory .datebase.items[PlayerPrefs .GetInt("Equip"+i)]: new item();
		}
	}

	//返回装备按钮ID号
	public int GetEquipID(string name)
	{
		int n=0;
		switch(name)
		{
		case "Weapon":
			n=0;
			break ;
		case "Clothing":
			n=1;
			break ;
		case "Trousers":
			n=2;
			break ;
		case "Shoes":
			n=3;
			break ;
		case "Casque":
			n=4;
			break ;
		case "Ring":
			n=5;
			break ;
		default :
			n=-1;
			break ;
		}
		return n;
	}

	//装载装备
	void AddEquip()
	{

	}
}
