using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Menu
{
    [MenuItem("YouYouTools/Settings")]
    public static void Settings()
    {
        SettingsWindow win = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow));
        win.titleContent = new GUIContent("全局设置");
        win.Show();
    }
    [MenuItem("YouYouTools/AssetsBundleCreate")]
    public static void AssetsBundleCreate()
    {

        AssetBundleWindow win = EditorWindow.GetWindow<AssetBundleWindow>();
        win.titleContent = new GUIContent("资源打包");
        win.Show();

        //string xmlPath = Application.dataPath + @"\Editor\AssetBundle\AssetBundleConfig.xml";
        //AssetBundleDAL dal = new AssetBundleDAL(xmlPath);
        //List<AssetBundleEntity> lst = dal.GetList();
        //foreach (var item in lst)
        //{
        //    Debug.LogFormat(item.Name);
        //}
    }
}
