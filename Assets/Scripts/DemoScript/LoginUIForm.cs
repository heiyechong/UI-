using SUIFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 登录窗体
/// </summary>

namespace DemoScript
{
    public class LoginUIForm : BaseUIForm
    {
        public Text _LoginText;              //登录系统的名称
        public Text _LoginButtonText;          //登录系统的登录按钮的名称
        private void Awake()
        {
            //定义本窗体的属性类型(默认为这个，如果是这个，可以不写)
            base.currentUIType.uIFormType = UIFormType.Normal;
            base.currentUIType.uIFormsMode = UIFormsMode.Normal;
            base.currentUIType.uIFormsLucencyType = UIFormsLucencyType.Lucency;

            //给按钮添加点击事件
            //  RegisterScript("OKButton", LoginSys);
            RegisterScript("OKButton", p => OpenUIForm(DemoDefine.DEMO_UINAME_SELECTHEROUIFORM));
        }

        private void Start()
        {
            _LoginText.text = LocalozationMGR.GetInstance().ShowText("LogonSystem");
            _LoginButtonText.text = LocalozationMGR.GetInstance().ShowText("Logon");
        }
        /// <summary>
        /// 登录方法
        /// </summary>
        public void LoginSys(GameObject go)
        {
            Debug.Log("登录成功");
            //前台或后台检测密码和账号的正确

            //显示下一个窗体
            OpenUIForm(DemoDefine.DEMO_UINAME_SELECTHEROUIFORM);
        }
    }
}