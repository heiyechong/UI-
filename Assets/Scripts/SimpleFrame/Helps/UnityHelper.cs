using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// unity�İ�����
/// ���ܣ��ṩһЩ���õĹ��ܷ������������Ա���ٿ�������
/// </summary>
public class UnityHelper : MonoBehaviour
{
    /// <summary>
    /// ����ָ���ڵ�,���ڲ㼶��ͼ�Ľڵ����
    /// </summary>
    /// <param name="parent">ָ��ڵ�ĸ��ڵ�</param>
    /// <param name="childName">ָ���ڵ������</param>
    /// <returns></returns>
    public static Transform FindTheChildNode(GameObject parent, string childName)
    {
        Transform searchTrans;
        searchTrans = parent.transform.Find(childName);
        if (searchTrans == null)
        {
            foreach (Transform trans in parent.transform)
            {
                //�ڵݹ�ʱҪ��ֵ������ֵ���ᴫ�ݵ���һ�㷽����
                searchTrans = FindTheChildNode(trans.gameObject, childName);
                if (searchTrans != null)
                {
                    return searchTrans;
                }
            }
        }
        return searchTrans;
    }

    /// <summary>
    /// ��ȡָ���ڵ�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetTheChildNodeComponent<T>(GameObject parent, string childName) where T : Component
    {
        Transform searchtrans;
        searchtrans = FindTheChildNode(parent, childName);
        if (searchtrans != null)
        {
            return searchtrans.GetComponent<T>();
        }
        return null;
    }
  
    
    /// <summary>
    /// ���ָ���ڵ�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parent"></param>
    /// <param name="childName"></param>
    /// <returns></returns>
    public static T AddTheChildNodeComponent<T>(GameObject parent, string childName) where T : Component
    {
        Transform searchtrans;
        searchtrans = FindTheChildNode(parent, childName);
        if (searchtrans != null)
        {
            //�������ͬ�ű�������ɾ��
            T[] ts = searchtrans.GetComponents<T>();
            for (int i = 0; i < ts.Length; i++)
            {
                if (ts[i] != null)
                {
                    Destroy(ts[i]);
                }
            }
            return searchtrans.gameObject.AddComponent<T>();
        }
        return null;
    }

   
    
    /// <summary>
    /// ���ӽڵ���Ӹ�����
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="child"></param>
    public static void AddTheChildNodeToParentNode(Transform parent, Transform child)
    {
        child.SetParent(parent, false);           //дtrueΪ�������꣬Ĭ��Ϊfalse
        child.localPosition = Vector3.zero;
        child.localScale = Vector3.one;
        child.localEulerAngles = Vector3.zero;
    }

}
