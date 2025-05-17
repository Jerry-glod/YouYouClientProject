using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleFSMMgr 
{
    public RoleController CurRoleCtrl { get;private set; }
    /// <summary>
    /// 当前角色状态枚举
    /// </summary>
    public RoleState CurRoleStateEnum { get; private set; }
    /// <summary>
    /// 当前角色状态
    /// </summary>
    public RoleStateAbstract m_CurRoleState = null;
    
    public Dictionary<RoleState, RoleStateAbstract> m_RoleStateDic;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="curRoleCtrl"></param>
    public RoleFSMMgr(RoleController curRoleCtrl)
    {
        CurRoleCtrl = curRoleCtrl;
        m_RoleStateDic = new Dictionary<RoleState, RoleStateAbstract>();
        m_RoleStateDic[RoleState.Idle] = new RoleStateIdle(this);
        m_RoleStateDic[RoleState.Attack] = new RoleStateAttack(this);
        m_RoleStateDic[RoleState.Die] = new RoleStateDie(this);
        m_RoleStateDic[RoleState.Hurt] = new RoleStateHurt(this);
        m_RoleStateDic[RoleState.Run] = new RoleStateRun(this);
        if (m_RoleStateDic.ContainsKey(CurRoleStateEnum))
        {
            m_CurRoleState = m_RoleStateDic[CurRoleStateEnum];

        }
    }
    public void OnUpdate()
    {
        if (m_CurRoleState != null)
        {
            m_CurRoleState.OnUpdate();  
        }
    }
    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="newState">新的状态</param>
    public void ChangeState(RoleState newState)
    {
        if (CurRoleStateEnum == newState)
            return;
        //调用之前的状态的离开方法
        if (m_CurRoleState != null)
            m_CurRoleState.OnLeave();
        //更改当前状态枚举
        CurRoleStateEnum = newState;
        //更改当前状态
        m_CurRoleState = m_RoleStateDic[newState];
        //调用新状态的进入方法
        m_CurRoleState.OnEnter();
    }
}
