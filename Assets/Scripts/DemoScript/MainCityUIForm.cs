using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoScript
{
    /// <summary>
    /// 主城窗体
    /// </summary>
    public class MainCityUIForm : BaseUIForm
    {
        private void Awake()
        {
            //注册商城按钮点击事件
            RegisterScript("BtnMarket", p => OpenUIForm("MarketUIForm"));
        }


    }
}
