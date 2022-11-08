using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject player;
    Animator anim;
    public Text text;
    public GameObject canvas;
    
    void Start()
    {
        anim = canvas.GetComponent<Animator>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            text.text = "GAME OVER!\n HIAG SCORE:" + enemy_health.highScore  + "\n press the space to replay";
            anim.SetTrigger("gameover");
            if (Input.GetKey(KeyCode.Space))
            {
                Application.LoadLevel(Application.loadedLevel);
                ScoreManager.score = 0;
            }
        }
    }
}
