using System.Collections;
using System.Collections.Generic;using UnityEngine;

public class TestMMoMemory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //List<ProductEntity> lst = ProductDBModel.Instance.GetList();
        //for (int i = 0; i < lst.Count; i++)
        //{
        //    Debug.Log("name" + lst[i].Name);
        //}
        ProductEntity entity = ProductDBModel.Instance.Get(5);
        if (entity != null)
        {
            Debug.Log("name" + entity.Name);
        }
        //ProductDBModel.Instance.GetByName();//��չ

        JobEntity job = JobDBModel.Instance.Get(2);
        if (job != null)
        {
            Debug.Log("name" + job.Name);
        }
        //JobDBModel.Instance.GetByName();//��չ


        NetWorkSocket.Instance.Content("192.168.0.111", 1011);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Send("���A");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Send("���S");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < 10; i++)
            {
                Send("���ѭ��" + i);
            }
        }
    }
    private void Send(string msg)
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUTF8String(msg);
            NetWorkSocket.Instance.SendMsg(ms.ToArray());
        }
    }
}
