using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��ʾunity�Դ���JsonUtility����Jsonʾ��1
/// </summary>
public class TestUnityJson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Hero hero = new Hero();
        hero.heroName = "Test";
        hero.level = new Level() { level = 1 };
        //����һ��json���л���������-->�ļ���
        string json = JsonUtility.ToJson(hero);
        Debug.Log(json);
        //�������������л��������ļ�-->����
      hero = JsonUtility.FromJson<Hero>(json);
        Debug.Log(hero.heroName+" "+hero.level);
        //�����������Ը��Ƿ����л����
        Hero hero2 = new Hero();
        hero2.heroName = "Test";
        hero2.level = new Level() { level = 1 };
        string json1 = JsonUtility.ToJson(hero2);
        JsonUtility.FromJsonOverwrite(json1, hero);
        Debug.Log(hero.heroName+" "+ hero.level);
    }
}
