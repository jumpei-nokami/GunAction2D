using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, IDamageAble
{
    private int mHP = 100;
    
    void IDamageAble.AddDamage(int damage)
    {
        mHP -= damage;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
