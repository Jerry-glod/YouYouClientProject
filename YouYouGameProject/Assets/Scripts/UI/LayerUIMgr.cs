using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI层级管理器
/// </summary>
public class LayerUIMgr : Singleton<LayerUIMgr>
{
    private int m_UIViewLayer=50;
    /// <summary>
    ///  检查窗口数量 如果没有打开的窗口 重置
    /// </summary>
    public void Reset()
    {
        m_UIViewLayer = 50;
    }
    /// <summary>
    /// 检查窗口数量 如果没有打开的窗口 重置
    /// </summary>
    public void CheckOpenWindow()
    {
        m_UIViewLayer--;
        if (UIViewUtil.Instance.OpenWindowCount == 0)
        {
            Reset();
        }
    }
    /// <summary>
    /// 设置层级
    /// </summary>
    /// <param name="obj"></param>
    public void SetLayer(GameObject obj)
    {
        m_UIViewLayer++;
        Canvas m_canvas = obj.GetComponent<Canvas>();
        m_canvas.sortingOrder = m_UIViewLayer;
    }
  
}
