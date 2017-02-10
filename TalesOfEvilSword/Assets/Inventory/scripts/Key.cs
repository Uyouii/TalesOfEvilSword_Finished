using UnityEngine;
using System.Collections;
using System .Collections .Generic ;
/// <summary>
/// 快捷键管理
/// </summary>
public class Key : MonoBehaviour {
	
	public bool draggingKey;//快捷键正被拖拽
	void Start()
	{
		draggingKey = false;
	}

	void Update()
	{
		//正在拖拽快捷键时按右键则清空拖拽物
		if(draggingKey)
		{
			if(Input .GetMouseButtonDown(1))
				Singleton .inventory .Clear_dragedItem();
		}
	}

	//清空该物品快捷键
	public void Clear_ItemKey(item itemKey)
	{
		foreach(Transform child in transform)
		{
			if(child .GetComponent<KeyCell>().keyItem==itemKey)
			{
				child .GetComponent<KeyCell>().keyItem=new item();
				break;
			}
		}
	}

	//清空该技能快捷键
	public void Clear_SkillKye(Skill skillKey)
	{
		foreach(Transform child in transform)
		{
			if(child .GetComponent<KeyCell>().keySkill ==skillKey)
			{
				child .GetComponent<KeyCell>().keySkill=new Skill();
				break;
			}
		}
	}

	//快捷键保存
	public void SaveKey()
	{
		int i = 0;
		foreach(Transform child in transform)
		{
			PlayerPrefs .SetInt("KeySkill"+i,child .GetComponent <KeyCell>().keySkill.skill_ID);
			PlayerPrefs .SetInt ("KeyItem"+i,child .GetComponent <KeyCell>().KeyItemID);
			i++;
		}
	}

	//快捷键加载
	public void LoadKey()
	{
		int i = 0;
		foreach(Transform child in transform)
		{
			if(PlayerPrefs .GetInt("KeySkill"+i,-1)>=0)
				child .GetComponent <KeyCell>().keySkill=Singleton .skillUI .skill[Singleton .skillUI .GetSkillID(PlayerPrefs .GetInt("KeySkill"+i))];
			else 
				child .GetComponent <KeyCell>().keySkill=new Skill();

			if(PlayerPrefs .GetInt("KeyItem"+i,-1)>=0)
			{
				child .GetComponent <KeyCell>().KeyItemID=PlayerPrefs .GetInt("KeyItem"+i);
				child .GetComponent <KeyCell>().keyItem =Singleton .inventory .inventory[PlayerPrefs .GetInt("KeyItem"+i)];
			}
			else 
			{
				child .GetComponent <KeyCell>().KeyItemID=-1;
				child .GetComponent <KeyCell>().keyItem =new item ();
			}

			i++;
		}
	}
}
