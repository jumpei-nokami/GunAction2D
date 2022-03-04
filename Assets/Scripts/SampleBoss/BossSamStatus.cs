using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSamStatus : MonoBehaviour, IDamageAble
{
    private int bHP = 500;
    
    void IDamageAble.AddDamage(int damage)
    {
        bHP -= damage;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("残りHPは" + bHP);
    }
}
