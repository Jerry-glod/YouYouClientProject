using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateAttack : RoleStateAbstract
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMgr">有限状态机管理器</param>
    public RoleStateAttack(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {
        
    }

    /// <summary>
    /// 实现基类 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        curRoleFSMMgr.CurRoleCtrl.animator.SetInteger(ToAnimatorCondition.ToPhyAttack.ToString(), 1);
    }
    /// <summary>
    /// 实现基类 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        curRoleFSMMgr.CurRoleCtrl.animator.SetInteger(ToAnimatorCondition.ToPhyAttack.ToString(), 0);
    }
    /// <summary>
    /// 实现基类 执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        currRoleAnimatorStateInfo = curRoleFSMMgr.CurRoleCtrl.animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.PhyAttack1.ToString()))
        {
            curRoleFSMMgr.CurRoleCtrl.animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleState.Attack);
            //如果动画执行一遍，切换待机
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                curRoleFSMMgr.CurRoleCtrl.ToIdle();
            }
        }
    }
    
}
