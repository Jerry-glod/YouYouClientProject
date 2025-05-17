using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateRun : RoleStateAbstract
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMgr">有限状态机管理器</param>
    public RoleStateRun(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }

    /// <summary>
    /// 实现基类 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("我在跑");
        curRoleFSMMgr.CurRoleCtrl.animator.SetBool(ToAnimatorCondition.ToRun.ToString(), true);
    }
    /// <summary>
    /// 实现基类 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        curRoleFSMMgr.CurRoleCtrl.animator.SetBool(ToAnimatorCondition.ToRun.ToString(), false);
    }
    /// <summary>
    /// 实现基类 执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        currRoleAnimatorStateInfo = curRoleFSMMgr.CurRoleCtrl.animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Run.ToString()))
        {
            curRoleFSMMgr.CurRoleCtrl.animator.SetInteger(ToAnimatorCondition.ToRun.ToString(), (int)RoleState.Run);
        }
        else
        {
            curRoleFSMMgr.CurRoleCtrl.animator.SetInteger(ToAnimatorCondition.ToRun.ToString(), 0);
        }
    }
}
