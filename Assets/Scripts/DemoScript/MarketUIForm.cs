using DemoScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoScript
{
    public class MarketUIForm : BaseUIForm
    {
        private void Awake()
        {
            base.currentUIType.uIFormType = UIFormType.PopUp;
            currentUIType.uIFormsLucencyType = UIFormsLucencyType.TransLucency;
            base.currentUIType.uIFormsMode = UIFormsMode.ReverseChange;
            RegisterScript("Button_Close", p => CloseUIForm());

            //鞋子道具
            RegisterScript
            ("BtnShoes",
              p =>
                { //打开子窗体
                    OpenUIForm("PropDetailUIForm");
                    //传递数据
                    KeyValuesUpdate keyValues = new KeyValuesUpdate("Shoes", "鞋子详细信息");
                    MessageCenter.SendMessage("Props", keyValues);
                }
            );

            //裤子道具
            RegisterScript("BtnTrousers", p =>
            { //打开子窗体
                OpenUIForm("PropDetailUIForm");
                //传递数据
                KeyValuesUpdate keyValues = new KeyValuesUpdate("Trousers", "裤子详细信息");
                MessageCenter.SendMessage("Props", keyValues);
            });

            //上衣道具
            RegisterScript("BtnClothes", p =>
            { //打开子窗体
                OpenUIForm("PropDetailUIForm");
                //传递数据
                KeyValuesUpdate keyValues = new KeyValuesUpdate("Clothes", "上衣详细信息");
                MessageCenter.SendMessage("Props", keyValues);
            });

            //拐杖道具
            RegisterScript("BtnCrutch", p =>
            { //打开子窗体
                OpenUIForm("PropDetailUIForm");
                //传递数据
                KeyValuesUpdate keyValues = new KeyValuesUpdate("Crutch", "神杖详细信息");
                MessageCenter.SendMessage("Props", keyValues);
            });
        }
    }
}