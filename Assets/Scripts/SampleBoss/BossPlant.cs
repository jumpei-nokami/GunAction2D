using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BossPlant : MonoBehaviour, IDamageAble
{
    private int bHP = 500;
    private bool _invincible = false;
    private Coroutine _moving = null;
    private float cooltime;
    private int randomInt;
    System.Random random = new System.Random();
    private GameObject leaf_prefab;

    public GameObject player;
    
    private enum BossState
    {
        First,
        Second,
        Final
    }

    private BossState _bossState = BossState.First;
    
    private enum MovePlant
    {
        None,
        Warp,
        Leaf,
        Thorn,
        Root,
        Whip
    }
    private MovePlant _movePlant = MovePlant.None;
    
    void IDamageAble.AddDamage(int damage)
    {
        /*
        if (_invincible)
        {
            return;
        }*/
        Debug.Log("ダメージを与える");
        bHP -= damage;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        leaf_prefab = (GameObject)Resources.Load("Prefab/Leaf");
    }

    // Update is called once per frame
    void Update()
    {
        if (bHP >= 350)
        {
            cooltime = 2.0f;
        }
        else if (bHP >= 150)
        {
            cooltime = 1.0f;
        }
        else if (bHP > 0)
        {
            _bossState = BossState.Final;
        }
        else
        {
            // 撃破
        }

        if (_moving == null)
        {
            switch (_movePlant)
            {
                case MovePlant.None:
                    _moving = StartCoroutine(WaitMove());
                    break;
                case MovePlant.Warp:
                    _moving = StartCoroutine(WarpMove());
                    break;
                case MovePlant.Leaf:
                    _moving = StartCoroutine(LeafAttack());
                    break;
                case MovePlant.Thorn:
                    _moving = StartCoroutine(ThornAttack());
                    break;
                case MovePlant.Root:
                    _moving = StartCoroutine(RootAttack());
                    break;
                case MovePlant.Whip:
                    _moving = StartCoroutine(WhipAttack());
                    break;
            }
        }
        
    }

    IEnumerator WaitMove()
    {
        Debug.Log(_movePlant);
        yield return new WaitForSeconds(cooltime);
        randomInt = random.Next(0, 9);
        switch (randomInt)
        {
            case 0:
            case 1:
                _movePlant = MovePlant.Warp;
                break;
            case 2:
            case 3:
                _movePlant = MovePlant.Leaf;
                break;
            case 4:
            case 5:
                _movePlant = MovePlant.Thorn;
                break;
            case 6:
                _movePlant = MovePlant.Root;
                break;
            case 7:
            case 8:
                _movePlant = MovePlant.Whip;
                break;
        }
        _moving = null;
    }
    
    IEnumerator WarpMove()
    {
        Debug.Log(_movePlant);
        yield return null;
        _movePlant = MovePlant.None;
        _moving = null;
    }
    
    IEnumerator LeafAttack()
    {
        Debug.Log(_movePlant);
        int spawntimes = 2;
        if (_bossState != BossState.First)
        {
            spawntimes = 3;
        }
        
        for (int i = 0; i < spawntimes; i++)
        {
            randomInt = random.Next(-2, 3);
            int spawn = (int)player.transform.position.x + randomInt;
            if (spawn > 5) spawn = 10 - spawn;
            if (spawn < -5) spawn = -10 - spawn;
            
            GameObject leaf_instance = (GameObject) Instantiate(leaf_prefab, new Vector3(spawn, 2.0f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        _movePlant = MovePlant.None;
        _moving = null;
    }
    
    IEnumerator ThornAttack()
    {
        Debug.Log(_movePlant);
        yield return null;
        _movePlant = MovePlant.None;
        _moving = null;
    }

    IEnumerator RootAttack()
    {
        Debug.Log(_movePlant);
        yield return null;
        _movePlant = MovePlant.None;
        _moving = null;
    }
    
    IEnumerator WhipAttack()
    {
        Debug.Log(_movePlant);
        yield return null;
        _movePlant = MovePlant.None;
        _moving = null;
    }
    
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 20, 100, 30),$"EnemyHP:{bHP}");
    }
}
