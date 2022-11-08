using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_attack : MonoBehaviour
{
    public GameObject shellPrefab;           //子弹预制体
    public float shellSpeed = 20;            //子弹发射速度
    public AudioClip shotAudio;
    public float TankShootTime;
    Transform head;
    GameObject Player;


    private Transform firePoint;             //发射点的transform组件
    void Start()
    {
        firePoint = transform.Find("FirePoint");//找到发射点
        head = GetComponent<Transform>();
        Player = GameObject.FindGameObjectWithTag("Player");
        TankShootTime = 2;
    }

    void Update()
    {
        if (TankShootTime > 0)
        {
            TankShootTime -= Time.deltaTime;
        }
        if (Vector3.Distance(Player.transform.position, head.transform.position) <= 15)
        {
            //看向目标
            head.LookAt(Player.transform);
            if (TankShootTime <= 0)
            {
                AudioSource.PlayClipAtPoint(shotAudio, transform.position);
                GameObject go = GameObject.Instantiate(shellPrefab, firePoint.position, firePoint.rotation);   //在发射点实例化（Instantiate）子弹
                go.GetComponent<Rigidbody>().velocity = go.transform.forward * shellSpeed;
                TankShootTime = 2;//开炮进入两秒冷却
            }
        }
        else
        {
            head.transform.localRotation = Quaternion.Euler(0, 0, 0);
            return;
        }
    }
    //用递归方法寻找子项目中的transform组件
    public Transform FintChid(string name, Transform parent)
    {
        if (parent.childCount < 1)
        {
            return null;
        }

        Transform t = parent.transform.Find(name);
        if (t != null)
        {
            return t;
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            t = FintChid(name, parent.GetChild(i));
            if (t != null)
            {
                break;
            }
        }

        return t;
    }
}
