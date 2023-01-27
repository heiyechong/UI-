using SUIFW;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoScript
{

    //UI面板的基类
    //功能：定义四个生命周期  1.DisPlay显示状态   2.Hiding  隐藏状态 （退出状态）  3.ReDisPlay  再显示状态    4.Freeze 冻结状态（当前窗体不允许动（点击））
    public class BaseUIForm : MonoBehaviour
    {
        //当前窗体是哪种类型
        private UIType _currentUIType = new UIType();

        public UIType currentUIType
        {
            get { return _currentUIType; }
            set { _currentUIType = value; }
        }

        #region 窗体状态方法

        //显示
        public virtual void DisPlay()
        {
            this.gameObject.SetActive(true);
            if (currentUIType.uIFormType ==UIFormType.PopUp)
            {
                UIMaskMgr.GetUIMaskMgr().SetMask(gameObject, currentUIType.uIFormsLucencyType);
            }
        }
        //隐藏
        public virtual void Hiding()
        {
            this.gameObject.SetActive(false);
            if (currentUIType.uIFormType == UIFormType.PopUp)
            {
                UIMaskMgr.GetUIMaskMgr().CancelMask();
            }
        }
        //再显示
        public virtual void ReDisPlay()
        {
            this.gameObject.SetActive(true);
            if (currentUIType.uIFormType == UIFormType.PopUp)
            {
                UIMaskMgr.GetUIMaskMgr().SetMask(gameObject, currentUIType.uIFormsLucencyType);
            }
        }
        //冻结
        public virtual void Freeze()
        {
            this.gameObject.SetActive(true);
        }
        #endregion

        #region 子类常用的方法

        /// <summary>
        /// 给按钮注册事件,注册事件方法
        /// </summary>
        protected void RegisterScript(string childName, EventTriggerListener.VoidDelegate listener)
        {
            //查找按钮节点
            Transform tran = UnityHelper.FindTheChildNode(gameObject, childName);
            //给按钮注册事件方法
            if (tran != null)
            {
                EventTriggerListener.Get(tran.gameObject).onClick = listener;
            }
        }

        /// <summary>
        /// 打开指定窗体
        /// </summary>
        /// <param name="uIName"></param>
        protected void OpenUIForm(string uIName)
        {
            UIManager.GetUIManager().ShowUIForms(uIName);
        }
        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="uIName"></param>
        protected void CloseUIForm()
        {
            string uIName = string.Empty;
            ///GetType():命名空间+类名
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
