using UnityEngine;
using System.Collections;
/// <summary>
/// 技能父类
/// </summary>
[System .Serializable]
public class Skill{

	public string skill_Name;//技能名称
	public int skill_ID;//技能ID
	public string skill_Desc;//技能说明
	public Texture2D skill_Icon;//技能图标
	public int skill_level;//当前技能等级
	public int Max_level;//最大技能等级
	public float CoolDown;//技能CD
	public float CurCD;//当前技能CD

	//构造函数
	public Skill(string name,int id,string desc,int level,int max,float CD)
	{
		skill_Name = name;
		skill_ID = id;
		skill_Desc = desc;
		skill_Icon= Resources .Load<Texture2D>("Skill Icon/"+skill_ID .ToString());
		skill_level = level;
		Max_level = max;
		CoolDown = CD;
		CurCD = 0;
	}

	//空构造函数
	public Skill()
	{
		skill_ID = -1;
	}

	//深拷贝函数
	public Skill Clone()       
	{            
		return this.MemberwiseClone()as Skill;        
	}

	//施放技能
	public void Puting()
	{
		Debug .Log ("我施放了"+skill_Name);
	}
}
