using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ű��ǲ���unity����json����ʹ�õ�
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
