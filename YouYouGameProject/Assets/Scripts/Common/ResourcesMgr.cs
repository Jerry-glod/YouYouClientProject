using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ResourcesMgr:Singleton<ResourcesMgr>
{
    /// <summary>
    /// 资源类型
    /// </summary>
    public enum ResourceType
    { 
        /// <summary>
        /// 场景UI
        /// </summary>
        UIScene,
        /// <summary>
        /// 窗口
        /// </summary>
        UIWindow,
        /// <summary>
        /// 角色
        /// </summary>
        Role,
        /// <summary>
        /// 特效
        /// </summary>
        Effect,
    }
    private Hashtable m_PrefabTable;

    public ResourcesMgr()
    {
        m_PrefabTable = new Hashtable();
    }


    #region 加载资源
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="path">短路径</param>
    /// <param name="cache">是否放入缓存</param>
    /// <returns>预设克隆体</returns>
    public GameObject Load(ResourceType type, string path, bool cache)
    {
        
        GameObject obj = null;
        if (m_PrefabTable.Contains(path))
        {
            obj = m_PrefabTable[path] as GameObject;
        }
        else
        {
            StringBuilder sbr = new StringBuilder();
            switch (type)
            {
                case ResourceType.UIScene:
                    sbr.Append("UIPrefab/UIScene/");
                    break;
                case ResourceType.UIWindow:
                    sbr.Append("UIPrefab/UIWindows/");
                    break;
                case ResourceType.Role:
                    sbr.Append("UIPrefab/Role/");
                    break;
                case ResourceType.Effect:
                    sbr.Append("UIPrefab/Effect/");
                    break;
            }
            sbr.Append(path);
            obj = Resources.Load(sbr.ToString()) as GameObject;
            if (cache)
            {
                Debug.Log("资源是从缓存中加载的");
                m_PrefabTable.Add(path, obj);
            }
        }
        return GameObject.Instantiate(obj);
    }
    #endregion

    public override void Dispose()
    {
        base.Dispose();
        m_PrefabTable.Clear();
        //把未使用的资源进行释放
        Resources.UnloadUnusedAssets();
    }
}
