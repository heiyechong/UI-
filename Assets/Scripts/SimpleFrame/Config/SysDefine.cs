using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//定义常用的变量 、委托 枚举类型（框架内的）（在框架内中的脚本使用的常量）
//1.框架核心参数

//1.系统常量
//2.全局性方法
//3.系统枚举变量
//4.委托定义
#region 系统枚举类型

/// <summary>
/// UI窗体（位置）类型
/// </summary>
public enum UIFormType
{
    Normal,   //正常（游戏中的全屏）
    Fixed,   //固定窗体（一个界面中一开始就有，不能动的）
    PopUp    //弹出窗体（点击后才出现）
}

/// <summary>
/// UI窗体的显示类型
/// </summary>
public enum UIFormsMode
{
    //普通（打开该窗体，还可以操作其他窗体）
    Normal,
    //反向切换（比如连续弹出了三个不同窗体，然后关闭窗体的顺序跟打开时相反（模态），相当于栈）
    ReverseChange,
    //隐藏其他（点击显示该窗体后把其他的窗体全部覆盖掉了 ，效果相当于场景切换）
    HideOther
}

/// <summary>
/// UI窗体透明度类型
/// </summary>
public enum UIFormsLucencyType
{

    Lucency,  //完全透明，但是不能穿透
    TransLucency, //半透明，不能穿透
    ImPenetrable,   //低透明度，不能穿透
    Pentrate        //可以穿透
}
#endregion
public class SysDefine : MonoBehaviour
{
    /*路径常量 根UI的路径*/
    public const string SYS_ROOT_PA = "Canvas";
    //inspect窗口中的标签名称
    public const string SYS_TAG_CANVAS = "RootUI";

    #region 窗体节点名称
    public static readonly string SYS_NODE_POPUP = "PopUp";
    public static readonly string SYS_NODE_NORMAL = "Normal";
    public static readonly string SYS_NODE_FIXED = "Fixed";
    public static readonly string SYS_NODE_UISCRIPTS = "UIScripts";
   
    //Json文件的路径
    public const string SYS_PATH_JSON_INFO =  "SUIFW_Res/UIFormsConfigInfo";
    #endregion
}
