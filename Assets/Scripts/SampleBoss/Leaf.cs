using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public float speed = 4.0f;
    private Rigidbody2D rb2d;
    private int damage = 5;
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (0 - (int)this.transform.position.x >= 0) direction = 1;
        if (0 - (int)this.transform.position.x < 0) direction = -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.MovePosition(transform.position += transform.right * direction * Time.fixedDeltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Leaf命中");
        var damageconfirmsed = other.gameObject.GetComponent<IDamageAble>();
        if (damageconfirmsed != null)
        {
            damageconfirmsed.AddDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
