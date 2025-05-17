using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色状态的抽象基类
/// </summary>
public abstract class RoleStateAbstract 
{
    /// <summary>
    /// 当前角色有限状态机管理器
    /// </summary>
    public RoleFSMMgr curRoleFSMMgr { get; private set; }
    public AnimatorStateInfo currRoleAnimatorStateInfo { get; set; }
    public RoleStateAbstract(RoleFSMMgr roleFSMMgr)
    {
        curRoleFSMMgr = roleFSMMgr;
       
    }
    /// <summary>
    /// 进入状态
    /// </summary>
    public virtual void OnEnter() { }
    /// <summary>
    /// 执行状态
    /// </summary>
    public virtual void OnUpdate() { }
    /// <summary>
    /// 离开状态
    /// </summary>
    public virtual void OnLeave() { }
}
