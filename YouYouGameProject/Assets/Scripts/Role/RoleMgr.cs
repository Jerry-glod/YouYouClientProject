using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMgr : Singleton<RoleMgr>
{
    private Dictionary<string, GameObject> m_Role = new Dictionary<string, GameObject>();
    /// <summary>
    /// 根据角色预设名称 克隆角色
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadPlayer(string name)
    {
        
        return null;
    }
}
