using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 语言国际化
/// 功能：使得我们发布的游戏，在每个国家显示对应的语言
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
    //语言字典
    private Dictionary<string, string> _localozationCache;

    private LocalozationMGR()
    {
        _localozationCache = new Dictionary<string, string>();
        InitLanguageCache("SUIFW_Res/LauguageJSONConfig");
    }

    /// <summary>
    /// 初始化语言字典
    /// </summary>
    /// <param name="jsonPath"></param>
    private void InitLanguageCache(string jsonPath)
    {
        IConfigManager configManagerOfJson = new ConfigManagerOfJson(jsonPath);
        _localozationCache = configManagerOfJson.AppSetting;
    }

    /// <summary>
    /// 得到显示文本信息
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
