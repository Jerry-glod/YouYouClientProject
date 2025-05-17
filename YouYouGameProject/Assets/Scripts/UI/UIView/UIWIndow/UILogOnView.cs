using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登录UI视图
/// </summary>

public class UILogOnView : UIWindowViewBase
{
    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "btnLogOn":
                UIDispatcher.Instance.Dispatch(ConstDefine.UILogOnView_btnLogOn);
                break;  
            case "btnToReg":
                UIDispatcher.Instance.Dispatch(ConstDefine.UILogOnView_btnToReg);
                break;
        }
    }
}
