using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消息传递中心
/// 功能：负责UI框架中的所有UI窗体中间数据的传值
/// </summary>
public class MessageCenter
{
    //消息传递
    public delegate void DelMessageDelivery(KeyValuesUpdate keyValuesUpdate);

    //消息中心缓存集合
    //string：数据大的分类    DelMessageDelivery:数据执行委托
    public static Dictionary<string, DelMessageDelivery> DelMessages = new Dictionary<string, DelMessageDelivery>();

    /// <summary>
    /// 添加消息的监听
    /// </summary>
    /// <param name="MessageType">消息分类</param>
    /// <param name="delivery">消息委托</param>
    public static void AddMessageListener(string MessageType, DelMessageDelivery delivery)
    {
        if (!DelMessages.ContainsKey(MessageType))
        {
            DelMessages.Add(MessageType, null);
        }
        DelMessages[MessageType] += delivery;
    }

    /// <summary>
    /// 取消消息监听
    /// </summary>
    /// <param name="MessageType"></param>
    /// <param name="delivery"></param>
    public static void RemoveMessageListener(string MessageType, DelMessageDelivery delivery)
    {

        if (DelMessages.ContainsKey(MessageType))
        {
            DelMessages[MessageType] -= delivery;
        }
    }
    /// <summary>
    /// 取消所有的消息监听
    /// </summary>
    public static void RemoveAllMessage()
    {
        if (DelMessages != null)
        {
            DelMessages.Clear();
        }
    }
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="MessageType"></param>
    /// <param name="keyValuesUpdate"></param>
    public static void SendMessage(string MessageType, KeyValuesUpdate keyValuesUpdate)
    {
        DelMessageDelivery delivery;
        if (DelMessages.TryGetValue(MessageType, out delivery))
        {
            if (delivery != null)
            {
                delivery(keyValuesUpdate);
            }

        }
    }
}

/// <summary>
/// 键值更新对
/// 功能：配合委托，实现委托数据传递
/// </summary>
public class KeyValuesUpdate
{
    private string key;
    public string Key
    {
        get { return key; }
    }
    private object value;
    public object Value
    {
        get { return value; }
    }

    public KeyValuesUpdate(string key, object value)
    {
        this.key = key;
        this.value = value;
    }
}
