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

            //Ь�ӵ���
            RegisterScript
            ("BtnShoes",
              p =>
                { //���Ӵ���
                    OpenUIForm("PropDetailUIForm");
                    //��������
                    KeyValuesUpdate keyValues = new KeyValuesUpdate("Shoes", "Ь����ϸ��Ϣ");
                    MessageCenter.SendMessage("Props", keyValues);
                }
            );

            //���ӵ���
            RegisterScript("BtnTrousers", p =>
            { //���Ӵ���
                OpenUIForm("PropDetailUIForm");
                //��������
                KeyValuesUpdate keyValues = new KeyValuesUpdate("Trousers", "������ϸ��Ϣ");
                MessageCenter.SendMessage("Props", keyValues);
            });

            //���µ���
            RegisterScript("BtnClothes", p =>
            { //���Ӵ���
                OpenUIForm("PropDetailUIForm");
                //��������
                KeyValuesUpdate keyValues = new KeyValuesUpdate("Clothes", "������ϸ��Ϣ");
                MessageCenter.SendMessage("Props", keyValues);
            });

            //���ȵ���
            RegisterScript("BtnCrutch", p =>
            { //���Ӵ���
                OpenUIForm("PropDetailUIForm");
                //��������
                KeyValuesUpdate keyValues = new KeyValuesUpdate("Crutch", "������ϸ��Ϣ");
                MessageCenter.SendMessage("Props", keyValues);
            });
        }
    }
}