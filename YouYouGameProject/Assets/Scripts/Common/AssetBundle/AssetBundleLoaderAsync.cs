using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class AssetBundleLoaderAsync : MonoBehaviour
{
    private string m_FullPath;
    private string m_Name;
    private AssetBundleCreateRequest request;
    private AssetBundle bundle;
    public System.Action<Object> OnLoadComplete;
    public void Init(string path,string name)
    {
        m_FullPath = LocalFileMgr.Instance.LocalFilePath + path;
        m_Name = name;
    }
    private void Start()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        request = AssetBundle.LoadFromMemoryAsync(LocalFileMgr.Instance.GetBuffer(m_FullPath));
        yield return request;
        bundle = request.assetBundle;
        if (OnLoadComplete != null)
        {
            OnLoadComplete(bundle.LoadAsset(m_Name));
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (bundle != null)
        {
            bundle.Unload(false);
            m_FullPath = null;
        }
    }
}
