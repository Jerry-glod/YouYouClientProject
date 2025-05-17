using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleWindow : EditorWindow
{
    private AssetBundleDAL dal;
    private List<AssetBundleEntity> m_List;
    private Dictionary<string, bool> m_Dic;
    private string[] arrTag = { "All", "Scene", "Role", "Effect", "Audio", "None" };
    private int tagIndex = 0;
    private string[] arrBuildTarget = { "Windows", "Android", "iOS" };
    private BuildTarget target;
    private int buildTargetIndex = 0;
    private Vector2 pos;

    private void OnEnable()
    {
        // 动态获取当前平台
        target = EditorUserBuildSettings.activeBuildTarget;
        // 根据当前 BuildTarget 匹配 arrBuildTarget 中的平台名称
        switch (target)
        {
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                buildTargetIndex = 0; // 对应 "Windows"
                break;
            case BuildTarget.Android:
                buildTargetIndex = 1; // 对应 "Android"
                break;
            case BuildTarget.iOS:
                buildTargetIndex = 2; // 对应 "iOS"
                break;
            default:
                buildTargetIndex = 0; // 默认设为 Windows
                break;
        }
        buildTargetIndex = (int)target;

        // 初始化路径和加载数据
        //string xmlPath = Application.dataPath+ @"\Editor\AssetBundle\AssetBundleConfig.xml";

        string xmlPath = Path.Combine(Application.dataPath, "Editor", "AssetBundle", "AssetBundleConfig.xml");
        dal = new AssetBundleDAL(xmlPath);
        m_List = dal.GetList();
        m_Dic = new Dictionary<string, bool>();
        for (int i = 0; i < m_List.Count; i++)
        {
            m_Dic[m_List[i].Key] = true;
        }

    }

    /// <summary>
    /// 绘制窗口
    /// </summary>
    private void OnGUI()
    {
        if (m_List == null) return;

        #region 按钮行
        GUILayout.BeginHorizontal("box");
        tagIndex = EditorGUILayout.Popup(tagIndex, arrTag, GUILayout.Width(100));
        if (GUILayout.Button("选定Tag", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnSelectTagCallBack;
        }

        buildTargetIndex = EditorGUILayout.Popup(buildTargetIndex, arrBuildTarget, GUILayout.Width(100));
        if (GUILayout.Button("选定Target", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnSelectTargetCallBack;
        }

        if (GUILayout.Button("打AssetBundle包", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnAssetBundleCallBack;
        }

        if (GUILayout.Button("清空AssetBundle", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallBack;
        }
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();
        #endregion

        GUILayout.BeginHorizontal("box");
        GUILayout.Label("包名");
        GUILayout.Label("标记",GUILayout.Width(100));
        GUILayout.Label("保存路径", GUILayout.Width(200));
        GUILayout.Label("版本", GUILayout.Width(100));
        GUILayout.Label("大小", GUILayout.Width(100));
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();

        pos = EditorGUILayout.BeginScrollView(pos);

        for (int i = 0; i < m_List.Count; i++)
        {
            AssetBundleEntity entity = m_List[i];
            GUILayout.BeginHorizontal("box");

            m_Dic[entity.Key] = GUILayout.Toggle(m_Dic[entity.Key], "", GUILayout.Width(20));
            GUILayout.Label(entity.Name);
            GUILayout.Label(entity.Tag, GUILayout.Width(100));
            GUILayout.Label(entity.ToPath, GUILayout.Width(200));
            GUILayout.Label(entity.Version.ToString(), GUILayout.Width(100));
            GUILayout.Label(entity.Size.ToString(), GUILayout.Width(100));
            GUILayout.EndHorizontal();
            foreach (string path in entity.PathList)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Space(40);
                GUILayout.Label(path);
                GUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();
        GUILayout.EndVertical();


    }



    /// <summary>
    /// 选定Tag回调
    /// </summary>
    private void OnSelectTagCallBack()
    {
        switch (tagIndex)
        {
            case 0://全选
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = true;
                }
                break;
            case 1://Scene
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 2://Role
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Role", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 3://Effect
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Effect", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 4://Audio
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Audio", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 5://None
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = false;
                }
                break;
        }
        Debug.LogFormat("当前选中的Tag：{0}", arrTag[tagIndex]);
    }
    /// <summary>
    /// 选定Target回调
    /// </summary>
    private void OnSelectTargetCallBack()
    {
        switch (buildTargetIndex)
        {
            case 0:
                target = BuildTarget.StandaloneWindows;
                break;
            case 1:
                target = BuildTarget.Android;
                break;
            case 2:
                target = BuildTarget.iOS;
                break;
        }
        Debug.LogFormat("当前选中的buildTarget：{0}", arrBuildTarget[tagIndex]);

    }
    /// <summary>
    /// 打包回调
    /// </summary>
    private void OnAssetBundleCallBack()
    {
        List<AssetBundleEntity> lstNeedBuild = new List<AssetBundleEntity>();
        foreach (AssetBundleEntity entity in m_List)
        {
            if (m_Dic[entity.Key])
            {
                lstNeedBuild.Add(entity);
            }
        }
        for (int i = 0; i < lstNeedBuild.Count; i++)
        {
            Debug.LogFormat("正在打包{0}/{1}", i + 1, lstNeedBuild.Count);
            BuildAssetBundle(lstNeedBuild[i]);
        }
        Debug.Log("打包完毕");
    }
    /// <summary>
    /// 打包方法
    /// </summary>
    /// <param name="entity"></param>
    private void BuildAssetBundle(AssetBundleEntity entity)
    {
        AssetBundleBuild[] arrBuild = new AssetBundleBuild[1];
        AssetBundleBuild build = new AssetBundleBuild();
        //包名
        build.assetBundleName = $"{entity.Name}/{(entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase) ? "unity3d" : "assetbundle")}";
        //资源路径
        build.assetNames = entity.PathList.ToArray();
        arrBuild[0] = build;
        string platformSubDir = arrBuildTarget[buildTargetIndex];
        string basePath = Path.Combine(Application.dataPath, "..", "AssetBundle", platformSubDir);
        string toPath = Path.Combine(basePath, entity.ToPath.TrimStart('/'));
        //如果目标路径不存在 则创建
        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }
        BuildPipeline.BuildAssetBundles(toPath, arrBuild, BuildAssetBundleOptions.None, target);
    }
    /// <summary>
    /// 清空AssetBundle回调
    /// </summary>
    private void OnClearAssetBundleCallBack()
    {
        if (buildTargetIndex < 0 || buildTargetIndex >= arrBuildTarget.Length)
        {
            Debug.LogError("平台索引越界！");
            return;
        }

        string platformSubDir = arrBuildTarget[buildTargetIndex];
        string path = Path.Combine(Application.dataPath, "..", "AssetBundle", platformSubDir);
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        Debug.Log("清理完成");
    }
}
