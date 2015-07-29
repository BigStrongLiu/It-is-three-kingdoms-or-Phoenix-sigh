using UnityEngine;
using System.Collections.Generic;

public struct AnimatorConst
{
    //const 是在编译时确定
    //static readonly 是在运行时构造

    public const string Idle = "Idle";
    public const string Attack = "Attack";
    public const string Skill1 = "Skill1";
    public const string Hurt = "Hurt";
    public const string Dead = "Dead";

    public static readonly int IdleHashID = Animator.StringToHash(Idle);
    public static readonly int AttackHashID = Animator.StringToHash(Attack);
    public static readonly int Skill1HashID = Animator.StringToHash(Skill1);
    public static readonly int HurtHashID = Animator.StringToHash(Hurt);
    public static readonly int DeadHashID = Animator.StringToHash(Dead);

    public static readonly Dictionary<int, int> SkillDic = new Dictionary<int, int>()
    {
        { 1,Skill1HashID},
    };

}
