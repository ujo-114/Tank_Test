using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkBoxScript : MonoBehaviour {

   

    private void Awake()
    {
       
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameManagerScript.Instance.IfWinDestroyG_Objcet(gameObject);
        transform.Rotate(Vector3.up*10);

	}

    void OnTriggerEnter(Collider collider)
    {

       PlayScript p =  collider.GetComponent<PlayScript>();   

        if (p != null)
        {
            p.MyFiringModeSwitch();
            GameManagerScript.Instance.RecoveryGameObjct(gameObject);
        }    

        //设为失活，回到缓冲池maneger
    }


}
