using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登录场景UI视图
/// </summary>
public class UISceneLogOnView : UISceneViewBase 
{
    protected override void OnStart()
    {
        base.OnStart();
        StartCoroutine(OpenLogOnWindow());
    }

    private IEnumerator OpenLogOnWindow()
    {
        yield return new WaitForSeconds(0.2f);
        UIViewMgr.Instance.OpenWindow(WindowUIType.LogOn);
        
    }
}
