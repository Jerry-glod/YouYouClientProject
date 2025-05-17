using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 所有UI视图的基类
/// </summary>

public class UIViewBase : MonoBehaviour
{
    private void Awake()
    {
        OnAwake();
    }
    void Start()
    {
        Button[] btnArr = GetComponentsInChildren<Button>(true);
        for (int i = 0; i < btnArr.Length; i++)
        {
            EventTriggerListener.Get(btnArr[i].gameObject).onClick = BtnClick;
        }
        OnStart();
    }
    private void OnDestroy()
    {
        BeforeOnDestory();
    }
    private void BtnClick(GameObject go)
    {
        OnBtnClick(go);
    }
               
    protected virtual void OnAwake() { }

    protected virtual void OnStart() { }
    protected virtual void BeforeOnDestory() { }
    protected virtual void OnBtnClick(GameObject go) { }
}
