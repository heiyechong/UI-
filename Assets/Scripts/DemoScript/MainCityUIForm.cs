using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoScript
{
    /// <summary>
    /// ���Ǵ���
    /// </summary>
    public class MainCityUIForm : BaseUIForm
    {
        private void Awake()
        {
            //ע���̳ǰ�ť����¼�
            RegisterScript("BtnMarket", p => OpenUIForm("MarketUIForm"));
        }


    }
}
