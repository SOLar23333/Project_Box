using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCreator : MonoBehaviour
{
    GameObject T1LPreb;
    public void Awake()
    {
        T1LPreb = Resources.Load("Assets/Prefabs/T1L.prefab") as GameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        //创建本页中所有关卡按钮
        for (int i = 0; i < 9; i++)
        {
            //创建每一个关卡的按钮单元
            Instantiate(T1LPreb, new Vector3(0, 0, 0), Quaternion.identity);
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
