using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player1;    //玩家1的Transform组件
    //public Transform player2;    //玩家2的Transform组件
    public AudioClip tankExplosionAudio;

    private Camera camera;       //camera组件
    private Vector3 offest;      //坦克中心和camera的偏移量
    void Start()
    {
        offest = transform.position - player1.position;  //获取偏移量
        camera = this.GetComponent<Camera>();   //获取camera组件

    }

   // public void TankDamageAudio()
   //{
   //    AudioSource.PlayClipAtPoint(tankExplosionAudio, transform.position);
   //}
    void Update()
    {
        if (player1 == null )   //游戏结束返回
            return;
        transform.position = player1.position  + offest;   //调整camera的位置
        //float ditance = Vector3.Distance(player1.position, player2.position);   //获取坦克间距离
        //if (ditance <= 5f)
            //return;
        //float size = ditance * 0.9f;     //获取size大小
        //camera.orthographicSize = size;  //赋到camera的size属性里

    }
}
