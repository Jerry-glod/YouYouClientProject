using UnityEditor;
using System.IO;

public class AutoSetScriptHead : UnityEditor.AssetModificationProcessor
{
    //导入资源创建资源时候调用
    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (!path.EndsWith(".cs"))
        {
            return;
        }
        string showMes = "//" +
            "***********************************************************\r\n"
            + "// 描述：\r\n"
            + "// 作者：郭金宝 \r\n"
            + "// 创建时间：#CreateTime#\r\n"
            + "// 备注：\r\n"
            + "//"
            + "***********************************************************\r\n";
        showMes += File.ReadAllText(path);
        showMes = showMes.Replace("#CreateTime#", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        File.WriteAllText(path, showMes);

    }
}