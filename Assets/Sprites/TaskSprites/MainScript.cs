using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {

    static bool isHaveManeger = false;

    GameObject MyManager;

   // GameObject target;

	// Use this for initialization
	void Start () {

        MyManager = Resources.Load<GameObject>("Preform/GameManager");

       // target = GameObject.Find("Tank");

        if (isHaveManeger==false)
        {
            Instantiate(MyManager);
            isHaveManeger = true;
        }


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void LateUpdate()
    //{
    //    if (!target)
    //    {
    //        //摄像机跟随
    //        Debug.Log(transform.name);
    //        transform.LookAt(transform.position + new Vector3(0, 1.8f, 0));
    //        transform.position = Vector3.Lerp(transform.position, transform.position - transform.forward * 5 + new Vector3(0, 2, 0), 0.2f);
    //    }

      

    //}

}
