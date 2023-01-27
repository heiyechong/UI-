using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 演示unity自带的JsonUtility解析Json示例1
/// </summary>
public class TestUnityJson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Hero hero = new Hero();
        hero.heroName = "Test";
        hero.level = new Level() { level = 1 };
        //方法一：json序列化（将对象-->文件）
        string json = JsonUtility.ToJson(hero);
        Debug.Log(json);
        //方法二：反序列化：（将文件-->对象）
      hero = JsonUtility.FromJson<Hero>(json);
        Debug.Log(hero.heroName+" "+hero.level);
        //方法三：测试覆盖反序列化输出
        Hero hero2 = new Hero();
        hero2.heroName = "Test";
        hero2.level = new Level() { level = 1 };
        string json1 = JsonUtility.ToJson(hero2);
        JsonUtility.FromJsonOverwrite(json1, hero);
        Debug.Log(hero.heroName+" "+ hero.level);
    }
}
