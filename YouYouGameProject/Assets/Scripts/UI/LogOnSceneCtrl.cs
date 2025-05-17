using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOnSceneCtrl : MonoBehaviour
{
    GameObject obj;
    private void Awake()
    {
        SceneUIMgr.Instance.LoadSceneUI(SceneUIType.LogOn);
    }
    private void Start()
    {
        Debug.Log("dataPath=" + Application.dataPath);
        Debug.Log("persistentDataPath=" + Application.persistentDataPath);
        Debug.Log("streamingAssetsPath=" + Application.streamingAssetsPath);
        Debug.Log("temporaryCachePath=" + Application.temporaryCachePath);
    }
}
