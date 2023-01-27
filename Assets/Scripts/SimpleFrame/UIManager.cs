using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemoScript;

//�ǿ�ܵĺ��ģ��û����򣨿��֮��ĳ���ͨ�����ű�����ʵ�ֿ�ܾ����������
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    //����Ԥ���·��������1������Ԥ������ƣ� ����2������Ԥ���·����
    private Dictionary<string, string> _DicFormsPaths;
    //��ǰ��ʾ�Ĵ���
    private Dictionary<string, BaseUIForm> _DicCurrentShowUIForms;
    //�洢���д����ʵ��
    private Dictionary<string, BaseUIForm> _DicALLForms;
    //����ջ���ϣ��洢��ʾ��ǰ���о߱������л����͵�UI����
    private Stack<BaseUIForm> _StaCurrentUIForms;
    //���ڵ�
    private Transform _TranCanvas;
    //ȫ��Ļ��ʾ�Ľڵ�
    private Transform _TraNormal;
    //�̶���ʾ�Ľڵ�
    private Transform _TraFixed;
    //�����ڵ�
    private Transform _TraPopUp;
    //UI����ű��Ľڵ�
    private Transform _TraUIScripts;
    public static UIManager GetUIManager()
    {
        if (_instance == null)
        {
            _instance = new GameObject("UIManager").AddComponent<UIManager>();
        }
        return _instance;
    }
    //��ʼ���������ݣ����ش���·�����ֵ���
    private void Awake()
    {
        //�ֶγ�ʼ��
        _DicALLForms = new Dictionary<string, BaseUIForm>();
        _DicCurrentShowUIForms = new Dictionary<string, BaseUIForm>();
        _DicFormsPaths = new Dictionary<string, string>();
        _StaCurrentUIForms = new Stack<BaseUIForm>();
        //��ʼ�����أ����ڵ㣩CanvasԤ��
        InitRootUI();

        //��ȡȫ��Ļ�ڵ㡢�̶��ڵ㡢�����ڵ�
        _TranCanvas = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS).transform;
        _TraPopUp = UnityHelper.FindTheChildNode(_TranCanvas.gameObject, SysDefine.SYS_NODE_POPUP);
        _TraNormal = UnityHelper.FindTheChildNode(_TranCanvas.gameObject, SysDefine.SYS_NODE_NORMAL);
        _TraFixed = UnityHelper.FindTheChildNode(_TranCanvas.gameObject, SysDefine.SYS_NODE_FIXED);
        _TraUIScripts = UnityHelper.FindTheChildNode(_TranCanvas.gameObject, SysDefine.SYS_NODE_UISCRIPTS);

        //�ѱ��ű���ΪUI����ű��ڵ���ӽڵ�
        gameObject.transform.SetParent(_TraUIScripts, false);

        //��ת������ʱ����֤������"��UI����"
        DontDestroyOnLoad(_TranCanvas);

        //��ʼ��UI����Ԥ���·�����ݣ���д�򵥵ģ�������дjson��
        if (_DicFormsPaths != null)
        {
            InitJsonData(SysDefine.SYS_PATH_JSON_INFO);
        }
    }
    //��ʼ�����أ����ڵ㣩CanvasԤ��
    private void InitRootUI()
    {
        ResourcesMgr.GetInstance().LoadAsset(SysDefine.SYS_ROOT_PA, false);
    }


    #region ��ʾ����

    /// <summary>
    ///��ʾUI����
    ///���ܣ�
    ///1.���ݴ������ƣ����뵽���洢���д����ʵ�������ֵ�
    ///2.����UI���岻ͬ����ʾ���ͣ�����ͬ�ļ��ط�ʽ
    ///3.
    /// </summary>
    /// <param name="uIName"></param>
    public void ShowUIForms(string uIName)
    {

        //�����ļ��
        if (string.IsNullOrEmpty(uIName))
        {
            return;
        }

        //���ݴ������ƣ����뵽���洢���д����ʵ�������ֵ�
        BaseUIForm baseUIForm = LoadFormsToAllDic(uIName);
        //�Ƿ����ջ�е�����  
        if (baseUIForm.currentUIType.isCleanStack)
        {
            CleanStack();
        }

        //����UI���岻ͬ����ʾ���ͣ�����ͬ�ļ��ط�ʽ
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
    //���ݴ������ƣ�������ʵ�����뵽���洢���д����ʵ�������ֵ�
    //�еĻ��Ͳ�������
    private BaseUIForm LoadFormsToAllDic(string UIName)
    {
        ///
        BaseUIForm baseUIForm;
        //����ֵ䲻����UI
        _DicALLForms.TryGetValue(UIName, out baseUIForm);
        if (baseUIForm == null)
        {
            baseUIForm = LoadForms(UIName);
        }
        return baseUIForm;
    }

    /// <summary>
    /// ����ָ�����ƵĴ���
    /// ���ܣ�
    /// 1.���ݡ�UI�������ơ�������Ԥ����
    /// 2.����Ԥ����Ľű���UI����λ�����ͣ�����Ԥ���嵽��Ӧ�ĸ�������ӽڵ�
    /// 3.���ظմ����õĴ���UI
    /// 4.�Ѹմ����Ĵ���UI��ӵ����洢���д����ʵ�������ֵ�
    /// </summary>
    /// <param name="uIName"></param>
    /// <returns></returns>
    private BaseUIForm LoadForms(string uIName)
    {
        string uIPath;
        //���ݴ������Ƶõ�·��
        _DicFormsPaths.TryGetValue(uIName, out uIPath);
        if (string.IsNullOrEmpty(uIPath))
        {
            return null;
        }
        //���ݴ���·��������Ԥ����
        GameObject game = ResourcesMgr.GetInstance().LoadAsset(uIPath, false);

        //���ô���ĸ��ڵ㣨����Ԥ����Ľű���UI����λ�����ͣ�
        if (_TranCanvas != null && game != null)
        {
            //��ȡԤ�����BaseUIForm�ű��е�UI����λ������
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
            //��������
            game.SetActive(false);
            //�Ѹմ����Ĵ���UI��ӵ����洢���д����ʵ�������ֵ�
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
    /// ����ǰ��ʾ�Ĵ���洢����ǰ��ʾ�Ĵ����ֵ���
    /// </summary>
    private void AddToDicCurrent(string uIName)
    {
        BaseUIForm baseUIForm;                                                          //uI�������
        BaseUIForm baseUIFormFromAllForms;                                              //�� _DicALLForms�õ��Ĵ���
        //����ڵ�ǰ��ʾ���ֵ����иô��壬ֱ�ӷ���
        _DicCurrentShowUIForms.TryGetValue(uIName, out baseUIForm);
        if (baseUIForm != null)
        {
            return;
        }

        //�ѵ�ǰ��ʾ�Ĵ���洢����������ʾ�ġ��ֵ���
        _DicALLForms.TryGetValue(uIName, out baseUIFormFromAllForms);
        if (baseUIFormFromAllForms != null)
        {
            _DicCurrentShowUIForms.Add(uIName, baseUIFormFromAllForms);
            //��ʾ�ô���
            baseUIFormFromAllForms.DisPlay();
        }

    }
    /// <summary>
    /// UI������ջ
    /// </summary>
    /// <param name="uIName"></param>
    private void PushUIFormToStack(string uIName)
    {
        BaseUIForm baseUIForm;
        //�ж�ջ������û���������壬�оͽ�ջ��Ԫ�ؽ��ж��ᴦ��
        if (_StaCurrentUIForms.Count > 0)
        {
            _StaCurrentUIForms.Peek().Freeze();
        }
        //�жϴ洢���д����ֵ���û��ָ����Ԫ��
        _DicALLForms.TryGetValue(uIName, out baseUIForm);
        if (baseUIForm != null)
        {
            //���ô����״̬����Ϊ��ʾ״̬
            baseUIForm.DisPlay();
            //����ui�������ջ��
            _StaCurrentUIForms.Push(baseUIForm);
        }
        else
        {
            Debug.Log("baseUIForm==null,Please check");
        }
    }

    /// <summary>
    /// �������������͡��������ʾ
    /// </summary>
    /// <param name="uIName"></param>
    private void EnterAndHidingOther(string uIName)
    {
        BaseUIForm baseUIForm;
        BaseUIForm baseUIFormFromAll;                                           //�Ӽ����еõ���BaseUIForm
        //TODO  �������

        //�жϡ���ǰ��ʾ�����ֵ������޸ô��壬����return
        _DicCurrentShowUIForms.TryGetValue(uIName, out baseUIForm);
        if (baseUIForm != null)
        {
            return;
        }
        //���ء���ǰ��ʾ�����ֵ��ջ�е�UI����
        foreach (BaseUIForm item in _DicCurrentShowUIForms.Values)
        {
            item.Hiding();
        }
        foreach (BaseUIForm stackUI in _StaCurrentUIForms)
        {
            stackUI.Hiding();
        }

        //����UI������ӽ�����ǰ��ʾ�����ֵ���
        _DicALLForms.TryGetValue(uIName, out baseUIFormFromAll);
        if (baseUIFormFromAll != null)
        {
            _DicCurrentShowUIForms.Add(uIName, baseUIFormFromAll);
            //���ô����״̬����Ϊ��ʾ״̬
            baseUIFormFromAll.DisPlay();
        }
    }

    /// <summary>
    /// ���ջ,���ҷ����Ƿ��Ѿ����ջ
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

    #region �رմ���
    public void CloseUIForms(string uIName)
    {
        BaseUIForm baseUIForm;
        //�������
        if (string.IsNullOrEmpty(uIName))
        {
            return;
        }
        //���洢���д����ʵ�������ֵ������޸ô���
        _DicALLForms.TryGetValue(uIName, out baseUIForm);
        if (baseUIForm == null)
        {
            return;
        }
        //���ݴ������ʾ���ͷֱ�����ͬ�Ĺرմ���
        switch (baseUIForm.currentUIType.uIFormsMode)
        {
            //��ͨ���͵�
            case UIFormsMode.Normal:
                ExitUIForm(uIName);
                break;
            //�����л�
            case UIFormsMode.ReverseChange:
                PopUIForm(uIName);
                break;
            //��������
            case UIFormsMode.HideOther:
                ExitUIFormAndDisplayOther(uIName);
                break;
        }
    }

    /// <summary>
    /// �˳�ָ����UI���壨��ͨ���͵��ã�
    /// </summary>
    /// <param name="uIName"></param>
    private void ExitUIForm(string uIName)
    {
        BaseUIForm baseUIForm;
        //�жϡ���ǰ��ʾ�Ĵ��塱���ֵ����޸ô���
        _DicCurrentShowUIForms.TryGetValue(uIName, out baseUIForm);
        if (baseUIForm == null)
        {
            return;
        }
        //�ô���״̬��Ϊ����״̬
        baseUIForm.Hiding();
        //�Ӵ洢����ǰ��ʾ�Ĵ��塱���ֵ����Ƴ��ô���
        _DicCurrentShowUIForms.Remove(uIName);
    }

    /// <summary>
    /// �߱������л���UI������˳�
    /// </summary>
    private void PopUIForm(string uIName)
    {

        if (_StaCurrentUIForms.Count >= 2)
        {
            //��ջ����
            BaseUIForm baseUIForm = _StaCurrentUIForms.Pop();
            //���ô���״̬��������״̬
            baseUIForm.Hiding();
            //��ջ�󣬽���һ������������ʾ״̬
            BaseUIForm topUIForm = _StaCurrentUIForms.Peek();
            topUIForm.ReDisPlay();
        }
        else if (_StaCurrentUIForms.Count == 1)
        {
            //��ջ����
            BaseUIForm baseUIForm = _StaCurrentUIForms.Pop();
            //���ô���״̬��������״̬
            baseUIForm.Hiding();
        }
    }
    /// <summary>
    /// �������������͡�����Ĺر��Ҵ���������
    /// </summary>
    /// <param name="uIName"></param>
    private void ExitUIFormAndDisplayOther(string uIName)
    {
        BaseUIForm baseUIForm;
        //TODO  �������

        //�жϡ���ǰ��ʾ�����ֵ������޸ô��壬����֤���д�����֣�return
        _DicCurrentShowUIForms.TryGetValue(uIName, out baseUIForm);
        if (baseUIForm == null)
        {
            return;
        }
        //����UI�����״̬����Ϊ����״̬�����ӡ���ǰ��ʾ�����ֵ����Ƴ���ȥ
        baseUIForm.Hiding();
        _DicCurrentShowUIForms.Remove(uIName);

        //��ʾ����ǰ��ʾ�����ֵ��ջ�е�UI����
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

    #region ��ʾUIManager�е������ֵ�����ݣ�������ʱʹ��
    /// <summary>
    /// "�洢���д����ʵ��"���ֵ�ĳ���
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
    /// "�洢��ǰ��ʾ���壨������������ͨ����ʵ��"���ֵ�ĳ���
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
    /// "�洢��ǰ��ʾ���壨�����л����͵Ĵ��壩��ʵ��"���ֵ�ĳ���
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
    /// ��JsonPathд��"����Ԥ���·��"���ֵ���
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
