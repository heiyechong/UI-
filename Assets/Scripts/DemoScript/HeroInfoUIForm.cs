using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoScript
{
    /// <summary>
    /// Ӣ����Ϣ����
    /// </summary>
    public class HeroInfoUIForm : BaseUIForm
    {
        private void Awake()
        {
            base.currentUIType.uIFormType = UIFormType.Fixed;
        }
    }
}