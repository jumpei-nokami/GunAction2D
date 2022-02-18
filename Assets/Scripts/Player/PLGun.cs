using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLGun : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray2D ray = new Ray2D(player.transform.position, player.transform.right);
        
        int layerMask = 1;
        int raydistance = 10;
        
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("キー押しています");
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, layerMask);
            Debug.DrawRay(player.transform.position, player.transform.right * raydistance, Color.green, 1.0f);
        }
    }
}
