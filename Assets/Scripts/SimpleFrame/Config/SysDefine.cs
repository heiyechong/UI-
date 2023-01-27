using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���峣�õı��� ��ί�� ö�����ͣ�����ڵģ����ڿ�����еĽű�ʹ�õĳ�����
//1.��ܺ��Ĳ���

//1.ϵͳ����
//2.ȫ���Է���
//3.ϵͳö�ٱ���
//4.ί�ж���
#region ϵͳö������

/// <summary>
/// UI���壨λ�ã�����
/// </summary>
public enum UIFormType
{
    Normal,   //��������Ϸ�е�ȫ����
    Fixed,   //�̶����壨һ��������һ��ʼ���У����ܶ��ģ�
    PopUp    //�������壨�����ų��֣�
}

/// <summary>
/// UI�������ʾ����
/// </summary>
public enum UIFormsMode
{
    //��ͨ���򿪸ô��壬�����Բ����������壩
    Normal,
    //�����л�����������������������ͬ���壬Ȼ��رմ����˳�����ʱ�෴��ģ̬�����൱��ջ��
    ReverseChange,
    //���������������ʾ�ô����������Ĵ���ȫ�����ǵ��� ��Ч���൱�ڳ����л���
    HideOther
}

/// <summary>
/// UI����͸��������
/// </summary>
public enum UIFormsLucencyType
{

    Lucency,  //��ȫ͸�������ǲ��ܴ�͸
    TransLucency, //��͸�������ܴ�͸
    ImPenetrable,   //��͸���ȣ����ܴ�͸
    Pentrate        //���Դ�͸
}
#endregion
public class SysDefine : MonoBehaviour
{
    /*·������ ��UI��·��*/
    public const string SYS_ROOT_PA = "Canvas";
    //inspect�����еı�ǩ����
    public const string SYS_TAG_CANVAS = "RootUI";

    #region ����ڵ�����
    public static readonly string SYS_NODE_POPUP = "PopUp";
    public static readonly string SYS_NODE_NORMAL = "Normal";
    public static readonly string SYS_NODE_FIXED = "Fixed";
    public static readonly string SYS_NODE_UISCRIPTS = "UIScripts";
   
    //Json�ļ���·��
    public const string SYS_PATH_JSON_INFO =  "SUIFW_Res/UIFormsConfigInfo";
    #endregion
}
