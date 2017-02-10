using UnityEngine;
using System.Collections;
using System .Collections .Generic ;
/// <summary>
/// 商店
/// </summary>
public class Store : MonoBehaviour {

	public List<item> store=new List<item>();//商品链表
	public GameObject StoreUI;//商店UI
	public GameObject NumInput;//购买数量输入框
	public GameObject Sure;//确认购买
	public GameObject Cancel;//取消购买
	public GameObject Notice;//提示框

	private ItemDatebase datebase;//物品数据
	private bool Show_Store=false;//是否显示商店
	private int ShopID;//购买商品ID号
	private int num=1;//购买数量

	void Start()
	{
		//初始化数量输入框
		InitNumInput ();

		//隐藏数量输入框
		HideNumInput ();

		//获取物品信息
		datebase = GameObject .FindGameObjectWithTag ("item_datebase").GetComponent <ItemDatebase>();

		//初始化商店
		initStore ();
	}

	void Update()
	{
		//限制购买数量
		SetMaxShop ();
	}

	//初始化数量输入框
	void InitNumInput()
	{
		//获取按钮
		Sure = GameObject .Find ("UI Root/store/Win/NumInput/Sure");
		Cancel= GameObject .Find ("UI Root/store/Win/NumInput/Cancel");
		
		//按钮监听事件
		UIEventListener .Get (Sure).onClick =SureClick;
		UIEventListener .Get (Cancel).onClick =CancelClick;
	}

	//确定按钮点击事件
	void SureClick(GameObject button)
	{
		//获取购买数量,并判断输入框不为空
		if(NumInput .GetComponentInChildren<UIInput> ().value!="")
		{
			//确保购买数量大于0
			if(num>0)
			{
				Shop_Item (ShopID);
				HideNumInput ();
			}
		}
	}

	//判断购买数量是否超出限定数量,若超出自动修改为最大购买数量
	void SetMaxShop()
	{
		if(NumInput .activeSelf)
		{
			if(NumInput .GetComponentInChildren<UIInput> ().value!="")
			{
				//获取购买数量,并判断输入框不为空
				num = int.Parse(NumInput .GetComponentInChildren<UIInput> ().value);
				//确保购买数量大于0
				if(num>0)
				{
					//如果购买物品为消耗品
					if(store[ShopID].itemType ==item.ItemType.Potion)
					{
						if(num >store[ShopID].itemMaxNum)
							NumInput .GetComponentInChildren<UIInput> ().value=store[ShopID].itemMaxNum.ToString();
					}
					//如果购买物品不为消耗品
					else 
					{
						if(num> Singleton .inventory .GetSoltNum())
							NumInput .GetComponentInChildren<UIInput> ().value=Singleton .inventory .GetSoltNum().ToString();
					}
				}
			}
		}
	}

	//取消按钮点击事件
	void CancelClick(GameObject button)
	{
		//隐藏数量输入框
		HideNumInput ();
	}

	//初始化商店
	void initStore()
	{
		for(int i=0;i<datebase .items .Count;i++)
		{
			store .Add(datebase .items[i]);
			GameObject Item =(GameObject)Instantiate(Resources .Load("item"));
			Item .GetComponent <StoreCell>().storeID =i;
			Item .transform .parent =GameObject .Find("store/Win/Scroll View/UIGrid").transform ;
			Item .transform .localScale =new Vector3(1,1,1);
			//Item .transform .parent.GetComponent<UIGrid>().repositionNow=true;
		}
		GetComponentInChildren<UIGrid> ().repositionNow = true;
		GetComponentInChildren<UIScrollView>().Press(true);
		StoreUI.SetActive (false);
		Notice .SetActive (false);
	}
	//商店GUI
	void OnGUI()
	{
		if(GUI .Button(new Rect(40,450,100,40),"打开商店"))
		{
			Show();
		}
	}

	//显示商店
	void Show()
	{
		Show_Store=!Show_Store;
		if(!Show_Store)
			Singleton .inventory .showTooltip =false;
		//还原窗口位置
		if(Show_Store)
		{
			transform .FindChild("Win").position =transform .position ;
		}

		StoreUI.SetActive(Show_Store);

		//置顶窗口
		Singleton .UI .UI_Top (StoreUI.transform .parent);
	}

	//显示数量输入框
	public void ShowNumInput(int id)
	{
		NumInput .SetActive(true);
		//设置购买商品ID号
		ShopID = id;

		//设置装备默认购买数量
		if(store[id].itemType !=item.ItemType.Potion)
			NumInput .GetComponentInChildren<UIInput> ().value = "1";
	}

	//隐藏数量输入框
	public void HideNumInput()
	{
		NumInput .SetActive(false);
	}

	//购买商品
	public void Shop_Item(int ID)
	{
		//判断背包是否已满
		if(!Singleton .inventory .is_Full(store[ID],num))
		{
			//判断是否有足够的金钱购买
			if(Singleton .money .money>=store [ID].itemPrice*num)
			{
				for(int i=0;i<num;i++)
				{
					//根据商品表ID添加指定物品ID的物品
					Singleton .inventory .AddItem (store[ID].itemID);
				}
				//扣除相应金钱
				Singleton .money .Set_money (Singleton .money .money - store [ID].itemPrice*num);
			}
			else 
				StartCoroutine(ShowNotice("金币不足,无法购买"));
		}
		else 
			StartCoroutine(ShowNotice("背包已满"));
	}

	//显示提示框
	IEnumerator ShowNotice(string s)
	{
		Notice .SetActive (true);
		Notice .GetComponentInChildren<UILabel> ().text = "提示:" + s;
		//延迟3秒后关闭提示框
		yield return new WaitForSeconds(3f);
		Notice .SetActive (false);
	}
}
