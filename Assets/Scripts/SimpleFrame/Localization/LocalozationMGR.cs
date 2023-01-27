using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Թ��ʻ�
/// ���ܣ�ʹ�����Ƿ�������Ϸ����ÿ��������ʾ��Ӧ������
/// </summary>
public class LocalozationMGR
{
    private static LocalozationMGR _instance;
    public static LocalozationMGR GetInstance()
    {
        if (_instance == null)
        {
            _instance = new LocalozationMGR();
        }
        return _instance;
    }
    //�����ֵ�
    private Dictionary<string, string> _localozationCache;

    private LocalozationMGR()
    {
        _localozationCache = new Dictionary<string, string>();
        InitLanguageCache("SUIFW_Res/LauguageJSONConfig");
    }

    /// <summary>
    /// ��ʼ�������ֵ�
    /// </summary>
    /// <param name="jsonPath"></param>
    private void InitLanguageCache(string jsonPath)
    {
        IConfigManager configManagerOfJson = new ConfigManagerOfJson(jsonPath);
        _localozationCache = configManagerOfJson.AppSetting;
    }

    /// <summary>
    /// �õ���ʾ�ı���Ϣ
    /// </summary>
    /// <returns></returns>
    public string ShowText(string languageKey)
    {
        string text = string.Empty;
        if (string.IsNullOrEmpty(languageKey))
        {
            return null;
        }
        if (_localozationCache!=null&&_localozationCache.Count>=1)
        {
            if (_localozationCache.TryGetValue(languageKey,out text))
            {
                if (text!=null)
                {
                    return text;
                }
            }
        }
        return null;
    }
}
