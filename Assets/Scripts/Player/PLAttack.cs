using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;
using Vector2 = System.Numerics.Vector2;

public class PLAttack : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    private PLAnimationController _plAnimationController;
    
    [SerializeField]
    private float gunInterval = 0.5f;
    [SerializeField]
    private float gunRange = 10f;
    [SerializeField]
    private int gunDamege = 15;
    
    [SerializeField]
    private float knifeInterval = 0.3f;
    [SerializeField]
    private float knifeRange = 1f;
    [SerializeField]
    private int knifeDamege = 5;

    private float attackInterval;
    private bool attackTimerIsActive = false;
    private RaycastHit2D hit;
    private WaitForSeconds attackIntervalWait;

    private Vector3 bodyforward;
    // Start is called before the first frame update
    void Start()
    {
        bodyforward = new Vector3(0, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_plAnimationController.Fstts == PLAnimationController.BodyFront.Down)
        {
            bodyforward = new Vector3(0, -1, 0);
        }
        if (_plAnimationController.Fstts == PLAnimationController.BodyFront.Up)
        {
            bodyforward = new Vector3(0, 1, 0);
        }
        if (_plAnimationController.Fstts == PLAnimationController.BodyFront.Right)
        {
            bodyforward = new Vector3(1, 0, 0);
        }
        if (_plAnimationController.Fstts == PLAnimationController.BodyFront.Left)
        {
            bodyforward = new Vector3(-1, 0, 0);
        }
        
        if (Input.GetKey(KeyCode.Z))
        {
            //Debug.Log("キー押しています");
            attackIntervalWait = new WaitForSeconds(gunInterval);
            Attack(bodyforward, gunRange, gunDamege);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            attackIntervalWait = new WaitForSeconds(knifeInterval);
            Attack(bodyforward, knifeRange, knifeDamege);
        }
    }

    void Attack(Vector3 bodyforward, float range, int damege)
    {
        
        if (attackTimerIsActive)
        {
            Debug.Log("撃てません");
            return;
        }
        
        Debug.Log("撃ちました");

        hit = Physics2D.Raycast(transform.position, bodyforward, range, 1 << 6);

        if (hit.collider)
        {
            BulletHit(damege);
        }
        StartCoroutine(nameof(AttackTimer));
    }

    void BulletHit(int damege)
    {
        Debug.Log("攻撃が「" + hit.collider.gameObject.name + "」命中！");
        Debug.Log(""+ damege +"ダメージを与えた");
    }

    IEnumerator AttackTimer()
    {
        attackTimerIsActive = true;

        yield return attackIntervalWait;

        attackTimerIsActive = false;
    }
}
