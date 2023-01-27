using DemoScript;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI���ֹ�����
/// ���ܣ����𡰵������塱��ģ̬��ʾʵ��
/// </summary>
public class UIMaskMgr : MonoBehaviour
{
    private static UIMaskMgr _instance = null;
    //������
    private GameObject _Canvas;
    //�������ֵĴ���
    private GameObject _UIMaskPanel;
    //UI�����
    private Camera _UICamera;
    //UI�����һ��ʼ�����
    private float _UICameraDepth;
    //���ű��������ĸ���������
    private GameObject _ParentUI;
    //������壨�������壩��Ҫ�����������������Inspect���ڵ������棩
    private GameObject _GOTopPanal;
    public static UIMaskMgr GetUIMaskMgr()
    {
        if (_instance == null)
        {
            _instance = new GameObject("UIMaskMgr").AddComponent<UIMaskMgr>();
        }
        return _instance;
    }
    void Awake()
    {
        _Canvas = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS);
        _GOTopPanal = _Canvas;
        _ParentUI = UnityHelper.FindTheChildNode(_Canvas, SysDefine.SYS_NODE_UISCRIPTS).gameObject;
        UnityHelper.AddTheChildNodeToParentNode(_ParentUI.transform, transform);
        _UIMaskPanel = UnityHelper.FindTheChildNode(_Canvas, "UIMaskPanel").gameObject;
        _UICamera = UnityHelper.FindTheChildNode(_Canvas, "UICamera").GetComponent<Camera>();
        if (_UICamera != null)
        {
            _UICameraDepth = _UICamera.depth;
        }
        else
        {
            Debug.Log("����,��UICamera");
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="goDisplayUIForm">��Ҫ��ʾ�Ĵ���</param>
    /// <param name="uIFormsLucencyType">��ʾ͸��������</param>
    public void SetMask(GameObject goDisplayUIForm, UIFormsLucencyType uIFormsLucencyType = UIFormsLucencyType.Lucency)
    {
        //���㴰������
        _GOTopPanal.transform.SetAsLastSibling();
        //���ִ�������
        _UIMaskPanel.transform.SetAsLastSibling();
        //��ʾ��������
        goDisplayUIForm.transform.SetAsLastSibling();
        //�������֣��������ִ����Լ�����͸����
        Color color = new Color(60 / 255f, 60 / 255f, 60 / 255f, 0);
        Color color1 = new Color(60 / 255f, 60 / 255f, 60 / 255f, 130/255f);
        Color color2 = new Color(60 / 255f, 60 / 255f, 60 / 255f, 200 / 255f);
        switch (uIFormsLucencyType)
        {
            //��ȫ͸�������ǲ��ܴ�͸
            case UIFormsLucencyType.Lucency:
                _UIMaskPanel.SetActive(true);
                _UIMaskPanel.GetComponent<Image>().color = color;
                break;
            //��͸�����ܴ�͸
            case UIFormsLucencyType.TransLucency:
                _UIMaskPanel.SetActive(true);
                _UIMaskPanel.GetComponent<Image>().color = color1;
                break;
            //��͸�����ܴ�͸
            case UIFormsLucencyType.ImPenetrable:
                _UIMaskPanel.SetActive(true);
                _UIMaskPanel.GetComponent<Image>().color = color2;
                break;
            //��ȫ͸�����Դ�͸
            case UIFormsLucencyType.Pentrate:
                if (_UIMaskPanel.activeInHierarchy)
                {
                    _UIMaskPanel.SetActive(false);
                }
               
                break;
            default:
                break;
        }
        //����UICamera����ȣ���֤��ǰUIcamera����ǰ����ʾ��
        if (_UICamera != null)
        {
            _UICamera.depth = _UICameraDepth + 100;
        }
    }

    /// <summary>
    /// ȡ������
    /// </summary>
    public void CancelMask()
    {
        //���㴰������
        _GOTopPanal.transform.SetAsFirstSibling();
        //ȡ������
        if (_UIMaskPanel.activeInHierarchy)
        {
            _UIMaskPanel.SetActive(false);
        }
        //��UICamera����ȵ���ԭʼ��ֵ
        if (_UICamera != null)
        {
            _UICamera.depth = _UICameraDepth;
        }
    }
}
