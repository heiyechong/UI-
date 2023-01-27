using DemoScript;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI遮罩管理器
/// 功能：负责“弹出窗体”的模态显示实现
/// </summary>
public class UIMaskMgr : MonoBehaviour
{
    private static UIMaskMgr _instance = null;
    //根窗体
    private GameObject _Canvas;
    //进行遮罩的窗体
    private GameObject _UIMaskPanel;
    //UI摄像机
    private Camera _UICamera;
    //UI摄像机一开始的深度
    private float _UICameraDepth;
    //本脚本放置在哪个父对象下
    private GameObject _ParentUI;
    //顶层面板（即根窗体）（要将顶层面板置于整个Inspect窗口的最下面）
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
            Debug.Log("报错,无UICamera");
        }
    }

    /// <summary>
    /// 设置遮罩
    /// </summary>
    /// <param name="goDisplayUIForm">需要显示的窗体</param>
    /// <param name="uIFormsLucencyType">显示透明度属性</param>
    public void SetMask(GameObject goDisplayUIForm, UIFormsLucencyType uIFormsLucencyType = UIFormsLucencyType.Lucency)
    {
        //顶层窗体下移
        _GOTopPanal.transform.SetAsLastSibling();
        //遮罩窗体下移
        _UIMaskPanel.transform.SetAsLastSibling();
        //显示窗体下移
        goDisplayUIForm.transform.SetAsLastSibling();
        //设置遮罩，启用遮罩窗体以及设置透明度
        Color color = new Color(60 / 255f, 60 / 255f, 60 / 255f, 0);
        Color color1 = new Color(60 / 255f, 60 / 255f, 60 / 255f, 130/255f);
        Color color2 = new Color(60 / 255f, 60 / 255f, 60 / 255f, 200 / 255f);
        switch (uIFormsLucencyType)
        {
            //完全透明，但是不能穿透
            case UIFormsLucencyType.Lucency:
                _UIMaskPanel.SetActive(true);
                _UIMaskPanel.GetComponent<Image>().color = color;
                break;
            //半透明不能穿透
            case UIFormsLucencyType.TransLucency:
                _UIMaskPanel.SetActive(true);
                _UIMaskPanel.GetComponent<Image>().color = color1;
                break;
            //低透明不能穿透
            case UIFormsLucencyType.ImPenetrable:
                _UIMaskPanel.SetActive(true);
                _UIMaskPanel.GetComponent<Image>().color = color2;
                break;
            //完全透明可以穿透
            case UIFormsLucencyType.Pentrate:
                if (_UIMaskPanel.activeInHierarchy)
                {
                    _UIMaskPanel.SetActive(false);
                }
               
                break;
            default:
                break;
        }
        //增加UICamera的深度（保证当前UIcamera在最前面显示）
        if (_UICamera != null)
        {
            _UICamera.depth = _UICameraDepth + 100;
        }
    }

    /// <summary>
    /// 取消遮罩
    /// </summary>
    public void CancelMask()
    {
        //顶层窗体上移
        _GOTopPanal.transform.SetAsFirstSibling();
        //取消遮罩
        if (_UIMaskPanel.activeInHierarchy)
        {
            _UIMaskPanel.SetActive(false);
        }
        //将UICamera的深度调回原始数值
        if (_UICamera != null)
        {
            _UICamera.depth = _UICameraDepth;
        }
    }
}
