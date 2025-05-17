using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneUIMgr : Singleton<SceneUIMgr>
{
    public UISceneViewBase CurrentUIScene;

    #region 打开窗口
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="type">窗口类型</param>
    /// <returns></returns>
    public GameObject LoadSceneUI(SceneUIType type)
    {
        GameObject obj = null;
        switch (type)
        {
            case SceneUIType.LogOn:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIScene, "UI_Root_LogOn", false);
                CurrentUIScene = obj.GetComponent<UISceneLogOnView>();
                break;
            case SceneUIType.Loading:
                break;
            case SceneUIType.MainCity:
                break;
            default:
                break;
        }
        return obj;
    }
    #endregion
}
