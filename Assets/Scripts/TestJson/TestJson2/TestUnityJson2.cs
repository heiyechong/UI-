using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本脚本是测试unity内置json解析使用的
/// </summary>
public class TestUnityJson2 : MonoBehaviour
{
    private void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("People");
        Persons persons = JsonUtility.FromJson<Persons>(textAsset.text);
        foreach (People people in persons.people)
        {
            Debug.Log(people.Name + people.age);
        }
    }
}
