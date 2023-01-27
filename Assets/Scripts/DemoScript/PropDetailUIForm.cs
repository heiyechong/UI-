using DemoScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropDetailUIForm : BaseUIForm
{
    public Text text;
    private void Awake()
    {
        currentUIType.uIFormsLucencyType = UIFormsLucencyType.TransLucency;
        currentUIType.uIFormsMode = UIFormsMode.ReverseChange;
        currentUIType.uIFormType = UIFormType.PopUp;

        RegisterScript("Button_Close", p=>CloseUIForm());

        MessageCenter.AddMessageListener("Props", p =>
        {
            text.text = p.Value.ToString(); 
        });
    }
}
