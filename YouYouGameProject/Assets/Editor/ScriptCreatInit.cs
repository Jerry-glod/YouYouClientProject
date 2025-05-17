//===============================================
//±¸   ×¢£ºÌæ»»´úÂë×¢ÊÍ
//===============================================
using UnityEngine;
using System.IO;
using UnityEditor;
using System;

/// <summary>
/// 
/// </summary>
public class ScriptCreatInit : UnityEditor.AssetModificationProcessor
{
    private static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta","");
        if (path.EndsWith(".cs"))
        {
            string strContent = File.ReadAllText(path);
            strContent = strContent.Replace("#AuthorName#", "¹ù½ð±¦").Replace("#CreateTime#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            File.WriteAllText(path, strContent);
            AssetDatabase.Refresh();
        }
    }
}
