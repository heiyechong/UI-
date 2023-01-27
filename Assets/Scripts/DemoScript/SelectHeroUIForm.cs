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

            #region ע���¼�
            //�������Ǵ����Ӣ����Ϣ����
            RegisterScript("BtnConfirm",
                p =>
                {
                    OpenUIForm("MainCityUIForm");
                    OpenUIForm("HeroInfoUIForm");
                }
            );


            //ʹ��lamda���ʽд
            //RegisterScript("BtnReturn", ReturnLoginUIForm); 
            RegisterScript("BtnReturn", p => CloseUIForm());
            #endregion
        }


        /// <summary>
        /// ���ص���¼����
        /// </summary>
        /// <param name="go"></param>
        private void ReturnLoginUIForm(GameObject go)
        {
            CloseUIForm();
        }
    }
}