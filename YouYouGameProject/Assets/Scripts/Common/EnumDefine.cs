using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumDefine
{ 
}
#region 场景类型
/// <summary>
/// 场景类型
/// </summary>
public enum SceneUIType
{
    /// <summary>
    /// 登录窗口
    /// </summary>
    LogOn,
    /// <summary>
    /// 注册窗口
    /// </summary>
    Loading,
    /// <summary>
    /// 主城
    /// </summary>
    MainCity,
}
#endregion
#region 窗口类型
/// <summary>
/// 窗口类型
/// </summary>
public enum WindowUIType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,
    /// <summary>
    /// 登录窗口
    /// </summary>
    LogOn,
    /// <summary>
    /// 注册窗口
    /// </summary>
    Reg,
}
#endregion


#region UI容器类型
/// <summary>
/// UI容器类型
/// </summary>
public enum WindowUIContainerType
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
    Center,
}
#endregion

public enum WindowShowStyle
{
    /// <summary>
    /// 正常打开
    /// </summary>
    Normal,
    /// <summary>
    /// 从中间打开
    /// </summary>
    CenterToBig,
    /// <summary>
    /// 从上往下
    /// </summary>
    FormTop,
    /// <summary>
    /// 从下往上
    /// </summary>
    FromDown,
    /// <summary>
    /// 从左往右
    /// </summary>
    FromLeft,
    /// <summary>
    /// 从右往左
    /// </summary>
    FromRight,
}
#region 角色类型
public enum RoleType
{
    None=0,
    /// <summary>
    /// 当前玩家
    /// </summary>
    MainPlayer = 1,
    /// <summary>
    /// 怪
    /// </summary>
    Monster = 2,
}
#endregion
public enum RoleState
{
    /// <summary>
    /// 未设置
    /// </summary>
    None=0,
    /// <summary>
    /// 待机
    /// </summary>
    Idle=1,
    /// <summary>
    /// 跑
    /// </summary>
    Run=2,
    /// <summary>
    /// 攻击
    /// </summary>
    Attack=3,
    /// <summary>
    /// 受伤
    /// </summary>
    Hurt=4,
    /// <summary>
    /// 死亡
    /// </summary>
    Die=5,

}
public enum RoleAnimatorName
{
    Idle_Normal,
    Idle_Fight,
    Run,
    Hurt,
    Die,
    PhyAttack1,
    PhyAttack2,
    PhyAttack3,
}
public enum ToAnimatorCondition
{
    ToIdleNormal,
    ToIdleFight,
    ToRun,
    ToDie,
    ToHurt,
    ToPhyAttack,
    CurrState,
}