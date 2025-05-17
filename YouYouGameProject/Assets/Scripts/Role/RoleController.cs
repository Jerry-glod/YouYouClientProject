using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色控制器
/// </summary>
public class RoleController : MonoBehaviour
{
    private CharacterController m_characterController;
    private Vector3 m_TargetPos = Vector3.zero;
    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField]
    private float m_Speed = 10f;
    /// <summary>
    /// 旋转速度
    /// </summary>
    [SerializeField]
    private float m_RotationSpeed = 0.2f;
    /// <summary>
    /// 转身的目标方向
    /// </summary>
    private Quaternion m_TargetQuaternion;
    /// <summary>
    /// 是否转身完成
    /// </summary>
    private bool m_RotationOver = false;
    /// <summary>
    /// 动画
    /// </summary>
    [SerializeField]
    public Animator animator;
    /// <summary>
    /// 角色类型
    /// </summary>
    public RoleType curRoleType = RoleType.None;
    /// <summary>
    /// 角色信息
    /// </summary>
    public RoleInfoBase curRoleInfo = null;
    /// <summary>
    /// 当前角色AI
    /// </summary>
    public IRoleAI curRoleAI = null;
    /// <summary>
    /// 当前角色有限状态机管理器
    /// </summary>
    public RoleFSMMgr curRoleFSMMgr = null;


    public void Init(RoleType roleType, RoleInfoBase roleInfo, IRoleAI ai)
    {
        curRoleType = roleType;
        curRoleInfo = roleInfo;
        curRoleAI = ai;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        if (CameraController.Instance != null)
        {
            CameraController.Instance.Init();
        }
        if (FingerEvent.Instance != null)
        {
            FingerEvent.Instance.OnFingerDrag += OnFingerDrag;
            FingerEvent.Instance.OnPlayerClickGround += OnPlayerClickGround;
            FingerEvent.Instance.OnZoom += OnZoom;
        }
        curRoleFSMMgr = new RoleFSMMgr(this);
    }
    // Update is called once per frame
    void Update()
    {
        //if (curRoleAI == null)
        //    return;
        //curRoleAI.DoAI();
        if (curRoleFSMMgr != null)
            curRoleFSMMgr.OnUpdate();
        if (m_characterController == null)
            return;

        //让角色贴地
        if (!m_characterController.isGrounded)
        {
            m_characterController.Move((transform.position + new Vector3(0, -1000, 0)) - transform.position);
        }
        //如果目标点不是原点，则移动
        if (m_TargetPos != Vector3.zero)
        {

            //Debug.DrawLine(Camera.main.transform.position, m_TargetPos);

            if (Vector3.Distance(m_TargetPos, transform.position) > 0.1f)
            {
                //transform.LookAt(m_TargetPos);
                //transform.Translate(Vector3.forward * m_Speed * Time.deltaTime);

                Vector3 direction = m_TargetPos - transform.position;
                direction = direction.normalized;//归一化
                direction = direction * Time.deltaTime * m_Speed;
                direction.y = 0;

                //transform.LookAt(new Vector3(m_TargetPos.x,transform.position.y,m_TargetPos.z));
                //角色缓慢转身
                if (m_RotationSpeed <= 1)
                {
                    m_RotationSpeed += 5f * Time.deltaTime;
                    m_TargetQuaternion = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_TargetQuaternion, m_RotationSpeed);
                    //if (Quaternion.Angle(m_TargetQuaternion, transform.rotation) < 1f)
                    //{
                    //    m_RotationSpeed = 1;
                    //    m_RotationOver = true;
                    //}
                }
                m_characterController.Move(direction);
            }
        }
        //AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        //if (info.IsName("Idle_Normal"))
        //{
        //    animator.SetInteger("CurrentState", 1);

        //}
        //else if (info.IsName("Idle_Normal"))
        //{
        //    animator.SetInteger("CurrentState", 2);
        //}
        CameraAutoFoller();

        if (Input.GetKeyDown(KeyCode.U))
        {
            ToRun();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ToAttack();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            ToHurt();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ToDie();
        }
    }

    #region 角色控制
    public void ToIdle()
    {
        curRoleFSMMgr.ChangeState(RoleState.Idle);
    }
    public void ToRun()
    {
        Debug.Log("进入跑的状态");
        curRoleFSMMgr.ChangeState(RoleState.Run);
    }
    public void ToAttack()
    {
        curRoleFSMMgr.ChangeState(RoleState.Attack);
    }
    public void ToHurt()
    {
        curRoleFSMMgr.ChangeState(RoleState.Hurt);
    }
    public void ToDie()
    {
        curRoleFSMMgr.ChangeState(RoleState.Die);
    }
    #endregion

    #region 销毁
    /// <summary>
    /// 销毁
    /// </summary>
    private void OnDestroy()
    {
        FingerEvent.Instance.OnZoom -= OnZoom;
        FingerEvent.Instance.OnFingerDrag -= OnFingerDrag;
        FingerEvent.Instance.OnPlayerClickGround -= OnPlayerClickGround;

    }
    #endregion



    #region 摄像机缩放
    /// <summary>
    /// 摄像机缩放
    /// </summary>
    /// <param name="obj"></param>
    private void OnZoom(FingerEvent.ZoomType obj)
    {
        switch (obj)
        {
            case FingerEvent.ZoomType.In:
                CameraController.Instance.SetCameraZoom(0);
                break;
            case FingerEvent.ZoomType.Out:
                CameraController.Instance.SetCameraZoom(1);
                break;
            default:
                break;
        }
    }
    #endregion

    #region 玩家点击地面
    /// <summary>
    /// 玩家点击地面
    /// </summary>
    private void OnPlayerClickGround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.name.Equals("Ground", System.StringComparison.CurrentCultureIgnoreCase))
            {
                m_TargetPos = hitInfo.point;
                m_RotationOver = false;
                m_RotationSpeed = 0;
                Debug.Log(m_TargetPos);
            }
        }
    }
    #endregion

    #region 手指滑动
    private void OnFingerDrag(FingerEvent.FingerDir obj)
    {
        switch (obj)
        {
            case FingerEvent.FingerDir.Up:
                CameraController.Instance.SetCameraUpAndDown(1);
                break;
            case FingerEvent.FingerDir.Down:
                CameraController.Instance.SetCameraUpAndDown(0);
                break;
            case FingerEvent.FingerDir.Left:
                CameraController.Instance.SetCameraRotate(1);
                break;
            case FingerEvent.FingerDir.Right:
                CameraController.Instance.SetCameraRotate(0);
                break;
            default:
                break;
        }
    }
    #endregion

    #region 相机跟随
    private void CameraAutoFoller()
    {
        if (CameraController.Instance == null) return;
        CameraController.Instance.transform.position = gameObject.transform.position;
        CameraController.Instance.AutoLookAt(gameObject.transform.position);


        if (Input.GetKey(KeyCode.A))
        {
            CameraController.Instance.SetCameraRotate(1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            CameraController.Instance.SetCameraRotate(0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            CameraController.Instance.SetCameraUpAndDown(1);
        }
        if (Input.GetKey(KeyCode.S))
        {

            CameraController.Instance.SetCameraUpAndDown(0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            CameraController.Instance.SetCameraZoom(0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            CameraController.Instance.SetCameraZoom(1);
        }
    }
    #endregion


}
