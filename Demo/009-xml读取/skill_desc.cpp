//typedef  StringA  string; // 为了兼容C#, string默认表示StringA

public class Skill_Configs
{
    [XmlElement("SKILL_COMMON_SKILL")]
    public List<SkillOne> skills;
}

public class SkillOne
{
    [XmlAttribute("AtkAngle")]
    public float AtkAngle;
    [XmlAttribute("IconName")]
    public string IconName;
    [XmlAttribute("SkillOcc")]
    public int SkillOcc;
    [XmlAttribute("CanMove")]
    public bool CanMove;
    
    [XmlAttribute("PlaySkillActionOnMoving")]
    public bool PlaySkillActionOnMoving;
    [XmlAttribute("AtkTargetDesc__")]
    public string AtkTargetDesc;
    [XmlAttribute("CanUpdateSelf")]
    public int CanUpdateSelf;
    [XmlAttribute("CastTargetDesc__")]
    public string CastTargetDesc;
    [XmlAttribute("ElementTypeClient")]
    public int ElementType;
    [XmlAttribute("IsLocked")]
    public int IsLocked;
    [XmlAttribute("CanRotate")]
    public bool canRotate;
    [XmlAttribute("bSelect")]
    public bool bNeedSelect;
    [XmlAttribute("LevelLimit")]
    public int LevelLimit;
    [XmlAttribute("SKILL_ID")]
    public int SKILL_ID;
    [XmlAttribute("SkillCombatState")]
    public int SkillCombatState;
    [XmlAttribute("SkillDesc__")]
    public string SkillDesc;
    [XmlAttribute("SkillDisplayType")]
    public int SkillDisplayType;
    [XmlAttribute("SkillName__")]
    public int SkillName;
    [XmlAttribute("SkillType")]
    public int SkillType;
    [XmlAttribute("SkillTypeDesc__")]
    public string SkillTypeDesc;
    [XmlAttribute("WeaponLimitDesc__")]
    public string WeaponLimitDesc;
    [XmlAttribute("Sound")]
    public string Sound;
    [XmlAttribute("Sound1")]
    public string Sound1;
    [XmlAttribute("IsJump")]
    public byte IsJump;
    [XmlAttribute("SkillLastTime")]
    public float SkillLastTime;
    [XmlAttribute("ShakeType")]
    public byte ShakeType;
    [XmlAttribute("ShakeDelayTime")]
    public float ShakeDelayTime;
    [XmlAttribute("DestType")]
    public int DestType;
    
    [XmlElement("LEVELSET")]
    public SkillLvSet skilllvSet;

    [XmlElement("BeHitEffect")]
    public SkillEffectInfo behitEffect;

    [XmlElement("BeHitFlyEffect")]
    public SkillEffectInfo behitFlyEffect;

    [XmlAttribute("DeathEffect")]
    public int DeathEffect;
    
    [XmlAttribute("DeathEffectRate")]
    public float DeathEffectRate;

    [XmlAttribute("DeathEffectBossValid")]
    public int DeathEffectBossValid;

    [XmlAttribute("HideInSkillPlane")]
    public bool HideInSkillPlane;

    [XmlAttribute("EnergyMax")]
    public float EnergyMax;

    [XmlAttribute("continuityNumb")]
    public float continuityNumb;

    [XmlAttribute("continuityTime")]
    public float continuityTime;

    [XmlAttribute("SkillBreakTime")]
    public float SkillBreakTime;

    // 是不是显示预警
    [XmlAttribute("ShowWarning")]
    public bool ShowWarning;

    [XmlAttribute("WarningLastTime")]
    public float WarningLastTime;

    [XmlAttribute("WarningColorID")]
    public int WarningColorID;

    // 是否锁定相机，释放技能过程中不随玩家移动
    [XmlAttribute("CameraFreeze")]
    public bool CameraFreeze;

    // 锁定相机时长，不填写时默认为技能持续时间
    [XmlAttribute("CameraFreezeTime")]
    public float CameraFreezeTime;

