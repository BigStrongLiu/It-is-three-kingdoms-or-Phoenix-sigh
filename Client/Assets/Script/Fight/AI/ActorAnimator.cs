using UnityEngine;
using System.Collections.Generic;

public class ActorAnimator : MonoBehaviour
{
    private Animator m_Animator;

    #region MonoBehaviour methods

    void Awake()
    {
        this.m_Animator = this.GetComponent<Animator>();
    }

    #endregion

    #region private methods

    private void SetBool(int id, bool value = true)
    {
        this.m_Animator.SetBool(id, value);
    }

    private void ResetAllTrigger()
    {
        List<string> nameList = new List<string>()
        {
            AnimatorConst.Attack,
            AnimatorConst.Skill1,
        };
        nameList.ForEach(a => this.ResetTrigger(a));
    }

    private void ResetTrigger(string name)
    {
        this.m_Animator.ResetTrigger(name);
    }

    #endregion

    #region public methods

    public void Idle()
    {
        this.SetBool(AnimatorConst.IdleHashID);
    }

    public void Attack()
    {
        this.SetBool(AnimatorConst.AttackHashID);
    }

    public void Hurt()
    {
        this.SetBool(AnimatorConst.HurtHashID);
    }

    public void Dead()
    {
        this.m_Animator.Play(AnimatorConst.DeadHashID);
    }

    public void Skill(int skillIndex)
    {
        this.SetBool(AnimatorConst.SkillDic[skillIndex]);
    }
    public bool IsCompleteAttackAnimation()
    {
        if (this.m_Animator.IsInTransition(0)) return false;
        AnimatorStateInfo info = this.m_Animator.GetCurrentAnimatorStateInfo(0);
        return info.IsName(AnimatorConst.Idle);
    }

    #endregion
}
