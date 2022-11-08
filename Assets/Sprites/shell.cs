using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell : MonoBehaviour
{
    public GameObject shellExplosionPrefab;
    public AudioClip shellExplosionAudio;
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        AudioSource.PlayClipAtPoint(shellExplosionAudio, transform.position);
        GameObject.Instantiate(shellExplosionPrefab, transform.position, transform.rotation);   //实例化爆炸效果
        GameObject.Destroy(this.gameObject);                 //删除子弹
        if (collider.tag == "Enemy")                           //如果碰撞的是“Enemy”
        {
            //发送消息调用函数
            collider.SendMessage("EnemyDamage");
        }
        if (collider.tag == "Player")                           //如果碰撞的是“Player”
        {
            //发送消息调用函数
            collider.SendMessage("TankDamage");
        }
    }
}