    //瞬移技能相机平滑移动时间
    [XmlAttribute("SmoothCameraTime")]
    public int SmoothCameraTime;

    //普通攻击技能连放，下一连锁技能
    [XmlAttribute("NextLinkSkill")]
    public int NextLinkSkill;

    //技能结束后是否继续播放完技能动作
    [XmlAttribute("ContinueActionOnEnd")]
    public int ContinueActionOnEnd;

    //装备心法后可在被控制状态下使用
    [XmlAttribute("AbnormalCitta")]
    public int AbnormalCitta;

    //服务器确认后再播放技能
    [XmlAttribute("PlayAfterConfirm")]
    public int PlayAfterConfirm;

    //技能结束时停止技能音效
    [XmlAttribute("StopSoundOnEnd")]
    public int StopSoundOnEnd;

    //播放技能同时说话
    [XmlAttribute("TalkOnPlay")]
    public int TalkOnPlay;

    //播放技能同时说话
    [XmlAttribute("SkillLearnLv")]
    public int SkillLearnLv;
}

public class SkillLvSet
{
    [XmlElement("LEVEL")]
    public List<Skill_Lv> skill_lv;
}

public class Skill_Lv
{
    /// <summary>
    /// 冷却时间
    /// </summary>
    [XmlAttribute("CoolDownTime")]
    public int CoolDownTime;

    /// <summary>
    /// 升级经验
    /// </summary>
    [XmlAttribute("ExpCost")]
    public int ExpCost;
    [XmlAttribute("IconID")]
    public int IconID;
    [XmlAttribute("Level")]
    public int Level;
    [XmlAttribute("LevelDesc__")]
    public string LevelDesc;
    /// <summary>
    /// 目标类型
    /// </summary>
    [XmlAttribute("DestType")]
    public int DestType;
    [XmlAttribute("LevelDescTrue")]
    public bool LevelDescTrue;
    [XmlAttribute("LevelLimit")]
    public int LevelLimit;
    [XmlAttribute("angle")]
    public float Angle;
    [XmlAttribute("TargetNum")]
    public int TargetNum;
    [XmlAttribute("CittaCost")]
    public int CittaCost;
    [XmlAttribute("DanCost")]
    public int DanCost;
    [XmlAttribute("MoneyCost")]
    public int MoneyCost;
    [XmlAttribute("GoodsCost")]
    public string GoodsCost;
    [XmlAttribute("GoodsCostShow")]
    public string GoodsCostShow;
    [XmlAttribute("RangeMax")]
    public float RangeMax;
    [XmlAttribute("RangeMin")]
    public float RangeMin;
    [XmlAttribute("RangeRadius")]
    public float RangeRadius;
    [XmlAttribute("SkillPoint")]
    public int SkillPoint;
    [XmlAttribute("SkillPunish")]
    public int SkillPunish;
    [XmlAttribute("AbnormalSkill")]
    public int AbnormalSkill;
    
    [XmlElement("SKILL_STATES")]
    public SkillState skillState;

    [XmlElement("SKILL_CONDITIONS")]
    public SkillCondition skillCondition;

    [XmlElement("SKILL_COSTS")]
    public SkillConst sConst;

    [XmlElement("FANS")]
    public SkillFans skillFans;

    [XmlElement("LINES")]
    public SkillLines skillLine;
}

public class SkillFans
{
    [XmlAttribute("Degree")]
    public float Degree;
}

public class SkillLines
{
    [XmlAttribute("LineSize")]
    public float LineSize;
}

public class SkillState
{
    [XmlElementAttribute("SKILL_STATE_PERFORM")]
    public List<SkillPlayInfo> skillPlay;

    [XmlElementAttribute("SKILL_STATE_HURTS")]
    public List<SkillHurtInfo> skillHurtList;

    [XmlElement("SKILL_STATE_SUMMON_MONSTER")]
    public List<SkillSummonInfo> skillSummonList;

    [XmlElement("SKILL_STATE_FORCEMOVE")]
    public List<SkillForceMoveInfo> skillForceMoveList;
}

