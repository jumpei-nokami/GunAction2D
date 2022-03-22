using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float chargetime = 1.5f;
    private int damage = 20;
    private GameObject trap_collider;

    // Start is called before the first frame update
    void Start()
    {
        trap_collider = transform.Find("AttackCollider").gameObject;
        StartCoroutine(AttackArea());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AttackArea()
    {
        yield return new WaitForSeconds(chargetime);
        trap_collider.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trap命中");
        var damageconfirmsed = other.gameObject.GetComponent<IDamageAble>();
        if (damageconfirmsed != null)
        {
            damageconfirmsed.AddDamage(damage);
        }
        trap_collider.SetActive(false);
    }
}
