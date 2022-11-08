using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScript : MonoBehaviour
{
    /// <summary>
    /// 坦克攻击状态
    /// </summary>
    enum TankeFiringMode
    {
        普通,
        暴走,
    }

    private Transform body;

    float v;//前后
    float h;//左右

    float mouse_x;//左右方向盘

    GameObject MyCamera;

    Texture MyQuasiHeart;

    Transform wheel1;
    Transform wheel2;
    Transform wheel3;
    Transform wheel4;

    AudioSource MyEngine;

    TankeFiringMode myFiringMode;

    Transform MyHead;

    public float MyFiringTime;

    float mouse_y;//上下方向盘

    Transform gatling_A;
    Transform gatling_B;

    Transform MyMuzzle;//炮口
    GameObject MyBullte_A;//子弹

    float TankShootTime = 0;

    Transform GL_Muzzle_A;//格林炮口
    Transform GL_Muzzle_B;//格林炮口

    GameObject MyBullet_Fly;

    public float MySpeed=0.1f;
    public float MySpeedTime = 0;

    private void Awake()
    {
        //MyCamera = GameObject.Find("Main Camera");
        //MyQuasiHeart = Resources.Load<Texture>("Textures/quasiHeart");
     


        ////找轮子
        //wheel1 = FintChid("Wheel1", transform);
        //wheel2 = FintChid("Wheel2", transform);
        //wheel3 = FintChid("Wheel3", transform);
        //wheel4 = FintChid("Wheel4", transform);

        //body = FintChid("Body", transform);

        //MyEngine = transform.GetComponent<AudioSource>();

        //myFiringMode = TankeFiringMode.普通;

        //MyHead = FintChid("Head", transform);

        //gatling_A = FintChid("GatlingA", transform);
        //gatling_B = FintChid("GatlingB", transform);

        //MyMuzzle = FintChid("Muzzle", transform);//炮口
        //MyBullte_A = Resources.Load<GameObject>("Preform/Bullte");

        //GL_Muzzle_A = gatling_A.Find("GMuzzle");
        //GL_Muzzle_B = gatling_B.Find("GMuzzle");
        //MyBullet_Fly = Resources.Load<GameObject>("Eff2/Bullet_FlyP");

    }

    private void OnEnable()
    {
        MyCamera = GameObject.Find("Main Camera");
        MyQuasiHeart = Resources.Load<Texture>("Textures/quasiHeart");

        //找轮子
        wheel1 = FintChid("Wheel1", transform);
        wheel2 = FintChid("Wheel2", transform);
        wheel3 = FintChid("Wheel3", transform);
        wheel4 = FintChid("Wheel4", transform);

        body = FintChid("Body", transform);

        MyEngine = transform.GetComponent<AudioSource>();

        myFiringMode = TankeFiringMode.普通;

        MyHead = FintChid("Head", transform);

        gatling_A = FintChid("GatlingA", transform);
        gatling_B = FintChid("GatlingB", transform);

        MyMuzzle = FintChid("Muzzle", transform);//炮口
        MyBullte_A = Resources.Load<GameObject>("Preform/Bullte");

        GL_Muzzle_A = gatling_A.Find("GMuzzle");
        GL_Muzzle_B = gatling_B.Find("GMuzzle");
        MyBullet_Fly = Resources.Load<GameObject>("Eff2/Bullet_FlyP");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        mouse_x = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up * mouse_x * 2);

        if (MySpeed>0.1f)
        {
            MySpeedTime += Time.deltaTime;
            if (MySpeedTime>=3)
            {
                MySpeedTime = 0;
                MySpeed = 0.1f;
            }
        }

        transform.Translate(h * MySpeed, 0, v * MySpeed, Space.Self);

        if (v != 0 || h != 0)
        {
            wheel1.Rotate(Vector3.right * 10);
            wheel2.Rotate(Vector3.right * 10);
            wheel3.Rotate(Vector3.right * 10);
            wheel4.Rotate(Vector3.right * 10);
            MyEngine.mute = false;
        }
        else
        {
            MyEngine.mute = true;
        }

        #region 轮子方向
        if (h > 0)
        {
            if (v > 0)
            {
                //下体向右上
                body.transform.localRotation = Quaternion.Euler(0, 45, 0);
            }
            else if (v < 0)
            {
                //下体向右下
                body.transform.localRotation = Quaternion.Euler(0, 135, 0);
            }
            else
            {
                //下体向右
                body.transform.localRotation = Quaternion.Euler(0, 90, 0);
            }

        }
        else if (h < 0)
        {
            if (v > 0)
            {
                //下体向左上
                body.transform.localRotation = Quaternion.Euler(0, -45, 0);
            }
            else if (v < 0)
            {
                //下体向左下
                body.transform.localRotation = Quaternion.Euler(0, -135, 0);
            }
            else
            {
                //下体向左
                body.transform.localRotation = Quaternion.Euler(0, -90, 0);
            }


        }
        else if (v < 0)
        {
            //下体向后
            body.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            //下体向前
            body.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        #endregion

        //开火计时器
        if (TankShootTime > 0)
        {
            TankShootTime -= Time.deltaTime;
        }

        switch (myFiringMode)
        {
            case TankeFiringMode.普通:

                MyHead.localRotation = Quaternion.Euler(0, 0, 0);

                //按鼠标开火             
                if (Input.GetMouseButtonDown(0) && TankShootTime <= 0)
                {

                    GameManagerScript.Instance.InstantiateG_Object(MyMuzzle, "P_Bullte", MyBullte_A);
   
                    TankShootTime = 2;//开炮进入两秒冷却

                }             

                break;

            case TankeFiringMode.暴走:

                MyHead.localRotation = Quaternion.Euler(0, 180, 0);

                mouse_y += Input.GetAxis("Mouse Y");//获取鼠标上下 控制格林枪上下
                mouse_y = Mathf.Clamp(mouse_y,180, 210);

                gatling_A.transform.localRotation = Quaternion.AngleAxis(mouse_y, Vector3.right);
                gatling_B.transform.localRotation = Quaternion.AngleAxis(mouse_y, Vector3.right);
                //gatling_A.Rotate(Vector3.right * mouse_y * 2);
                //gatling_B.Rotate(Vector3.right * mouse_y * 2);

                #region 暴走开火
                float fier = Input.GetAxis("Fire1");
                if (fier > 0)
                {
                    gatling_A.Find("Gears").Rotate(Vector3.up * 18, Space.Self);
                    gatling_B.Find("Gears").Rotate(Vector3.up * 18, Space.Self);
                }

                if (Input.GetMouseButtonDown(0))
                {

                    //GL_Muzzle_A
                    GameManagerScript.Instance.InstantiateG_Object(GL_Muzzle_A, "Bullet_FlyP", MyBullet_Fly);
                    //GL_Muzzle_B
                    GameManagerScript.Instance.InstantiateG_Object(GL_Muzzle_B, "Bullet_FlyP", MyBullet_Fly);

                } 
                #endregion

                //暴走时间结束后
                MyFiringTime += Time.deltaTime;
                if (MyFiringTime >= 6)
                {
                    MyFiringTime = 0;
                    myFiringMode = TankeFiringMode.普通;
                    gatling_A.localRotation = Quaternion.Euler(-148, 0, 0);
                    gatling_B.localRotation = Quaternion.Euler(-148, 0, 0);
                }

                break;
            default:
                break;
        }

        //开火

    }


    private void LateUpdate()
    {
        //摄像机跟随
        MyCamera.transform.LookAt(transform.position + new Vector3(0, 1.8f, 0));
        MyCamera.transform.position = Vector3.Lerp(MyCamera.transform.position, transform.position - transform.forward * 5 + new Vector3(0, 2, 0), 0.2f);

    }

    void OnGUI()
    {

        GUI.DrawTexture(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), MyQuasiHeart);

        //if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), "退出"))
        //{
        //    //关闭游戏

        //}  

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

    /// <summary>
    /// 开火模式切换到暴走
    /// </summary>
    public void MyFiringModeSwitch()
    {
        myFiringMode = PlayScript.TankeFiringMode.暴走;
        MyFiringTime = 0;

        gatling_A.localRotation = Quaternion.Euler(180, 0, 0);
        gatling_B.localRotation = Quaternion.Euler(180, 0, 0);

    }

    /// <summary>
    /// 玩家受伤
    /// </summary>
    public void PlayDamage(int atk)
    {
        GameManagerScript.Instance.RecoveryGameObjct(gameObject);
    }


}
