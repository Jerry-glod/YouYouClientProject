﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRegView : UIWindowViewBase
{
    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "btnReg":
                UIDispatcher.Instance.Dispatch(ConstDefine.UIRegView_btnReg);
                break;
            case "btnToLogOn":
                UIDispatcher.Instance.Dispatch(ConstDefine.UIRegView_btnToLogOn);
                break;
           
        }
    }
}
