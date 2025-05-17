using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class AssetBundleDAL
{
    /// <summary>
    /// xml路径
    /// </summary>
    private string m_Path;
    /// <summary>
    /// 返回的数据集合
    /// </summary>
    private List<AssetBundleEntity> m_List = null;

    public AssetBundleDAL(string path)
    {
        m_Path = path;
        m_List = new List<AssetBundleEntity>();
    }
    /// <summary>
    /// 返回XML 数据
    /// </summary>
    /// <returns></returns>
    public List<AssetBundleEntity> GetList()
    {
        m_List.Clear();
        try
        {
            //读取XML 把数据添加到m_List里边
            XDocument xDoc = XDocument.Load(m_Path);
            XElement root = xDoc.Root;

            XElement assetBundleNode = root.Element("AssetBundle");

            IEnumerable<XElement> lst = assetBundleNode.Elements("Item");

            int index = 0;
            foreach (XElement item in lst)
            {
                AssetBundleEntity entity = new AssetBundleEntity();
                entity.Key = "Key" + ++index;
                entity.Name = item.Attribute("Name").Value;
                entity.Tag = item.Attribute("Tag").Value;
                entity.Version = item.Attribute("Version").Value.ToInt();
                entity.Size = item.Attribute("Size").Value.ToLong();
                entity.ToPath = item.Attribute("ToPath").Value;

                Debug.Log(entity.Name);
                IEnumerable<XElement> pathList = item.Elements("Path");

                foreach (XElement path in pathList)
                {
                    //entity.PathList.Add(string.Format("Assets/0" + path.Attribute("Value").Value));
                    entity.PathList.Add(Path.Combine("Assets", path.Attribute("Value").Value)); // 使用Path.Combine
                    //entity.PathList.Add(Path.Combine("Assets", path.Attribute("Value").Value.Replace('\\', '/')));

                }
                m_List.Add(entity);
            }
            Debug.Log($"成功加载 {m_List.Count} 个AssetBundle配置项");
        }
        catch (Exception ex)
        {
            Debug.LogError($"加载XML文件失败: {ex.Message}");
        }

        return m_List;
    }
}
