using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_flash : MonoBehaviour
{

    public GameObject enemy;
    public float flashTime = 2f;
    public Transform flashPoint;
    void Start()
    {
        //flashPoint = GetComponent<Transform>();
        //InvokeRepeating("flash", flashTime, flashTime);//固定时间调用flash函数
    }

    // Update is called once per frame
    void Update()
    {
        if (flashTime > 0)
        {
            flashTime -= Time.deltaTime;
        }
        if (flashTime <= 0)    
        {
            GameObject go = GameObject.Instantiate(enemy, flashPoint.position, flashPoint.rotation);   //在发射点实例化（Instantiate）子弹
            
            flashTime = 2;//开炮进入两秒冷却
        }
    }
}
