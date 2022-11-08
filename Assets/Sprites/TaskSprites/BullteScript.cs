using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MySource
{
    玩家,
    敌人,
}

public class BullteScript : MonoBehaviour
{

    public MySource ShootingSource = MySource.玩家;//发射源

    Material m1;
    Material m2;

    float shootTime;

    GameObject BoomEff;

    int atk;

    private void Awake()
    {
     

    }


    //激活
    private void OnEnable()
    {
        m1 = Resources.Load<Material>("Material/colo3");
        m2 = Resources.Load<Material>("Material/colo4");

        BoomEff = Resources.Load<GameObject>("Eff/Explosion");

        switch (ShootingSource)
        {
            case MySource.玩家:
                transform.GetComponent<MeshRenderer>().material = m1;
                break;
            case MySource.敌人:
                transform.GetComponent<MeshRenderer>().material = m2;
                break;
            default:
                break;
        }
        

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
        switch (ShootingSource)
        {
            case MySource.玩家:
                transform.GetComponent<MeshRenderer>().material = m1;
                break;
            case MySource.敌人:
                transform.GetComponent<MeshRenderer>().material = m2;
                break;
            default:
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        GameManagerScript.Instance.IfWinDestroyG_Objcet(gameObject);

        transform.Translate(Vector3.forward * 0.6f);

        shootTime += Time.deltaTime;
        if (shootTime >= 1)
        {
            shootTime = 0;
            GameManagerScript.Instance.RecoveryGameObjct(gameObject);
        }

    }

    //碰到什么时
    private void OnTriggerEnter(Collider collider)
    {

        switch (ShootingSource)
        {
            case MySource.玩家:

                atk = 100;

                EnemyScript enemy = collider.GetComponent<EnemyScript>();
                if (enemy)
                {
                    enemy.EnemyDamage(atk);
                }

                break;

            case MySource.敌人:
                
                atk = 100;

                PlayScript P = collider.GetComponent<PlayScript>();
                if (P)
                {
                    P.PlayDamage(atk);
                }
                break;

            default:
                break;
        }

        //实例化爆炸特效
        GameManagerScript.Instance.InstantiateG_Object(transform, "Explosion", BoomEff);

        //Transform go = GameManagerScript.Instance.FindG_Objcet("Explosion");
        //if (go == null)
        //{
        //    go = Instantiate(BoomEff).transform;
        //    go.name = "Explosion";
        //}
        //else
        //{
        //    go.parent = null;
        //}

        //go.position = transform.position;
        //go.gameObject.SetActive(true);

        GameManagerScript.Instance.RecoveryGameObjct(gameObject);
    }


}
