using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��Ϣ��������
/// ���ܣ�����UI����е�����UI�����м����ݵĴ�ֵ
/// </summary>
public class MessageCenter
{
    //��Ϣ����
    public delegate void DelMessageDelivery(KeyValuesUpdate keyValuesUpdate);

    //��Ϣ���Ļ��漯��
    //string�����ݴ�ķ���    DelMessageDelivery:����ִ��ί��
    public static Dictionary<string, DelMessageDelivery> DelMessages = new Dictionary<string, DelMessageDelivery>();

    /// <summary>
    /// �����Ϣ�ļ���
    /// </summary>
    /// <param name="MessageType">��Ϣ����</param>
    /// <param name="delivery">��Ϣί��</param>
    public static void AddMessageListener(string MessageType, DelMessageDelivery delivery)
    {
        if (!DelMessages.ContainsKey(MessageType))
        {
            DelMessages.Add(MessageType, null);
        }
        DelMessages[MessageType] += delivery;
    }

    /// <summary>
    /// ȡ����Ϣ����
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
    /// ȡ�����е���Ϣ����
    /// </summary>
    public static void RemoveAllMessage()
    {
        if (DelMessages != null)
        {
            DelMessages.Clear();
        }
    }
    /// <summary>
    /// ������Ϣ
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
/// ��ֵ���¶�
/// ���ܣ����ί�У�ʵ��ί�����ݴ���
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
