using UnityEditor;
using System.IO;

public class AutoSetScriptHead : UnityEditor.AssetModificationProcessor
{
    //������Դ������Դʱ�����
    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (!path.EndsWith(".cs"))
        {
            return;
        }
        string showMes = "//" +
            "***********************************************************\r\n"
            + "// ������\r\n"
            + "// ���ߣ����� \r\n"
            + "// ����ʱ�䣺#CreateTime#\r\n"
            + "// ��ע��\r\n"
            + "//"
            + "***********************************************************\r\n";
        showMes += File.ReadAllText(path);
        showMes = showMes.Replace("#CreateTime#", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        File.WriteAllText(path, showMes);

    }
}