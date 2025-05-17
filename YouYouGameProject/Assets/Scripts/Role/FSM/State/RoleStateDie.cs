using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateDie : RoleStateAbstract
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMgr">有限状态机管理器</param>
    public RoleStateDie(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }

    /// <summary>
    /// 实现基类 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("死亡");
        curRoleFSMMgr.CurRoleCtrl.animator.SetBool(ToAnimatorCondition.ToDie.ToString(), true);
    }
    /// <summary>
    /// 实现基类 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        curRoleFSMMgr.CurRoleCtrl.animator.SetBool(ToAnimatorCondition.ToDie.ToString(), false);
    }
    /// <summary>
    /// 实现基类 执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        currRoleAnimatorStateInfo = curRoleFSMMgr.CurRoleCtrl.animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Die.ToString()))
        {
            curRoleFSMMgr.CurRoleCtrl.animator.SetInteger(ToAnimatorCondition.ToDie.ToString(), (int)RoleState.Die);

        }
    }
}
