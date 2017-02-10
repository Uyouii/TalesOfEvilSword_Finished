using UnityEngine;
using System.Collections;
using System .Collections .Generic ;
/// <summary>
/// 技能数据
/// </summary>
public class SkillDatebase : MonoBehaviour {

	public List<Skill> skills=new List<Skill>();

	void Start()
	{
		skills .Add (new Skill("双刀斩",0,"快速攻击两次",0,10,4f));
		skills .Add (new Skill("力量强化",1,"增强人物力量",0,10,0f));
		skills .Add (new Skill("快速刀",2,"以极快的速度砍出",0,10,3f));
		skills .Add (new Skill("天崩地裂",3,"对敌方造成全体土属性伤害",0,10,10f));
		skills .Add (new Skill("雷霆万钧",4,"对敌方造成全体雷属性伤害",0,10,10f));
		skills .Add (new Skill("疾风步",5,"快速移动",0,10,20f));
		skills .Add (new Skill("治疗术",6,"增加人物30%血量",0,10,20f));
	}
}
