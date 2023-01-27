using SUIFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ��¼����
/// </summary>

namespace DemoScript
{
    public class LoginUIForm : BaseUIForm
    {
        public Text _LoginText;              //��¼ϵͳ������
        public Text _LoginButtonText;          //��¼ϵͳ�ĵ�¼��ť������
        private void Awake()
        {
            //���屾�������������(Ĭ��Ϊ������������������Բ�д)
            base.currentUIType.uIFormType = UIFormType.Normal;
            base.currentUIType.uIFormsMode = UIFormsMode.Normal;
            base.currentUIType.uIFormsLucencyType = UIFormsLucencyType.Lucency;

            //����ť��ӵ���¼�
            //  RegisterScript("OKButton", LoginSys);
            RegisterScript("OKButton", p => OpenUIForm(DemoDefine.DEMO_UINAME_SELECTHEROUIFORM));
        }

        private void Start()
        {
            _LoginText.text = LocalozationMGR.GetInstance().ShowText("LogonSystem");
            _LoginButtonText.text = LocalozationMGR.GetInstance().ShowText("Logon");
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public void LoginSys(GameObject go)
        {
            Debug.Log("��¼�ɹ�");
            //ǰ̨���̨���������˺ŵ���ȷ

            //��ʾ��һ������
            OpenUIForm(DemoDefine.DEMO_UINAME_SELECTHEROUIFORM);
        }
    }
}