using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {

    private GameObject GrassBlock;//草快
    private GameObject Clods;//土块
    private GameObject ground;//地面（父物体）

    private GameObject MyTrees;
    private GameObject MyGrass;

    private void Awake()
    {
        GrassBlock = Resources.Load<GameObject>("Preform/Cube1");
        Clods = Resources.Load<GameObject>("Preform/Cube2");
        ground = GameObject.Find("Ground(Clone)");

        MyTrees = Resources.Load<GameObject>("Preform/Mytree_1");
        MyGrass = Resources.Load<GameObject>("Preform/grass01");
    }


    private void OnEnable()
    {

        //生成地图
        #region 随机地皮
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                int r = Random.Range(0, 10);
                GameObject go;
               
                if (r > 4)
                {
                    go = Instantiate(GrassBlock);
                    go.transform.position = new Vector3(i, 0, j);
                }
                else
                {
                    go = Instantiate(Clods);
                    go.transform.position = new Vector3(i, 0, j);
                }             

                go.transform.SetParent(ground.transform);

            }
        }
        #endregion

        for (int i = 0; i < 8; i++)
        {
            GameObject go = Instantiate(MyTrees);
            int x = Random.Range(2, 48);
            int z = Random.Range(2, 48);
            int y = Random.Range(-170, 180);
            go.transform.position = new Vector3(x, 1, z);//随机树木位置
            go.transform.rotation = Quaternion.Euler(0, y, 0);//随机树木角度
            go.transform.SetParent(ground.transform);

        }

        for (int i = 0; i < 28; i++)
        {
            GameObject go = Instantiate(MyGrass);
            int x = Random.Range(1, 50);
            int z = Random.Range(1, 50);
            go.transform.position = new Vector3(x, 0.6f, z);
            go.transform.localScale = Vector3.one * 2;
            go.transform.rotation = Quaternion.Euler(0, 0, 0);
            go.transform.SetParent(ground.transform);
        }
        
    }

    // Use this for initialization
    void Start () {
        

    }

    // Update is called once per frame
    void Update () {
		
	}



}
