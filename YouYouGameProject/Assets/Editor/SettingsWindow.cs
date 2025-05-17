using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SettingsWindow : EditorWindow
{
    private List<MacorItem> m_List = new List<MacorItem>();
    private Dictionary<string, bool> m_Dic = new Dictionary<string, bool>();
    private string m_Macro = null;

    private void OnEnable()
    {
        // 初始化列表和字典
        m_List = new List<MacorItem>
        {
            new MacorItem { Name = "Debug_MODEL", DisPlayName = "调试模式", IsDebug = true, IsRelease = false },
            new MacorItem { Name = "Debug_LOG", DisPlayName = "打印日志", IsDebug = true, IsRelease = false },
            new MacorItem { Name = "STAT_TD", DisPlayName = "开启统计", IsDebug = false, IsRelease = true }
        };
        m_Dic = new Dictionary<string, bool>();

        // 安全获取宏定义
        m_Macro = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        // 初始化字典状态
        foreach (var item in m_List)
        {
            bool isActive = !string.IsNullOrEmpty(m_Macro) && m_Macro.Contains(item.Name);
            m_Dic[item.Name] = isActive;
        }
    }
   
    private void OnGUI()
    {
        for (int i = 0; i < m_List.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            m_Dic[m_List[i].Name] = GUILayout.Toggle(m_Dic[m_List[i].Name], m_List[i].DisPlayName);
            EditorGUILayout.EndHorizontal();

        }
        //开启一行
        EditorGUILayout.BeginHorizontal("box");
        if (GUILayout.Button("保存", GUILayout.Width(100)))
        {
            SaveMacro();
        }
  

        if (GUILayout.Button("调试模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsDebug;
            }
            SaveMacro();
        }
        if (GUILayout.Button("发布模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsDebug;
            }
            SaveMacro();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void SaveMacro()
    {
        m_Macro = string.Empty;
        foreach (var item in m_Dic)
        {
            if (item.Value)
            {
                m_Macro += string.Format("{0};",item.Key);
            }
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, m_Macro);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_Macro);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_Macro);

    }

    public class MacorItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 显示的名称
        /// </summary>
        public string DisPlayName;
        /// <summary>
        /// 是否调试
        /// </summary>
        public bool IsDebug;
        /// <summary>
        /// 是否发布项
        /// </summary>
        public bool IsRelease;
    }
}
