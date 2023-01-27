using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///通用配置管理器接口
///功能：基于“键值对”配置文件（json文件）的通用解析
/// </summary>
interface IConfigManager
{
    /// <summary>
    /// 只读属性，应用设置
    /// 功能：得到键值对集合的数据
    /// </summary>
    Dictionary<string, string> AppSetting { get; }

    /// <summary>
    /// 得到配置文件（AppSetting）的最大数量
    /// </summary>
    /// <returns></returns>
    int GetAppSettingMaxNummber();
}
[System.Serializable]   
internal class KeyValuesInfo
{
    //配置信息
    public List<KeyValue> ConfigInfo;
}
[System.Serializable]   
internal class KeyValue
{
    public string key;
    public string value;
}

