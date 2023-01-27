using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemoScript;

//是框架的核心，用户程序（框架之外的程序）通过本脚本，来实现框架绝大多数功能
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    //窗体预设的路径（参数1：窗体预设的名称， 参数2：窗体预设的路径）
    private Dictionary<string, string> _DicFormsPaths;
    //当前显示的窗体
    private Dictionary<string, BaseUIForm> _DicCurrentShowUIForms;
    //存储所有窗体的实例
    private Dictionary<string, BaseUIForm> _DicALLForms;
    //定义栈集合，存储显示当前所有具备反向切换类型的UI窗体
    private Stack<BaseUIForm> _StaCurrentUIForms;
    //根节点
    private Transform _TranCanvas;
    //全屏幕显示的节点
    private Transform _TraNormal;
    //固定显示的节点
    private Transform _TraFixed;
    //弹出节点
    private Transform _TraPopUp;
    //UI管理脚本的节点
    private Transform _TraUIScripts;
    public static UIManager GetUIManager()
    {
        if (_instance == null)
        {
            _instance = new GameObject("UIManager").AddComponent<UIManager>();
        }
        return _instance;
    }
    //初始化核心数据，加载窗体路径到字典中
    private void Awake()
    {
        //字段初始化
        _DicALLForms = new Dictionary<string, BaseUIForm>();
        _DicCurrentShowUIForms = new Dictionary<string, BaseUIForm>();
        _DicFormsPaths = new Dictionary<string, string>();
        _StaCurrentUIForms = new Stack<BaseUIForm>();
        //初始化加载（根节点）Canvas预设
        InitRootUI();

        //获取全屏幕节点、固定节点、弹出节点
        _TranCanvas = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS).transform;
        _TraPopUp = UnityHelper.FindTheChildNode(_TranCanvas.gameObject, SysDefine.SYS_NODE_POPUP);
        _TraNormal = UnityHelper.FindTheChildNode(_TranCanvas.gameObject, SysDefine.SYS_NODE_NORMAL);
        _TraFixed = UnityHelper.FindTheChildNode(_TranCanvas.gameObject, SysDefine.SYS_NODE_FIXED);
        _TraUIScripts = UnityHelper.FindTheChildNode(_TranCanvas.gameObject, SysDefine.SYS_NODE_UISCRIPTS);

        //把本脚本作为UI管理脚本节点的子节点
        gameObject.transform.SetParent(_TraUIScripts, false);

        //在转换场景时，保证不销毁"根UI窗体"
        DontDestroyOnLoad(_TranCanvas);

        //初始化UI窗体预设的路径数据（先写简单的，后面再写json）
        if (_DicFormsPaths != null)
        {
            InitJsonData(SysDefine.SYS_PATH_JSON_INFO);
        }
    }
    //初始化加载（根节点）Canvas预设
    private void InitRootUI()
    {
        ResourcesMgr.GetInstance().LoadAsset(SysDefine.SYS_ROOT_PA, false);
    }


    #region 显示窗体

    /// <summary>
    ///显示UI窗体
    ///功能：
    ///1.根据窗体名称，加入到“存储所有窗体的实例”的字典
    ///2.根据UI窗体不同的显示类型，做不同的加载方式
    ///3.
    /// </summary>
    /// <param name="uIName"></param>
    public void ShowUIForms(string uIName)
    {

        //参数的检测
        if (string.IsNullOrEmpty(uIName))
        {
            return;
        }

        //根据窗体名称，加入到“存储所有窗体的实例”的字典
        BaseUIForm baseUIForm = LoadFormsToAllDic(uIName);
        //是否清空栈中的数据  
        if (baseUIForm.currentUIType.isCleanStack)
        {
            CleanStack();
        }

        //根据UI窗体不同的显示类型，做不同的加载方式
        switch (baseUIForm.currentUIType.uIFormsMode)
        {
            case UIFormsMode.Normal:
                AddToDicCurrent(uIName);
                break;
            case UIFormsMode.ReverseChange:
                PushUIFormToStack(uIName);
                break;
            case UIFormsMode.HideOther:
                EnterAndHidingOther(uIName);
                break;

        }
    }
    //根据窗体名称，将窗体实例加入到“存储所有窗体的实例”的字典
    //有的话就不加载了
    private BaseUIForm LoadFormsToAllDic(string UIName)
    {
        ///
        BaseUIForm baseUIForm;
        //如果字典不含该UI
        _DicALLForms.TryGetValue(UIName, out baseUIForm);
        if (baseUIForm == null)
        {
            baseUIForm = LoadForms(UIName);
        }
        return baseUIForm;
    }

    /// <summary>
    /// 加载指定名称的窗体
    /// 功能：
    /// 1.根据“UI窗体名称”，加载预设体
    /// 2.根据预设体的脚本的UI窗体位置类型，加载预设体到对应的根窗体的子节点
    /// 3.隐藏刚创建好的窗体UI
    /// 4.把刚创建的窗体UI添加到“存储所有窗体的实例”的字典
    /// </summary>
    /// <param name="uIName"></param>
    /// <returns></returns>
    private BaseUIForm LoadForms(string uIName)
    {
        string uIPath;
        //根据窗体名称得到路径
        _DicFormsPaths.TryGetValue(uIName, out uIPath);
        if (string.IsNullOrEmpty(uIPath))
        {
            return null;
        }
        //根据窗体路径，加载预制体
        GameObject game = ResourcesMgr.GetInstance().LoadAsset(uIPath, false);

        //设置窗体的父节点（根据预设体的脚本的UI窗体位置类型）
        if (_TranCanvas != null && game != null)
        {
            //获取预设体的BaseUIForm脚本中的UI窗体位置类型
            BaseUIForm baseUIForm = game.GetComponent<BaseUIForm>();
            if (baseUIForm == null)
            {
                return null;
            }
            switch (baseUIForm.currentUIType.uIFormType)
            {
                case UIFormType.Normal:
                    game.transform.SetParent(_TraNormal, false);
                    break;
                case UIFormType.Fixed:
                    game.transform.SetParent(_TraFixed, false);
                    break;
                case UIFormType.PopUp:
                    game.transform.SetParent(_TraPopUp, false);
                    break;
                default:
                    break;
            }
            //设置隐藏
            game.SetActive(false);
            //把刚创建的窗体UI添加到“存储所有窗体的实例”的字典
            _DicALLForms.Add(uIName, baseUIForm);
            return baseUIForm;
        }
        else
        {
            Debug.Log("_TranCanvas==null || game==null");
        }
        return null;

    }

    /// <summary>
    /// 将当前显示的窗体存储到当前显示的窗体字典中
    /// </summary>
    private void AddToDicCurrent(string uIName)
    {
        BaseUIForm baseUIForm;                                                          //uI窗体基类
        BaseUIForm baseUIFormFromAllForms;                                              //从 _DicALLForms得到的窗体
        //如果在当前显示的字典中有该窗体，直接返回
        _DicCurrentShowUIForms.TryGetValue(uIName, out baseUIForm);
        if (baseUIForm != null)
        {
            return;
        }

        //把当前显示的窗体存储到“正在显示的”字典中
        _DicALLForms.TryGetValue(uIName, out baseUIFormFromAllForms);
        if (baseUIFormFromAllForms != null)
        {
            _DicCurrentShowUIForms.Add(uIName, baseUIFormFromAllForms);
            //显示该窗体
            baseUIFormFromAllForms.DisPlay();
        }

    }
    /// <summary>
    /// UI窗体入栈
    /// </summary>
    /// <param name="uIName"></param>
    private void PushUIFormToStack(string uIName)
    {
        BaseUIForm baseUIForm;
        //判断栈里面有没有其他窗体，有就将栈顶元素进行冻结处理
        if (_StaCurrentUIForms.Count > 0)
        {
            _StaCurrentUIForms.Peek().Freeze();
        }
        //判断存储所有窗体字典有没有指定的元素
        _DicALLForms.TryGetValue(uIName, out baseUIForm);
        if (baseUIForm != null)
        {
            //将该窗体的状态调整为显示状态
            baseUIForm.DisPlay();
            //将该ui窗体加入栈中
            _StaCurrentUIForms.Push(baseUIForm);
        }
        else
        {
            Debug.Log("baseUIForm==null,Please check");
        }
    }

    /// <summary>
    /// “隐藏其他类型”窗体的显示
    /// </summary>
    /// <param name="uIName"></param>
    private void EnterAndHidingOther(string uIName)
    {
        BaseUIForm baseUIForm;
        BaseUIForm baseUIFormFromAll;                                           //从集合中得到的BaseUIForm
        //TODO  参数检测

        //判断“当前显示”的字典中有无该窗体，有则return
        _DicCurrentShowUIForms.TryGetValue(uIName, out baseUIForm);
        if (baseUIForm != null)
        {
            return;
        }
        //隐藏“当前显示”的字典和栈中的UI窗体
        foreach (BaseUIForm item in _DicCurrentShowUIForms.Values)
        {
            item.Hiding();
        }
        foreach (BaseUIForm stackUI in _StaCurrentUIForms)
        {
            stackUI.Hiding();
        }

        //将该UI窗体添加进“当前显示”的字典中
        _DicALLForms.TryGetValue(uIName, out baseUIFormFromAll);
        if (baseUIFormFromAll != null)
        {
            _DicCurrentShowUIForms.Add(uIName, baseUIFormFromAll);
            //将该窗体的状态调整为显示状态
            baseUIFormFromAll.DisPlay();
        }
    }

    /// <summary>
    /// 清空栈,并且返回是否已经清空栈
    /// </summary>
    /// <returns></returns>
    private bool CleanStack()
    {
        if (_StaCurrentUIForms != null && _StaCurrentUIForms.Count >= 1)
        {
            _StaCurrentUIForms.Clear();
            return true;
        }
        return false;
    }
    #endregion

    #region 关闭窗体
    public void CloseUIForms(string uIName)
    {
        BaseUIForm baseUIForm;
        //参数检查
        if (string.IsNullOrEmpty(uIName))
        {
            return;
        }
        //“存储所有窗体的实例”的字典中有无该窗体
        _DicALLForms.TryGetValue(uIName, out baseUIForm);
        if (baseUIForm == null)
        {
            return;
        }
        //根据窗体的显示类型分别做不同的关闭处理
        switch (baseUIForm.currentUIType.uIFormsMode)
        {
            //普通类型的
            case UIFormsMode.Normal:
                ExitUIForm(uIName);
                break;
            //反向切换
            case UIFormsMode.ReverseChange:
                PopUIForm(uIName);
                break;
            //隐藏其他
            case UIFormsMode.HideOther:
                ExitUIFormAndDisplayOther(uIName);
                break;
        }
    }

    /// <summary>
    /// 退出指定的UI窗体（普通类型调用）
    /// </summary>
    /// <param name="uIName"></param>
    private void ExitUIForm(string uIName)
    {
        BaseUIForm baseUIForm;
        //判断“当前显示的窗体”的字典有无该窗体
        _DicCurrentShowUIForms.TryGetValue(uIName, out baseUIForm);
        if (baseUIForm == null)
        {
            return;
        }
        //该窗体状态改为隐藏状态
        baseUIForm.Hiding();
        //从存储“当前显示的窗体”的字典中移除该窗体
        _DicCurrentShowUIForms.Remove(uIName);
    }

    /// <summary>
    /// 具备反向切换的UI窗体的退出
    /// </summary>
    private void PopUIForm(string uIName)
    {

        if (_StaCurrentUIForms.Count >= 2)
        {
            //出栈处理
            BaseUIForm baseUIForm = _StaCurrentUIForms.Pop();
            //将该窗体状态置于隐藏状态
            baseUIForm.Hiding();
            //出栈后，将下一个置于重新显示状态
            BaseUIForm topUIForm = _StaCurrentUIForms.Peek();
            topUIForm.ReDisPlay();
        }
        else if (_StaCurrentUIForms.Count == 1)
        {
            //出栈处理
            BaseUIForm baseUIForm = _StaCurrentUIForms.Pop();
            //将该窗体状态置于隐藏状态
            baseUIForm.Hiding();
        }
    }
    /// <summary>
    /// “隐藏其他类型”窗体的关闭且打开其他窗体
    /// </summary>
    /// <param name="uIName"></param>
    private void ExitUIFormAndDisplayOther(string uIName)
    {
        BaseUIForm baseUIForm;
        //TODO  参数检测

        //判断“当前显示”的字典中有无该窗体，无则证明有错误出现，return
        _DicCurrentShowUIForms.TryGetValue(uIName, out baseUIForm);
        if (baseUIForm == null)
        {
            return;
        }
        //将该UI窗体的状态设置为隐藏状态，并从“当前显示”的字典中移除出去
        baseUIForm.Hiding();
        _DicCurrentShowUIForms.Remove(uIName);

        //显示“当前显示”的字典和栈中的UI窗体
        foreach (BaseUIForm item in _DicCurrentShowUIForms.Values)
        {
            item.ReDisPlay();
        }
        foreach (BaseUIForm stackUI in _StaCurrentUIForms)
        {
            stackUI.ReDisPlay();
        }
    }
    #endregion

    #region 显示UIManager中的三个字典的数据，供测试时使用
    /// <summary>
    /// "存储所有窗体的实例"的字典的长度
    /// </summary>
    /// <returns></returns>
    public int ShowDicALLFormsCount()
    {
        if (_DicALLForms != null)
        {
            return _DicALLForms.Count;
        }
        return 0;
    }
    /// <summary>
    /// "存储当前显示窗体（隐藏其他和普通）的实例"的字典的长度
    /// </summary>
    /// <returns></returns>
    public int ShowDicCurrentShowUIFormsCount()
    {
        if (_DicCurrentShowUIForms != null)
        {
            return _DicCurrentShowUIForms.Count;
        }
        return 0;
    }

    /// <summary>
    /// "存储当前显示窗体（反向切换类型的窗体）的实例"的字典的长度
    /// </summary>
    /// <returns></returns>
    public int ShowStaCurrentUIFormsCount()
    {
        if (_StaCurrentUIForms != null)
        {
            return _StaCurrentUIForms.Count;
        }
        return 0;
    }
    #endregion

    /// <summary>
    /// 将JsonPath写入"窗体预设的路径"的字典中
    /// </summary>
    private void InitJsonData(string json)
    {
        if (json != null)
        {
            ConfigManagerOfJson configManagerOfJson = new ConfigManagerOfJson(json);
            _DicFormsPaths = configManagerOfJson.AppSetting;
        }
    }
}