public class SkillPlayInfo
{
    [XmlAttribute("RIndex")]
    public int RIndex;
    [XmlAttribute("LastTime")]
    public int lastTime;

    [XmlElementAttribute("SkillActionInfo")]
    public SkillActionInfo skillActions;

    [XmlElementAttribute("SkillEffects")]
    public SkillEffects skillEffects;
}

public class SkillSummonInfo
{
    [XmlAttribute("RIndex")]
    public int RIndex;
    [XmlAttribute("LastTime")]
    public int lastTime;
    [XmlElement("SUMMON_RESULT")]
    public SkillSummonResult skillSummon;
}

public class SkillSummonResult
{
    [XmlAttribute("LastTime")]
    public float LastTime;
    [XmlAttribute("AttackMax")]
    public int AttackMax;
    [XmlElement("Skill")]
    public List<SkillDesReplace> skillReplaceDes;
}

public class SkillForceMoveInfo
{
    [XmlAttribute("RIndex")]
    public int RIndex;
    [XmlAttribute("LastTime")]
    public int lastTime;

    [XmlAttribute("MoveType")]
    public int MoveType;
    [XmlAttribute("MaxMoveRange")]
    public int MaxMoveRange;
}

public class SkillActionInfo
{    
    [XmlAttribute("EquipType")]
    public int EquipType;
    [XmlElementAttribute("SkillAction")]
    public List<SkillAction> skillAction;
}

public class SkillEffects
{
    [XmlElementAttribute("SkillEffect")]
    public List<SkillEffectInfo> skillEffectInfos;
}

public class SkillEffectInfo
{
    [XmlAttribute("EffectID")]
    public int EffectID;

    [XmlAttribute("DelayTime")]
    public float delayTime;

    [XmlAttribute("Scale")]
    public float sale;

    [XmlAttribute("BesierType")]
    public int BesierType;

    [XmlAttribute("BesierDis")]
    public float BesierDis;
    /// <summary>
    /// 0:不绑定 1：头 2：胸 3：脚下
    /// </summary>
    [XmlAttribute("BindPos")]
    public int BindPos;
    /// <summary>
    /// 0:不绑定 1：头 2：胸 3：脚下
    /// </summary>
    [XmlAttribute("BindPosOther")]
    public int BindPosOther;
    /// <summary>
    /// 0：没有目标 1：自己 2：对方
    /// </summary>
    [XmlAttribute("TargetType")]
    public int TargetType;
    [XmlAttribute("LastTime")]
    public int lastTime;

    [XmlAttribute("Speed")]
    public float Speed;
    // private int eType;
    /// <summary>
    /// 1：点 2：方向 3：目标 4：跟随
    /// </summary>
    [XmlAttribute("ETYPE")]
    public int EType;
    [XmlAttribute("MaxDistance")]
    public float MaxDistance;
    [XmlAttribute("bLoop")]
    public bool bLoop;
    [XmlAttribute("Sound")]
    public string Sound;
    [XmlAttribute("Sound1")]
    public string Sound1;
    [XmlAttribute("MutiEffect")]
    public bool MutiEffect;
    [XmlAttribute("FlyType")]
    public int FlyType;
}

public class SkillAction
{
    [XmlAttribute("ActionID")]
    public int ActionID;
    [XmlAttribute("bLoop")]
    public bool Loop;
    [XmlAttribute("DelayTime")]
    public int delayTime;
    // 绑定特效
    [XmlAttribute("EffectID")]
    public int EffectID;

    // 绑定位置
    [XmlAttribute("BindPos")]
    public int BindPos;
}

public class SkillDesReplace
{
    [XmlAttribute("SkillID")]
    public int SkillID;
    [XmlAttribute("SkillLevel")]
    public int SkillLevel;
    [XmlAttribute("TimeUnit")]
    public float TimeUnit;
}

public class SkillShakeInfo
{
    //  是不是影响被攻击者
    [XmlAttribute("AffectBeAttacker")]
    public bool AffectBeAttacker;
    
    [XmlAttribute("Height")]
    public float Height;

    [XmlAttribute("Frequency")]
    public float Frequency;
    
