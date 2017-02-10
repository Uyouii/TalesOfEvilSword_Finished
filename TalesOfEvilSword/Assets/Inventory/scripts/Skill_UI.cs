using UnityEngine;
using System.Collections;
using System .Collections .Generic ;
/// <summary>
/// 技能框
/// </summary>
public class Skill_UI : MonoBehaviour {

	public List<Skill> skill=new List<Skill>();//技能链表
	private SkillDatebase datebase;//技能数据库
	public GameObject skillWin;//技能框界面
	public GameObject Tooltip;//技能介绍框
	public bool draggingSkill;//技能是否正被拖拽
	public Skill dragedSkill;//被拖拽的技能
	private bool Show_Skill=false;//是否显示技能框
	public GameObject Temp;//拖拽临时图标
	void Start()
	{
		//初始化技能数据
		datebase = GameObject.Find ("Skill_Datebase").GetComponent<SkillDatebase> ();

		//初始化技能框界面
		initSkill_UI ();

	}

	void Update()
	{
		//是否显示技能框
		if(Input .GetKeyDown(KeyCode.K))
		{
			Show();
		}

		//如果技能正被拖拽
		if(draggingSkill)
		{
			if(Input .GetMouseButtonDown(1))
			{
				//清空技能拖拽
				Clear_Draged();
			}
			Temp .transform .position = UICamera .currentCamera .ScreenToWorldPoint (Input .mousePosition); 
			Temp.GetComponent<UITexture>().mainTexture=dragedSkill .skill_Icon;
		}

		//技能冷却
		skillCD ();
	}

	//技能冷却
	void skillCD()
	{
		for(int i=0;i<skill .Count ;i++)
		{
			if(skill[i].CurCD!=0)
			{
				skill[i].CurCD-=Time .deltaTime;
				if(skill[i].CurCD<=0)
					skill[i].CurCD=0;
			}
		}
	}

	//显示技能界面
	void Show()
	{
		Show_Skill=!Show_Skill ;
		if(!Show_Skill)
			Singleton.inventory.showTooltip=false ;
		//还原窗口位置
		if(Show_Skill)
		{
			transform .FindChild("Win").position =transform .position ;
		}
		skillWin .SetActive(Show_Skill);
		//置顶窗口
		Singleton .UI .UI_Top (skillWin .transform .parent);

	}

	void OnGUI()
	{
		if(GUI .Button(new Rect(40,350,100,40),"技能(快捷键 K)"))
		{
			Show();
		}
	}
	//初始化技能框
	void initSkill_UI()
	{
		for(int i=0;i<datebase .skills.Count;i++)
		{
			skill.Add(datebase .skills[i]);
			GameObject Skill =(GameObject)Instantiate(Resources .Load("skill"));
			Skill .GetComponent <SkillCell>().skillID=i;
			Skill .transform .parent =GameObject .Find("skill_UI/Win/Scroll View/UIGrid").transform ;
			Skill .transform .localScale =new Vector3(1,1,1);
		}
		GetComponentInChildren<UIGrid> ().repositionNow = true;
		GetComponentInChildren<UIScrollView>().Press(true);
		skillWin .SetActive (false);
		draggingSkill = false;
	}

	//清空技能拖拽
	public void Clear_Draged()
	{
		dragedSkill =new Skill();
		draggingSkill =false;
		Temp.GetComponent<UITexture> ().mainTexture = null;
	}

	//技能升级
	public void SkillUP(int id)
	{
		if(skill [id].skill_level<skill [id].Max_level)
			skill [id].skill_level ++;
	}

	//使用技能
	public void UseSkill(ref Skill S)
	{
		if(S.CurCD ==0)
		{
			//施放技能
			S.Puting ();
			//设置冷却时间
			S.CurCD =S.CoolDown ;
		}
	}

	//寻找技能框是否有该id的技能,若有则返回技能框ID号,否则返回-1
	public int GetSkillID(int id)
	{
		for(int i=0;i<skill .Count ;i++)
		{
			if(skill[i].skill_ID ==id)
			{
				return i;
			}
		}
		return -1;
	}

	//显示技能介绍
	public void Show_Tooltip(int id)
	{
		//判断是否是被动技能
		if(skill[id].CoolDown ==0)
			Tooltip .GetComponentInChildren<UILabel> ().text = "[FF0000]名称:[-] [99ff00][被动][-]"+skill [id].skill_Name + "\n\n" + 
			"[FF0000]说明:[-] "+skill [id].skill_Desc;
		else 
			Tooltip .GetComponentInChildren<UILabel> ().text = "[FF0000]名称:[-] "+skill [id].skill_Name + "\n\n" + 
			"[FF0000]说明:[-] "+skill [id].skill_Desc;
	}

	//保存技能数据
	public void SaveSkill()
	{
		for(int i=0;i<skill .Count ;i++)
		{
			//保存技能等级
			PlayerPrefs .SetInt ("Skill Level"+i,skill[i].skill_level);
		}
	}

	//加载技能数据
	public void LoadSkill()
	{
		for(int i=0;i<skill .Count ;i++)
		{
			//加载技能等级
			skill[i].skill_level = PlayerPrefs .GetInt("Skill Level"+i);
		}
	}
}
