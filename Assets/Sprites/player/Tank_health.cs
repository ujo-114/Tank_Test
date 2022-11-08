using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank_health : MonoBehaviour
{
    public int hp = 100;
    public GameObject tankExplosion;
    public AudioClip tankExplosionAudio;
    private int hpTotal;
    public Slider hpSlider;
    public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        hpTotal = hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TankDamage()
    {
        if (hp <= 0)
        {
            return;
        }
        else hp -= 20;
        hpSlider.value = (float)hp / hpTotal;
        if (hp <= 0)
        {
            AudioSource.PlayClipAtPoint(tankExplosionAudio, Camera.transform.position);
            GameObject.Instantiate(tankExplosion, transform.position + Vector3.up, transform.rotation);
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            AudioSource.PlayClipAtPoint(tankExplosionAudio, Camera.transform.position);
            GameObject.Instantiate(tankExplosion, transform.position + Vector3.up, transform.rotation);
        }
    }
}