    [XmlAttribute("Time")]
    public float Time;
 
    [XmlAttribute("DelayTime")]
    public float DelayTime;
}

public class SkillHurtInfo
{   
    [XmlAttribute("ActionID")]
    public int ActionID;
    [XmlAttribute("ActionLoop")]
    public int ActionLoop;
    [XmlAttribute("ActionType")]
    public int ActionType;
    [XmlAttribute("BreakByHurt")]
    public int BreakByHurt;
    [XmlAttribute("BreakByMove")]
    public int BreakByMove;
    [XmlAttribute("CalculateTimes")]
    public int CalculateTimes;
    [XmlAttribute("ConditionBuffID")]
    public int ConditionBuffID;
    [XmlAttribute("ConditionBuffLevel")]
    public int ConditionBuffLevel;
    [XmlAttribute("ConditionPSkillID")]
    public int ConditionPSkillID;
    [XmlAttribute("ConditionPSkillLevel")]
    public int ConditionPSkillLevel;
    [XmlAttribute("LastTime")]
    public int lastTime;
    [XmlAttribute("Next")]
    public string Next;
    [XmlAttribute("RIndex")]
    public int RIndex;
    [XmlAttribute("StateName")]
    public string StateName;
    [XmlAttribute("TimeShow")]
    public float TimeShow;
   
    [XmlElement("SKILL_RESULTS")]
    public SkillResult skillResult;
    [XmlElement("Shake")]
    public SkillShakeInfo shakeInfo;
}

public class SkillResult
{
    [XmlElementAttribute("SUFFER_RESULT")]
    public SufferResult sufferResult;

    [XmlElementAttribute("CASTER_RESULT")]
    public SkillCasterResult skillCastResult;
}

public class SufferResult
{
    [XmlElementAttribute("SKILLRESULT_HURT")]
    public SkillHurtResult skillHuit;
}

public class SkillCasterResult
{
    [XmlElementAttribute("SKILLRESULT_BUFF_ADD")]
    public List<SkillBuffAdd> skillBuffs;
}

public class SkillBuffAdd
{
    [XmlAttribute("BuffID")]
    public int BuffID;
    [XmlAttribute("BuffLevel")]
    public int BuffLevel;
}

public class SkillHurtResult
{
    /// <summary>
    /// 暴击威力
    /// </summary>
    [XmlAttribute("AtkCtPowerValue")]
    public int AtkCtPowerValue;
    /// <summary>
    /// 暴击几率
    /// </summary>
    [XmlAttribute("AtkCtValue")]
    public int AtkCtValue;
    [XmlAttribute("AtkHitValue")]
    public int AtkHitValue;
    [XmlAttribute("AtkAppendValue")]
    public int AtkAppendValue;
    [XmlAttribute("AtkMax")]
    public int AtkMax;
    [XmlAttribute("AtkMin")]
    public int AtkMin;
    [XmlAttribute("AtkProp")]
    public float AtkProp;
    [XmlAttribute("ConditionBuff")]
    public int ConditionBuff;
    [XmlAttribute("ElementTypeClient")]
    public int ElementType;
    [XmlAttribute("MainHandWeaponProp")]
    public float MainHandWeaponProp;
    [XmlAttribute("OffHandWeaponProp")]
    public float OffHandWeaponProp;
}

public class SkillCondition
{
    [XmlElement("CNSKILL_CONDITION_EQUIP_NEED")]
    public SkillNeedEquip skillNeedEquip;
}

public class SkillNeedEquip
{   
    [XmlAttribute("ErrorDesc__")]
    public string ErrorDesc;
    [XmlAttribute("Number")]
    public int Number;
    [XmlElement("EquipType")]
    public List<EquipTypeList> equiptypes;
}

public class EquipTypeList
{
    [XmlAttribute("EquipType")]
    public int EquipType;
}

public class SkillConst
{
    [XmlElementAttribute("SKILL_COST_MP")]
    public ConstMp constMP;
}

public class ConstMp
{
    [XmlAttribute("value")]
    public int MValue;
}

