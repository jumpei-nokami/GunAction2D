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
    private float attackInterval = 0.5f;
    [SerializeField]
    private float gunRange = 10f;

    private bool attackTimerIsActive = false;
    private RaycastHit2D hit;
    private WaitForSeconds attackIntervalWait;

    private Vector3 bodyforward;
    // Start is called before the first frame update
    void Start()
    {
        bodyforward = new Vector3(0, -1, 0);
        attackIntervalWait = new WaitForSeconds(attackInterval);
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
        
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("キー押しています");
            GunAttack(bodyforward);
        }

        StartCoroutine(nameof(AttackTimer));
    }

    void GunAttack(Vector3 bodyforward)
    {
        
        if (attackTimerIsActive)
        {
            Debug.Log("撃てません");
            return;
        }
        
        Debug.Log("撃ちました");

        hit = Physics2D.Raycast(transform.position, bodyforward, gunRange, 1 << 7);

        if (hit.collider)
        {
            BulletHit();
        }
    }

    void BulletHit()
    {
        Debug.Log("弾が「" + hit.collider.gameObject.name + "」命中！");
    }

    IEnumerator AttackTimer()
    {
        attackTimerIsActive = true;

        yield return attackIntervalWait;

        attackTimerIsActive = false;
    }
}
