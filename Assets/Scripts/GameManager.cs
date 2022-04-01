using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerStatus player;
    public BossPlant boss;

    // Start is called before the first frame update
    private void Awake()
    {
        //player = GetComponent<PlayerStatus>();
        //boss = GetComponent<BossPlant>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.mHP <= 0)
        {
            GameOver();
        }
        else if (boss.bHP <= 0)
        {
            GameClear();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameEnd();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }
    
    public void GameEnd()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
    }
}
