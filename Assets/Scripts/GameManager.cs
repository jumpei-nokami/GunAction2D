using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float turnDelay = 0.1f;
    [HideInInspector] public bool playersTurn = true;
    private bool wait;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playersTurn || wait)
        {
            return;
        }

        StartCoroutine(WaitTurn());
    }

    IEnumerator WaitTurn()
    {
        wait = true;
        yield return new WaitForSeconds(turnDelay);
        yield return new WaitForSeconds(turnDelay);
        playersTurn = true;
        wait = false;
    }
}
