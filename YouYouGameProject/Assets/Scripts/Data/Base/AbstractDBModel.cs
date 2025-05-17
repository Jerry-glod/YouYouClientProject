using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDBModel<T,P> 
    where T:class,new()
    where P:AbstractEntity
{
    protected List<P> m_List;
    protected Dictionary<int, P> m_Dic;

    public AbstractDBModel()
    {
        m_List = new List<P>();
        m_Dic = new Dictionary<int, P>();
        LoadData();
    }

    #region ����
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    #endregion

    #region ��Ҫ����ʵ�ֵ����Ժͷ���
    protected abstract string FileName { get; }

    /// <summary>
    /// ����ʵ��
    /// </summary>
    /// <param name="parser"></param>
    /// <returns></returns>
    protected abstract P MakeEntity(GameDataTableParser parser);
#endregion

    /// <summary>
    /// ��������
    /// </summary>
    private void LoadData()
    {
        using (GameDataTableParser parse = new GameDataTableParser(string.Format(@"F:\UnityProject\MyProject\MyYouYouProject\YouYouGameProject\Assets\www\Data\{0}", FileName)))
        {
            while (!parse.Eof)
            {
                ///����ʵ��
                P p = MakeEntity(parse);
                m_List.Add(p);
                m_Dic[p.Id] = p;
                parse.Next();
            }
        }
    }
    public List<P> GetList()
    {
        return m_List;
    }

    public P Get(int id)
    {
        if (m_Dic.ContainsKey(id))
        {
            return m_Dic[id];
        }
        return null;
    }
}
