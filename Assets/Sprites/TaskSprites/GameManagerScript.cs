using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


enum GameState
{
    等待,
    开始,
    胜利,
    结束,
}


public class GameManagerScript : MonoBehaviour
{

    static public GameManagerScript Instance;

    //private GameObject GrassBlock;//草快
    //private GameObject Clods;//土块
    //private GameObject ground;//地面（父物体）

    //private GameObject MyTrees;
    //private GameObject MyGrass;

    GameObject enemyTank;

    float instantiateEnemyTime = 0;//生成敌人的时间
    float instantiateEnemyCount = 0;//生成敌人的数量
    float instantiateEnemyMaxCount = 1;//生成敌人的最大数量（随关卡改变）

    GameState gameStart;//游戏是否开始

    GameObject MyGround;
    GameObject playTank;

    int fraction = 0;//分数

    GameObject go_parent;

    Texture lost;
    Texture win;
    Texture BJ;

    List<GameObject> list_playBullte_A = new List<GameObject>();
    List<GameObject> enemyBullte = new List<GameObject>();
    List<GameObject> list_eff_A = new List<GameObject>();
    List<GameObject> list_movBox = new List<GameObject>();
    List<GameObject> list_atkBox = new List<GameObject>();
    List<GameObject> list_enemyTank = new List<GameObject>();
    List<GameObject> list_playTank = new List<GameObject>();

    private void Awake()
    {

        DontDestroyOnLoad(gameObject);

        Instance = this;

        //GrassBlock = Resources.Load<GameObject>("Preform/Cube1");
        //Clods = Resources.Load<GameObject>("Preform/Cube2");
        //ground = GameObject.Find("Ground");

        //MyTrees = Resources.Load<GameObject>("Preform/Mytree_1");
        //MyGrass = Resources.Load<GameObject>("Preform/grass01");

        Instance = this;

        enemyTank = Resources.Load<GameObject>("Preform/EnemyTank");

        gameStart = GameState.等待;

        MyGround = Resources.Load<GameObject>("Preform/Ground");
        playTank = Resources.Load<GameObject>("Preform/Tank");

        go_parent = GameObject.Find("GO_Parent");

        lost = Resources.Load<Texture>("Textures/lost");
        win = Resources.Load<Texture>("Textures/win");
        BJ = Resources.Load<Texture>("Textures/2");

    }

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        instantiateEnemyTime += Time.deltaTime;
        if (instantiateEnemyTime >= 3 && instantiateEnemyCount < instantiateEnemyMaxCount && gameStart == GameState.开始)
        {
            instantiateEnemyTime = 0;

            //随机本管理器的位置
            int x = UnityEngine.Random.Range(8, 42);
            int z = UnityEngine.Random.Range(8, 42);
            transform.position = new Vector3(x, transform.position.y, z);

            //生成敌人
            InstantiateG_Object(transform, "EnemyTank", enemyTank);
            instantiateEnemyCount++;


        }

