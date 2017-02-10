using UnityEngine;
using System.Collections;
/// <summary>
/// 技能槽
/// </summary>
public class SkillCell : MonoBehaviour {

	public GameObject Icon;//技能图标
	public GameObject Level;//技能等级
	public GameObject Name;//技能名称
	public int skillID;//技能槽ID号

	void Update()
	{
		//显示数据
		Icon .GetComponent<UITexture> ().mainTexture = Singleton .skillUI .skill [skillID].skill_Icon;
		Level .GetComponent<UILabel> ().text = "Lv"+Singleton .skillUI .skill [skillID].skill_level.ToString()+"/"+Singleton .skillUI .skill [skillID].Max_level.ToString();
		Name .GetComponent <UILabel> ().text = Singleton .skillUI .skill [skillID].skill_Name;

	}

	void OnPress()
	{
		if(Input .GetMouseButtonDown(0))
		{
			//不在拖拽状态中
			if(!Singleton .inventory .draggingItem && !Singleton .key .draggingKey )
			{
				//判断该图标是否是被动技能
				if(Singleton .skillUI .skill [skillID].CoolDown !=0)
				{
					Singleton .skillUI .draggingSkill =true;
					Singleton .skillUI .dragedSkill =Singleton .skillUI .skill [skillID];
				}
			}
		}

	}

	//鼠标经过技能框
	void OnHover(bool isOver)
	{
		if(isOver)
		{
			Singleton .skillUI .Show_Tooltip(skillID);
			Singleton .inventory .showTooltip =true;
		}
		else 
		{
			Singleton .inventory .showTooltip=false ;
		}
	}
}
