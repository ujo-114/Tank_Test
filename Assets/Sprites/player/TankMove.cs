using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float speed = 10;        //坦克移动速度
    public float angularSpeed = 30; //坦克旋转速度
    //public float number;            //玩家编号
    public AudioClip idleAudio;     //静止声音
    public AudioClip drivingAudio;  //发动声音

    private Rigidbody rigidbody;
    private AudioSource audio;      //声音组件
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();  //获得刚体组件
        audio = this.GetComponent<AudioSource>();      //获得声音组件

    }
    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");  //对应键盘箭头，按下时触发
        rigidbody.velocity = transform.forward * v * speed;  //利用刚体施加速度

        float h = Input.GetAxis("Horizontal");
        rigidbody.angularVelocity = transform.up * h * angularSpeed;

        if (Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1){
            audio.clip = drivingAudio;
            if (audio.isPlaying == false)
                audio.Play();
        }
        else
        {
            audio.clip = idleAudio;
            if (audio.isPlaying == false)
                audio.Play();
        }
    }
}