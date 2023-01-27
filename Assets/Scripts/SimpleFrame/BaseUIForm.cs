using SUIFW;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoScript
{

    //UI���Ļ���
    //���ܣ������ĸ���������  1.DisPlay��ʾ״̬   2.Hiding  ����״̬ ���˳�״̬��  3.ReDisPlay  ����ʾ״̬    4.Freeze ����״̬����ǰ���岻�������������
    public class BaseUIForm : MonoBehaviour
    {
        //��ǰ��������������
        private UIType _currentUIType = new UIType();

        public UIType currentUIType
        {
            get { return _currentUIType; }
            set { _currentUIType = value; }
        }

        #region ����״̬����

        //��ʾ
        public virtual void DisPlay()
        {
            this.gameObject.SetActive(true);
            if (currentUIType.uIFormType ==UIFormType.PopUp)
            {
                UIMaskMgr.GetUIMaskMgr().SetMask(gameObject, currentUIType.uIFormsLucencyType);
            }
        }
        //����
        public virtual void Hiding()
        {
            this.gameObject.SetActive(false);
            if (currentUIType.uIFormType == UIFormType.PopUp)
            {
                UIMaskMgr.GetUIMaskMgr().CancelMask();
            }
        }
        //����ʾ
        public virtual void ReDisPlay()
        {
            this.gameObject.SetActive(true);
            if (currentUIType.uIFormType == UIFormType.PopUp)
            {
                UIMaskMgr.GetUIMaskMgr().SetMask(gameObject, currentUIType.uIFormsLucencyType);
            }
        }
        //����
        public virtual void Freeze()
        {
            this.gameObject.SetActive(true);
        }
        #endregion

        #region ���ೣ�õķ���

        /// <summary>
        /// ����ťע���¼�,ע���¼�����
        /// </summary>
        protected void RegisterScript(string childName, EventTriggerListener.VoidDelegate listener)
        {
            //���Ұ�ť�ڵ�
            Transform tran = UnityHelper.FindTheChildNode(gameObject, childName);
            //����ťע���¼�����
            if (tran != null)
            {
                EventTriggerListener.Get(tran.gameObject).onClick = listener;
            }
        }

        /// <summary>
        /// ��ָ������
        /// </summary>
        /// <param name="uIName"></param>
        protected void OpenUIForm(string uIName)
        {
            UIManager.GetUIManager().ShowUIForms(uIName);
        }
        /// <summary>
        /// �رյ�ǰ����
        /// </summary>
        /// <param name="uIName"></param>
        protected void CloseUIForm()
        {
            string uIName = string.Empty;
            ///GetType():�����ռ�+����
            uIName = GetType().ToString();
            int index = -1;
            index = uIName.IndexOf('.');
            if (index != -1)
            {
                uIName = uIName.Substring(index + 1);
            }

            UIManager.GetUIManager().CloseUIForms(uIName);
        }
        #endregion
    }
}
