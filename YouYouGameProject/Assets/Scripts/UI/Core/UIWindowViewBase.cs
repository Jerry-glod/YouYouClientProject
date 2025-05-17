using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowViewBase : UIViewBase
{
    /// <summary>
    /// 挂点类型
    /// </summary>
    [SerializeField]
    public WindowUIContainerType containerType = WindowUIContainerType.Center;
    /// <summary>
    /// 打开方式
    /// </summary>
    [SerializeField]
    public WindowShowStyle showStyle = WindowShowStyle.Normal;
    /// <summary>
    /// 打开或关闭动画效果持续时间
    /// </summary>
    [SerializeField]
    public float duration = 0.2f;
    /// <summary>
    /// 当前窗口类型
    /// </summary>
    [HideInInspector]
    public WindowUIType CurrentUIType;
    /// <summary>
    /// 是否会有下个窗口
    /// </summary>
    private bool m_OpenNext = false;
    /// <summary>
    /// 视图关闭后的委托
    /// </summary>
    public Action OnViewClose;

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        if (go.name.Equals("btnClose", StringComparison.CurrentCultureIgnoreCase))
        {
            Debug.Log(go.name);
            Close(false);
        }
    }
    /// <summary>
    /// 关闭窗口
    /// </summary>
    public virtual void Close(bool openNext)
    {
        m_OpenNext = openNext;
        UIViewUtil.Instance.CloseWindow(CurrentUIType);
    }

    /// <summary>
    /// 销毁之前执行
    /// </summary>
    protected override void BeforeOnDestory()
    {
        LayerUIMgr.Instance.CheckOpenWindow();
        if (m_OpenNext)
        {
            if (OnViewClose != null)
            {
                OnViewClose();
            }
        }
      
    }
}
