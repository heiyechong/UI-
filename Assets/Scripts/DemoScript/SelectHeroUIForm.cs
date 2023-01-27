using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemoScript
{
    public class SelectHeroUIForm : BaseUIForm
    {
        private void Awake()
        {
            base.currentUIType.uIFormsMode = UIFormsMode.HideOther;

            #region 注册事件
            //进入主城窗体和英雄信息窗体
            RegisterScript("BtnConfirm",
                p =>
                {
                    OpenUIForm("MainCityUIForm");
                    OpenUIForm("HeroInfoUIForm");
                }
            );


            //使用lamda表达式写
            //RegisterScript("BtnReturn", ReturnLoginUIForm); 
            RegisterScript("BtnReturn", p => CloseUIForm());
            #endregion
        }


        /// <summary>
        /// 返回到登录窗体
        /// </summary>
        /// <param name="go"></param>
        private void ReturnLoginUIForm(GameObject go)
        {
            CloseUIForm();
        }
    }
}