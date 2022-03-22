using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour
{
    [SerializeField] private float chargetime = 1f;
    private int damage = 10;
    private GameObject whip_collider;

    // Start is called before the first frame update
    void Start()
    {
        whip_collider = transform.Find("AttackCollider").gameObject;
        StartCoroutine(AttackArea());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AttackArea()
    {
        yield return new WaitForSeconds(chargetime);
        whip_collider.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Whip命中");
        var damageconfirmsed = other.gameObject.GetComponent<IDamageAble>();
        if (damageconfirmsed != null)
        {
            damageconfirmsed.AddDamage(damage);
        }
        whip_collider.SetActive(false);
    }
}
