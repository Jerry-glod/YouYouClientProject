using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalFileMgr : Singleton<LocalFileMgr>
{
#if UNITY_EDITOR
#if UNITY_STANDALONE_WIN
    public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/Windows";
#elif UNITY_ANDROID
    public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/Android";
#elif UNITY_IPHONE
    public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/iOS";

#endif
#elif UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_WIN
    public readonly string LocalFilePath = Application.persistentDataPath + "/../AssetBundles/iOS";
#endif

    public byte[] GetBuffer(string path)
    {
        byte[] buffer = null;
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }
}
