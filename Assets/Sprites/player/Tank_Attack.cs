using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Attack : MonoBehaviour
{
    public GameObject shellPrefab;           //子弹预制体
    //public KeyCode fireKey = KeyCode.Space;  //定义发射键
    public float shellSpeed = 20;            //子弹发射速度
    public AudioClip shotAudio;
    public float TankShootTime;


    public Transform firePoint;             //发射点的transform组件
    void Start()
    {
        TankShootTime = 0;
    }

    void Update()
    {
        if (TankShootTime > 0)
        {
            TankShootTime -= Time.deltaTime;
        }
        if (Input.GetButton("Fire1") && TankShootTime <= 0)    //按下空格键后
        {
            AudioSource.PlayClipAtPoint(shotAudio, transform.position);
            GameObject go = GameObject.Instantiate(shellPrefab, firePoint.position, firePoint.rotation);   //在发射点实例化（Instantiate）子弹
            go.GetComponent<Rigidbody>().velocity = go.transform.forward * shellSpeed;
            TankShootTime = 0.5f;//开炮进入0.5s冷却
        }
    }
}
