using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.GetUIManager().ShowUIForms("LoginUIForm");
    }

    
}
