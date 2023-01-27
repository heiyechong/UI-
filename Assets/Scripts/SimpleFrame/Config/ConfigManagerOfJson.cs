using Assets.Scripts.SimpleFrame.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManagerOfJson : IConfigManager
{
    //存储json字符串的数据
    private Dictionary<string, string> appSetting;

    public Dictionary<string, string> AppSetting
    {
        get { return appSetting; }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jsonPath">json文件的路径</param>
    public ConfigManagerOfJson(string jsonPath)
    {
        appSetting = new Dictionary<string, string>();
        //解析json并存储到字典中
        InitAndAnalysisJson(jsonPath);
    }

    /// <summary>
    /// 解析json文件
    /// </summary>
    /// <param name="jsonPath"></param>
    private void InitAndAnalysisJson(string jsonPath)
    {
        try
        {
            //判断是否为空
            if (string.IsNullOrEmpty(jsonPath))
            {
                return;
            }
            //解析json文件
            TextAsset textAsset = Resources.Load<TextAsset>(jsonPath);
            KeyValuesInfo keyValues = JsonUtility.FromJson<KeyValuesInfo>(textAsset.text);
            //存储到字典中
            foreach (KeyValue item in keyValues.ConfigInfo)
            {
                appSetting.Add(item.key, item.value);
            }
        }
        catch
        {
            //抛自定义异常
            throw new JsonAnalysisException("JsonPath is Wrong");
        }
    }
    //得到配置文件（AppSetting）的最大数量
    public int GetAppSettingMaxNummber()
    {
        if (appSetting != null && appSetting.Count >= 1)
        {
            return appSetting.Count;
        }
        return -1;
    }


}
