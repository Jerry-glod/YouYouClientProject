using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    
    
    /// <summary>
    /// 控制摄像机上下
    /// </summary>
    [SerializeField]
    private Transform m_CameraUpAndDown;
    /// <summary>
    /// 摄像机缩放父物体
    /// </summary>
    [SerializeField]
    private Transform m_CameraZoomContainer;
    /// <summary>
    /// 摄像机容器
    /// </summary>
    [SerializeField]
    private Transform m_CameraContainer;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        m_CameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUpAndDown.transform.localEulerAngles.z, 35f, 70f));
    }
    /// <summary>
    /// 相机旋转
    /// </summary>
    /// <param name="type">0=左 1=右</param>
    public void SetCameraRotate(int type)
    {
        transform.Rotate(0, 20 * Time.deltaTime * (type == 0 ? -1 : 1), 0);
    }
    /// <summary>
    /// 相机上下
    /// </summary>
    /// <param name="type">0=上 1=下</param>
    public void SetCameraUpAndDown(int type)
    {
        m_CameraUpAndDown.transform.Rotate(0, 0, 15 * Time.deltaTime * (type == 0 ? -1 : 1));
        m_CameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUpAndDown.transform.localEulerAngles.z, 35f, 70f));
    }
    /// <summary>
    /// 相机缩放
    /// </summary>
    /// <param name="type">0=大 1=小</param>
    public void SetCameraZoom(int type)
    {
        m_CameraContainer.Translate(Vector3.forward * 10 * Time.deltaTime * (type == 0 ? -1 : 1));
        m_CameraContainer.localPosition = new Vector3(0, 0, Mathf.Clamp(m_CameraContainer.localPosition.z, -5, 5));
    }
    /// <summary>
    /// 自动看目标
    /// </summary>
    /// <param name="pos"></param>
    public void AutoLookAt(Vector3 pos)
    {
        m_CameraZoomContainer.LookAt(pos);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 15f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 14f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 12f);


    }
}
