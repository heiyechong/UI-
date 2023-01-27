using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///ͨ�����ù������ӿ�
///���ܣ����ڡ���ֵ�ԡ������ļ���json�ļ�����ͨ�ý���
/// </summary>
interface IConfigManager
{
    /// <summary>
    /// ֻ�����ԣ�Ӧ������
    /// ���ܣ��õ���ֵ�Լ��ϵ�����
    /// </summary>
    Dictionary<string, string> AppSetting { get; }

    /// <summary>
    /// �õ������ļ���AppSetting�����������
    /// </summary>
    /// <returns></returns>
    int GetAppSettingMaxNummber();
}
[System.Serializable]   
internal class KeyValuesInfo
{
    //������Ϣ
    public List<KeyValue> ConfigInfo;
}
[System.Serializable]   
internal class KeyValue
{
    public string key;
    public string value;
}

