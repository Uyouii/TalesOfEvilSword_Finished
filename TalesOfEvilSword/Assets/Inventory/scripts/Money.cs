using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour {

	public int money;//当前金币
	public GameObject Label;//金币标签

	void Start()
	{
		//初始化金币
		Set_money (money);
	}

	//更新金币
	public void Set_money(int num)
	{
		money = num;
		Label .GetComponent <UILabel> ().text = money.ToString ("#,##0");
	}

	//保存金钱
	public void SaveMoney()
	{
		PlayerPrefs .SetInt ("money",money);
	}

	//加载金钱
	public void LoadMoney()
	{
		Set_money (PlayerPrefs .GetInt("money"));
	}
}
