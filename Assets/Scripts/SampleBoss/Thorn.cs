using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    private int damage = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Thorn命中");
        var damageconfirmsed = other.gameObject.GetComponent<IDamageAble>();
        if (damageconfirmsed != null)
        {
            damageconfirmsed.AddDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
