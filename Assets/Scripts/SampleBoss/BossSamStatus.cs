using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSamStatus : MonoBehaviour, IDamageAble
{
    private int bHP = 500;
    
    void IDamageAble.AddDamage(int damage)
    {
        //Debug.Log("ダメージを与える");
        bHP -= damage;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 20, 100, 30),$"EnemyHP:{bHP}");
    }
}