        if (a != null)
        {
            Debug.Log(a.progress);
            if (a.progress == 1)
            {
                a = null;
                Debug.Log("加载完成100%");
                list_playBullte_A.Clear();
                enemyBullte.Clear();
                list_eff_A.Clear();
                list_movBox.Clear();
                list_atkBox.Clear();
                list_enemyTank.Clear();
                list_playTank.Clear();
                go_parent = GameObject.Find("GO_Parent");
                //for (int i = 0; i < transform.childCount; i++)
                //{
                //   Destroy(transform.GetChild(i).gameObject);
                //}
                InstantiateGround();
                fraction = 0;
                instantiateEnemyCount = 0;

                InstantiateG_Object(transform, "Tank", playTank);
                //GameObject go = Instantiate(playTank, new Vector3(8, 8, 8), Quaternion.identity);
                //go.name = "Tank";
                instantiateEnemyTime = 0;

                gameStart = GameState.开始;

            }

        }

    }

    private void OnGUI()
    {

        switch (gameStart)
        {
            case GameState.等待:

                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BJ);

                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height * 0.5f, 100, 50), "开始游戏"))
                {

                    //GameObject go = Instantiate(playTank, new Vector3(8, 8, 8), Quaternion.identity);
                    //go.name = "Tank";
                    InstantiateG_Object(transform, "Tank", playTank);
                    InstantiateGround();
                    gameStart = GameState.开始;

                }

                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height * 0.6f, 100, 50), "退出游戏"))
                {

#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif

                }

                break;
            case GameState.开始:

                GUI.Label(new Rect(80, 50, 100, 50), "分数：" + fraction);

                GUI.Label(new Rect(Screen.width * 0.5f - 50, 50, 100, 50), "第" + instantiateEnemyMaxCount + "关");

                Cursor.visible = false;//隐藏鼠标
                Cursor.lockState = CursorLockMode.Locked;//限制鼠标在屏幕中心（按Ctrl+P播放或关闭游戏）

                break;
            case GameState.胜利:

                Cursor.visible = true;//显示鼠标
                Cursor.lockState = CursorLockMode.None;//不限制鼠标在屏幕中心

                GUI.DrawTexture(new Rect(Screen.width * 0.5f - 200, Screen.height * 0.2f, 400, 200), win);

                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height * 0.6f, 100, 50), "下一关"))
                {
                    instantiateEnemyMaxCount++;
                    StartCoroutine(MyloadScence());

                }

                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height * 0.7f, 100, 50), "退出游戏"))
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                }

                break;
            case GameState.结束:

                Cursor.visible = true;//显示鼠标
                Cursor.lockState = CursorLockMode.None;//不限制鼠标在屏幕中心

                GUI.DrawTexture(new Rect(Screen.width * 0.5f - 200, Screen.height * 0.2f, 400, 200), lost);

                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height * 0.6f, 100, 50), "重新来过"))
                {
                    StartCoroutine(MyloadScence());

                }

                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height * 0.7f, 100, 50), "退出游戏"))
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                }

                break;
            default:
                break;
        }


    }


    /// <summary>
    /// 回收物体到自身
    /// </summary>
    public void RecoveryGameObjct(GameObject go)
    {
        if (go.GetComponent<EnemyScript>())
        {
            fraction++;
            instantiateEnemyCount--;
            if (fraction >= 8)
            {
                gameStart = GameState.胜利;
            }
            list_enemyTank.Add(go);
        }

        if (go.GetComponent<PlayScript>())
        {
            gameStart = GameState.结束;
            list_playTank.Add(go);

        }

        switch (go.name)
        {
            case "E_Bullte":
                enemyBullte.Add(go);
                break;
            case "P_Bullte":
                list_playBullte_A.Add(go);
                break;
            case "Explosion":
                list_eff_A.Add(go);
                break;
            case "Movbox":
                list_movBox.Add(go);
                break;
            case "Atkbox":
                list_atkBox.Add(go);
                break;


            default:
                break;
        }

        go.transform.SetParent(go_parent.transform);
        go.SetActive(false);

        //if (go.GetComponent<ParticleSystem>())
        //{
        //    go.GetComponent<ParticleSystem>().Stop();
        //}
        //else
        //{
        //    go.SetActive(false);
        //}

    }

    ///// <summary>
    ///// 找缓冲池（游戏管理器）里面的儿子
    ///// </summary>
    ///// <param name="name"></param>
    ///// <returns></returns>
    //Transform FindG_Objcet(string name)
    //{
    //    return transform.Find(name);
    //}

    /// <summary>
    /// 实例化游戏物体
    /// </summary>
    /// <param name="t"></param>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void InstantiateG_Object(Transform t, string name, GameObject obj)
    {
        Transform go = null;// = go_parent.transform.Find(name);
        switch (name)
        {
            case "E_Bullte":
                if (enemyBullte.Count > 0)
                {
                    go = enemyBullte[0].transform;
                    enemyBullte.Remove(go.gameObject);
                }
                break;
            case "P_Bullte":
                if (list_playBullte_A.Count > 0)
                {
                    go = list_playBullte_A[0].transform;
                    list_playBullte_A.Remove(go.gameObject);
                }
                break;
            case "Explosion":
                if (list_eff_A.Count > 0)
                {
                    go = list_eff_A[0].transform;
                    list_eff_A.Remove(go.gameObject);
                }
                break;
            case "EnemyTank":
                if (list_enemyTank.Count > 0)
                {
                    go = list_enemyTank[0].transform;
                    list_enemyTank.Remove(go.gameObject);
                }
                break;
            case "Movbox":
                if (list_movBox.Count > 0)
                {
                    go = list_movBox[0].transform;
                    list_movBox.Remove(go.gameObject);
                }
                break;
            case "Atkbox":
                if (list_atkBox.Count > 0)
                {
                    go = list_atkBox[0].transform;
                    list_atkBox.Remove(go.gameObject);
                }
                break;


            default:
                break;
        }

        if (go == null)
        {
            go = Instantiate(obj).transform;
            go.name = name;
        }
        else
        {
            go.parent = null;
        }

        go.position = t.position;
        go.rotation = t.rotation;


        if (t.name == "EnemyMuzzle")
        {
            go.GetComponent<BullteScript>().ShootingSource = MySource.敌人;
        }

        if (t.name == "Muzzle")
        {
            go.GetComponent<BullteScript>().ShootingSource = MySource.玩家;
        }

        //go.gameObject.SetActive(false);
        go.gameObject.SetActive(true);


    }

    /// <summary>
    /// 生成场景
    /// </summary>
    void InstantiateGround()
    {
        Instantiate(MyGround, Vector3.zero, Quaternion.identity);
    }



    public void IfWinDestroyG_Objcet(GameObject go)
    {
        if (gameStart == GameState.胜利)
        {
            Destroy(go);
        }
    }


    AsyncOperation a;
    IEnumerator MyloadScence()
    {
        a = SceneManager.LoadSceneAsync("Scene2");
        yield return a;

    }



}
