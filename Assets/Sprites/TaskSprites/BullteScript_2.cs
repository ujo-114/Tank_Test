using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullteScript_2 : MonoBehaviour {

    float shootTime;

    private void Awake()
    {          
     
    }


    //激活
    private void OnEnable()
    {
        shootTime = 0;
    }

    //沉睡
    private void OnDisable()
    {
        //transform.SetParent(parent.transform);//为何要报错
        //Debug.Log(transform.name);
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 0.6f);

        shootTime += Time.deltaTime;
        if (shootTime >= 1)
        {

            shootTime = 0;
            //GameManagerScript.Instance.RecoveryGameObjct(gameObject);//粒子特效问题 不能回收
            Destroy(gameObject);
            
        }

    }

    //碰到什么时
    private void OnTriggerEnter(Collider collider)
    {
     
        if (!collider.GetComponent<PlayScript>()&&!collider.GetComponent<BullteScript_2>())
        {
            EnemyScript enemy = collider.GetComponent<EnemyScript>();
            if (enemy)
            {
                enemy.EnemyDamage(18);//如果打到敌人就调用敌人的受伤方法 攻击力为8           
            }

            //GameManagerScript.Instance.RecoveryGameObjct(gameObject);//粒子特效问题 不能回收
            Destroy(gameObject);

        }

      
    }

  

}
