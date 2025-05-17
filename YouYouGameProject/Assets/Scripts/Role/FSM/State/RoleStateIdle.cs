using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateIdle : RoleStateAbstract
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMgr">有限状态机管理器</param>
    public RoleStateIdle(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {
        
    }

    /// <summary>
    /// 实现基类 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        curRoleFSMMgr.CurRoleCtrl.animator.SetBool(ToAnimatorCondition.ToIdleNormal.ToString(), true);
        

    }
    /// <summary>
    /// 实现基类 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        curRoleFSMMgr.CurRoleCtrl.animator.SetBool(ToAnimatorCondition.ToIdleNormal.ToString(), false);
    }
    /// <summary>
    /// 实现基类 执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        currRoleAnimatorStateInfo  = curRoleFSMMgr.CurRoleCtrl.animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Idle_Normal.ToString()))
        {
            curRoleFSMMgr.CurRoleCtrl.animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleState.Idle);
        }
    }
}
