using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    int hp;
    int maxHp;
    Texture hpTexture;
    Texture hpBackgroundTexture;

    GameObject MyBox_Atk;
    GameObject MyBox_Move;

    float MovTime;
    int isMove = 0;

    //敌人4个轮子
    Transform wheel1;
    Transform wheel2;
    Transform wheel3;
    Transform wheel4;

    GameObject enemyTarget;//敌人目标
    Transform head;

    float enemyShootTime;
    GameObject enemyBullte_A;//子弹
    Transform enemyMuzzle;//炮口


    private void OnEnable()
    {
        maxHp = 100;
        hp = maxHp;

        enemyTarget = GameObject.Find("Tank");
        hpTexture = Resources.Load<Texture>("Textures/XueMaie");
        hpBackgroundTexture = Resources.Load<Texture>("Textures/XueUsro");

        MyBox_Atk = Resources.Load<GameObject>("Preform/Atkbox");//爆炸奖励
        MyBox_Move = Resources.Load<GameObject>("Preform/Movbox");//爆炸奖励

        wheel1 = FintChid("Wheel1", transform);
        wheel2 = FintChid("Wheel2", transform);
        wheel3 = FintChid("Wheel3", transform);
        wheel4 = FintChid("Wheel4", transform);


        head = FintChid("Head", transform);

        enemyBullte_A = Resources.Load<GameObject>("Preform/Bullte");
        enemyMuzzle = FintChid("EnemyMuzzle", transform);//炮口
    }

    private void Awake()
    {
      

    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GameManagerScript.Instance.IfWinDestroyG_Objcet(gameObject);

        #region 敌人的移动
        MovTime += Time.deltaTime;
        if (MovTime >= 1)
        {
            MovTime = 0;

            transform.Rotate(Vector3.up * Random.Range(0, 360));

            isMove = Random.Range(0, 100);

        }

        if (isMove > 30)
        {
            transform.Translate(Vector3.forward * 0.1f);
            wheel1.Rotate(Vector3.right * 10);
            wheel2.Rotate(Vector3.right * 10);
            wheel3.Rotate(Vector3.right * 10);
            wheel4.Rotate(Vector3.right * 10);

        }
        #endregion

        //开火计时器
        if (enemyShootTime > 0)
        {
            enemyShootTime -= Time.deltaTime;
        }

        //敌人的攻击
        if (Vector3.Distance(enemyTarget.transform.position, transform.position) <= 18)
        {
            //看向目标
            head.LookAt(enemyTarget.transform);

            //然后开火           
            if (enemyShootTime <= 0)
            {

                GameManagerScript.Instance.InstantiateG_Object(enemyMuzzle, "E_Bullte", enemyBullte_A);
                enemyShootTime = 4;//开炮进入两秒冷却

            }

        }
        else
        {
            head.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

    }

    int hpWide = 8;//血条宽
    int hpHigh = -8;//血条高
    int hpLong = 164;//血条长

    float hpB_thickA = 0.26f;//血条背景上下厚度
    int hpB_thickB = 46;//血条背景左右厚度

    private void OnGUI()
    {
        Vector3 posi = Camera.main.WorldToScreenPoint(transform.position);
        if (hp < maxHp && posi.z > 0)
        {
            //画血条

            GUI.DrawTexture(new Rect(posi.x - ((maxHp / posi.z) * (hpWide + hpB_thickA)) / 2, Screen.height - posi.y + (maxHp / posi.z) * (hpHigh - hpB_thickA), (maxHp / posi.z) * (hpWide + hpB_thickA), (hpLong + hpB_thickB) / posi.z), hpBackgroundTexture);//画血条背景
            GUI.DrawTexture(new Rect(posi.x - (maxHp / posi.z) * hpWide / 2, Screen.height - posi.y + (maxHp / posi.z) * hpHigh, (hp / posi.z) * hpWide, (hpLong / posi.z)), hpTexture);

        }
    }

    /// <summary>
    /// 敌人受伤
    /// </summary>
    public void EnemyDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {

            int num = Random.Range(0, 100);
            if (num < 32)
            {
                GameManagerScript.Instance.InstantiateG_Object(transform, "Atkbox", MyBox_Atk);
            }
            if (num > 72)
            {
                GameManagerScript.Instance.InstantiateG_Object(transform, "Movbox", MyBox_Move);
            }         

            //回收
            GameManagerScript.Instance.RecoveryGameObjct(gameObject);
        }
    }


    /// <summary>
    /// 递归找儿子
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    Transform FintChid(string name, Transform parent)
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
