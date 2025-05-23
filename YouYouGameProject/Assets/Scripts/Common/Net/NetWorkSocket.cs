using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetWorkSocket : MonoBehaviour
{
    #region ����
    private static NetWorkSocket instance;
    public static NetWorkSocket Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                DontDestroyOnLoad(obj);
                instance = obj.GetOrCreatCompontent<NetWorkSocket>();
            }
            return instance;
        }
    }
    #endregion

    //private byte[] buffer = new byte[10240];
    /// <summary>
    /// ������Ϣ����
    /// </summary>
    private Queue<byte[]> m_SendQueue = new Queue<byte[]>();
    /// <summary>
    /// �����е�ί��
    /// </summary>
    private Action m_CheckSendueue;
    /// <summary>
    /// �ͻ���Socket
    /// </summary>
    private Socket m_Client;

    #region 接收消息所需变量
    /// <summary>
    /// 接收数据包的数组字节数组缓冲区
    /// </summary>
    private byte[] m_ReceiveBuffer = new byte[10240];
    /// <summary>
    /// 接收数据包的缓冲数据流
    /// </summary>
    private MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();

    /// <summary>
    /// 接收消息的队列
    /// </summary>
    private Queue<byte[]> m_ReceiveQueue = new Queue<byte[]>();

    private int m_ReceiveCount = 0;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region 从队列中获取数据
        while (true)
        {
            if (m_ReceiveCount <= 5)
            {
                m_ReceiveCount++;
                lock (m_ReceiveQueue)
                {
                    if (m_ReceiveQueue.Count > 0)
                    {
                        byte[] buffer = m_ReceiveQueue.Dequeue();
                        using (MMO_MemoryStream ms = new MMO_MemoryStream())
                        {
                            string msg = ms.ReadUTF8String();
                            Debug.Log(msg);
                        }
                        using (MMO_MemoryStream ms = new MMO_MemoryStream())
                        {
                            ms.WriteUTF8String("客户端时间" + DateTime.Now.ToString());
                            this.SendMsg(ms.ToArray());
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                m_ReceiveCount = 0;
                break;
            }
        }
        #endregion




    }
    private void OnDestroy()
    {
        if (m_Client != null && m_Client.Connected)
        {
            m_Client.Shutdown(SocketShutdown.Both);
            m_Client.Close();
        }
    }
    #region Content ���ӵ�socket������
    /// <summary>
    /// ���ӵ�socket������
    /// </summary>
    /// <param name="ip">ip</param>
    /// <param name="port">�˿�</param>
    public void Content(string ip, int port)
    {
        ///���socket �Ѿ����ڣ����Ҵ���������״̬����ֱ��return
        if (m_Client != null && m_Client.Connected)
        {
            return;
        }
        m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            m_Client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            m_CheckSendueue = OnCheckSendQueueCallBack;
            ReceiveMsg();
            Debug.Log("���ӳɹ�");
        }
        catch (Exception ex)
        {

            Debug.Log("����ʧ��=" + ex.Message);
        }
    }
    #endregion

    #region OnCheckSendQueueCallBack �����е�ί�лص�
    /// <summary>
    /// �����е�ί�лص�
    /// </summary>
    private void OnCheckSendQueueCallBack()
    {
        lock (m_SendQueue)
        {
            //��������������ݰ� �������ݰ�
            if (m_SendQueue.Count > 0)
            {
                //�������ݰ�
                Send(m_SendQueue.Dequeue());
            }
        }
    }
    #endregion

    #region ��װ���ݰ�
    /// <summary>
    /// ��װ���ݰ�
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private byte[] MakeBuffer(byte[] data)
    {
        byte[] retBuffer = null;
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort((ushort)data.Length);
            ms.Write(data, 0, data.Length);
            retBuffer = ms.ToArray();

        }
        return retBuffer;
    }
    #endregion

    #region SendMsg ������Ϣ ����Ϣ���뵽����
    /// <summary>
    /// ������Ϣ
    /// </summary>
    /// <param name="buffer"></param>
    public void SendMsg(byte[] buffer)
    {
        //�õ���װ������ݰ�
        byte[] sendBuffer = MakeBuffer(buffer);
        lock (m_SendQueue)
        {
            ///�����ݰ��������
            m_SendQueue.Enqueue(sendBuffer);
            ///���ί�� (ִ��ί��)
            m_CheckSendueue.BeginInvoke(null, null);
        }
    }
    #endregion

    #region Send �����������ݰ���������
    /// <summary>
    /// �����������ݰ���������
    /// </summary>
    /// <param name="buffer"></param>
    private void Send(byte[] buffer)
    {
        m_Client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallBack, m_Client);
    }
    #endregion

    #region SendCallBack �������ݰ��Ļص�
    /// <summary>
    /// �������ݰ��Ļص�
    /// </summary>
    /// <param name="ar"></param>
    private void SendCallBack(IAsyncResult ar)
    {
        m_Client.EndSend(ar);
        //����������
        OnCheckSendQueueCallBack();
    }
    #endregion

    //==========================================
    #region ReceiveMgr 接收数据
    /// <summary>
    /// 接收数据
    /// </summary>

    private void ReceiveMsg()
    {
        //异步接收数据
        m_Client.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallBack, m_Client);
    }

    #endregion

    #region ReceiveCallBack 接收数据回调
    /// <summary>
    /// 接收数据回调
    /// </summary>
    /// <param name="ar"></param>
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            int len = m_Client.EndReceive(ar);
            if (len > 0)
            {
                //已经接收到数据

                //把接收到的数据 写入缓冲数据流的尾部
                m_ReceiveMS.Position = m_ReceiveMS.Length;
                //把指定长度的字节 写入数据流
                m_ReceiveMS.Write(m_ReceiveBuffer, 0, len);
                //如果缓存数据流的长度>2，说明至少有个不完整的包过来了
                //为什么是2？ 以为客户端封装数据包 用的是ushort， 长度就是2
                if (m_ReceiveMS.Length > 2)
                {
                    //进行循环，拆分数据包
                    while (true)
                    {
                        //把数据流指针位置放在0处
                        m_ReceiveMS.Position = 0;
                        //currMsgLen = 包体的长度
                        int currMsgLen = m_ReceiveMS.ReadUShort();
                        //currFullMsgLen 总包的长度=包头长度+包体长度
                        int currFullMsgLen = 2 + currMsgLen;
                        //如果数据流的长度>=整包的长度 说明至少收到了一个完整包
                        if (m_ReceiveMS.Length >= currFullMsgLen)
                        {
                            //至少收到一个完整包
                            //定义包体的byte[]数组
                            byte[] buffer = new byte[currMsgLen];
                            //把数据流指针放到2的位置， 也就是包体的位置
                            m_ReceiveMS.Position = 2;
                            //把包体读到byte[]数组
                            m_ReceiveMS.Read(buffer, 0, currMsgLen);
                            m_ReceiveQueue.Enqueue(buffer);
                            lock (m_ReceiveQueue)
                            {
                                m_ReceiveQueue.Enqueue(buffer);
                            }

                            //========================处理剩余字节数组======================================

                            //剩余字节长度
                            int remainLen = (int)m_ReceiveMS.Length - currFullMsgLen;
                            if (remainLen > 0)
                            {
                                //把指针放在第一个包的尾部
                                m_ReceiveMS.Position = currFullMsgLen;
                                //定义剩余字节数组
                                byte[] remainBuffer = new byte[remainLen];
                                //把数据流读取到剩余字节数组
                                m_ReceiveMS.Read(remainBuffer, 0, remainLen);
                                //清空数据流
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);
                                //把剩余字节数组重新写入数据流
                                m_ReceiveMS.Write(remainBuffer, 0, remainBuffer.Length);
                                remainBuffer = null;
                            }
                            else
                            {
                                //没有剩余字节
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);
                            }
                        }
                        else
                        {
                            //还没有收到完整包
                            break;
                        }
                    }
                }
                //进行下一次接收数据包
                ReceiveMsg();
            }
            else
            {
                //客户端断开连接
                Debug.Log(string.Format("服务器{0}断开连接", m_Client.RemoteEndPoint.ToString()));
            }
        }
        catch
        {
            Debug.Log(string.Format("服务器{0}断开连接", m_Client.RemoteEndPoint.ToString()));
        }

    }
    #endregion

}
