using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 账号系统控制器
/// </summary>
public class AccountCtrl:Singleton<AccountCtrl>,ISystemCtrl
{
    /// <summary>
    /// 登录窗口试图
    /// </summary>
    private UILogOnView m_LogOnView;
    /// <summary>
    /// 注册窗口视图
    /// </summary>
    private UIRegView m_RegView;

    /// <summary>
    /// 构造函数
    /// </summary>
    public AccountCtrl()
    {
        UIDispatcher.Instance.AddEventListener(ConstDefine.UILogOnView_btnLogOn, LogOnViewBtnLogOnClick);
        UIDispatcher.Instance.AddEventListener(ConstDefine.UILogOnView_btnToReg, LogOnViewBtnToRegOnClick);

        UIDispatcher.Instance.AddEventListener(ConstDefine.UIRegView_btnReg, RegViewBtnRegClick);
        UIDispatcher.Instance.AddEventListener(ConstDefine.UIRegView_btnToLogOn, RegViewBtnToLogOnClick);
    }
    /// <summary>
    /// 注册视图 注册按钮点击
    /// </summary>
    /// <param name="param"></param>
    private void RegViewBtnRegClick(object[] param)
    {
        UIViewUtil.Instance.OpenWindow(WindowUIType.Reg);
    }
    /// <summary>
    /// 注册视图 返回登录按钮点击
    /// </summary>
    /// <param name="param"></param>
    private void RegViewBtnToLogOnClick(object[] param)
    {
        m_RegView.Close(true);
    }

    /// <summary>
    /// 登陆视图 登录按钮点击
    /// </summary>
    /// <param name="param"></param>
    private void LogOnViewBtnLogOnClick(object[] param)
    {
        Debug.Log("登录按钮点击");
        UIViewUtil.Instance.OpenWindow(WindowUIType.LogOn);
    }
    /// <summary>
    /// 登录视图 去注册按钮点击
    /// </summary>
    /// <param name="param"></param>
    private void LogOnViewBtnToRegOnClick(object[] param)
    { 
        Debug.Log("去注册按钮点击");
        m_LogOnView.Close(true);
    }

    /// <summary>
    /// 打开注册视图
    /// </summary>
    public void OpenRegView()
    {
        m_RegView = UIViewUtil.Instance.OpenWindow(WindowUIType.Reg).GetComponent<UIRegView>();
        m_RegView.OnViewClose = () => {
            OpenLogOnView();
        };
    }
    /// <summary>
    /// 打开登录视图
    /// </summary>
    public void OpenLogOnView()
    {
        m_LogOnView= UIViewUtil.Instance.OpenWindow(WindowUIType.LogOn).GetComponent<UILogOnView>();
        m_LogOnView.OnViewClose=() => {
            OpenRegView();
        };
    }

    public void OpenView(WindowUIType type)
    {
        switch (type)
        {
            case WindowUIType.LogOn:
                OpenLogOnView();
                break;
            case WindowUIType.Reg:
                OpenRegView();
                break;
        }
    }
    /// <summary>
    /// 释放
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();
        UIDispatcher.Instance.RemoveEventListener(ConstDefine.UILogOnView_btnLogOn, LogOnViewBtnLogOnClick);
        UIDispatcher.Instance.RemoveEventListener(ConstDefine.UILogOnView_btnToReg, LogOnViewBtnToRegOnClick);

        UIDispatcher.Instance.RemoveEventListener(ConstDefine.UIRegView_btnReg, RegViewBtnRegClick);
        UIDispatcher.Instance.RemoveEventListener(ConstDefine.UIRegView_btnToLogOn, RegViewBtnToLogOnClick);
    }
}
