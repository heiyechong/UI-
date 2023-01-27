using Assets.Scripts.SimpleFrame.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManagerOfJson : IConfigManager
{
    //�洢json�ַ���������
    private Dictionary<string, string> appSetting;

    public Dictionary<string, string> AppSetting
    {
        get { return appSetting; }
    }

    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="jsonPath">json�ļ���·��</param>
    public ConfigManagerOfJson(string jsonPath)
    {
        appSetting = new Dictionary<string, string>();
        //����json���洢���ֵ���
        InitAndAnalysisJson(jsonPath);
    }

    /// <summary>
    /// ����json�ļ�
    /// </summary>
    /// <param name="jsonPath"></param>
    private void InitAndAnalysisJson(string jsonPath)
    {
        try
        {
            //�ж��Ƿ�Ϊ��
            if (string.IsNullOrEmpty(jsonPath))
            {
                return;
            }
            //����json�ļ�
            TextAsset textAsset = Resources.Load<TextAsset>(jsonPath);
            KeyValuesInfo keyValues = JsonUtility.FromJson<KeyValuesInfo>(textAsset.text);
            //�洢���ֵ���
            foreach (KeyValue item in keyValues.ConfigInfo)
            {
                appSetting.Add(item.key, item.value);
            }
        }
        catch
        {
            //���Զ����쳣
            throw new JsonAnalysisException("JsonPath is Wrong");
        }
    }
    //�õ������ļ���AppSetting�����������
    public int GetAppSettingMaxNummber()
    {
        if (appSetting != null && appSetting.Count >= 1)
        {
            return appSetting.Count;
        }
        return -1;
    }


}
