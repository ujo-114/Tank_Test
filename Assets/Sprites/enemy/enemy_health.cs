using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemy_health : MonoBehaviour
{
    public int hp = 100;
    public GameObject tankExplosion;
    public AudioClip tankExplosionAudio;
    private int hpTotal;
    public Slider hpSlider;
    public static int highScore;

    // Start is called before the first frame update
    void Start()
    {
        hpTotal = hp;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void EnemyDamage()
    {
        if (hp <= 0)
        {
            return;
        }
        else hp -= 20;
        if (hp <= 0)
        {
            AudioSource.PlayClipAtPoint(tankExplosionAudio, transform.position);
            GameObject.Instantiate(tankExplosion, transform.position + Vector3.up, transform.rotation);
            GameObject.Destroy(this.gameObject);
            ScoreManager.score += 10;
            if (ScoreManager.score > highScore)
                highScore = ScoreManager.score;
}
    }
}
