using UnityEngine;
using System.Collections;
/// <summary>
/// 技能升级按钮
/// </summary>
public class Skill_Up : MonoBehaviour {

	void OnPress()
	{
		if(Input .GetMouseButtonDown(0))
		{
			//升级技能
			Singleton .skillUI .SkillUP(transform .parent .GetComponent<SkillCell>().skillID);
		}
	}
}
