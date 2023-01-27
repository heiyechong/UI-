using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// unity的帮助类
/// 功能：提供一些常用的功能方法，方便程序员快速开发程序
/// </summary>
public class UnityHelper : MonoBehaviour
{
    /// <summary>
    /// 查找指定节点,对于层级视图的节点查找
    /// </summary>
    /// <param name="parent">指点节点的根节点</param>
    /// <param name="childName">指定节点的名称</param>
    /// <returns></returns>
    public static Transform FindTheChildNode(GameObject parent, string childName)
    {
        Transform searchTrans;
        searchTrans = parent.transform.Find(childName);
        if (searchTrans == null)
        {
            foreach (Transform trans in parent.transform)
            {
                //在递归时要赋值，否则值不会传递到上一层方法中
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
    /// 获取指定节点的组件
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
    /// 添加指定节点的组件
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
            //如果有相同脚本，则先删除
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
    /// 给子节点添加父对象
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="child"></param>
    public static void AddTheChildNodeToParentNode(Transform parent, Transform child)
    {
        child.SetParent(parent, false);           //写true为世界坐标，默认为false
        child.localPosition = Vector3.zero;
        child.localScale = Vector3.one;
        child.localEulerAngles = Vector3.zero;
    }

}
