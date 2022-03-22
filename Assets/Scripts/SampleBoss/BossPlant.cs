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
    private GameObject whip_prefab;
    private GameObject Thorn_prefab;
    private GameObject trap_prefab;

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
        Eat,
        Whip
    }
    private MovePlant _movePlant = MovePlant.None;
    
    void IDamageAble.AddDamage(int damage)
    {
        /**/
        if (_invincible)
        {
            return;
        }
        Debug.Log("ダメージを与える");
        bHP -= damage;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        leaf_prefab = (GameObject)Resources.Load("Prefab/Leaf");
        whip_prefab = (GameObject)Resources.Load("Prefab/Whip");
        Thorn_prefab = (GameObject)Resources.Load("Prefab/Thorn");
        trap_prefab = (GameObject)Resources.Load("Prefab/Trap");
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
                case MovePlant.Eat:
                    _moving = StartCoroutine(EatAttack());
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
        Vector3 posPL = player.transform.position;
        Vector3 posEN = this.transform.position;
        float distance = Vector3.Distance(posPL, posEN);
        randomInt = random.Next(0, 10);
        if (distance > 2.0f)
        {
            switch (randomInt)
            {
                case 0:
                case 1:
                    _movePlant = MovePlant.Warp;
                    break;
                case 2:
                case 3:
                    _movePlant = MovePlant.Eat;
                    break;
                case 4:
                case 5:
                case 6:
                    _movePlant = MovePlant.Leaf;
                    break;
                case 7:
                case 8:
                case 9:
                    _movePlant = MovePlant.Whip;
                    break;
            }
        }
        else
        {
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
                    _movePlant = MovePlant.Whip;
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                    _movePlant = MovePlant.Thorn;
                    break;
            }
        }
        _moving = null;
    }
    
    IEnumerator WarpMove()
    {
        Debug.Log(_movePlant);
        int warptimes = random.Next(1, 4);
        for(int i = 0; i <= warptimes; i++)
        {
            StartCoroutine(Warp());
            yield return new WaitForSeconds(0.3f);
        }
        _movePlant = MovePlant.None;
        _moving = null;
    }

    IEnumerator Warp()
    {
        _invincible = true;
        this.GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
        //this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        randomInt = random.Next(-1, 2);
        this.transform.position = new Vector3(randomInt * 3,3);
        yield return new WaitForSeconds(0.1f);
        this.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _invincible = false;
        this.GetComponent<SpriteRenderer>().color = Color.black;
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
            int side = random.Next(0, 2);
            randomInt = random.Next(-1, 2);
            int spawn = (int)player.transform.position.y + randomInt;
            if (spawn > 2) spawn = 2;
            if (spawn < -5) spawn = -5;
            
            GameObject leaf_instance = (GameObject) Instantiate(leaf_prefab, new Vector3(-5.0f + 10 * side, spawn, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        _movePlant = MovePlant.None;
        _moving = null;
    }
    
    IEnumerator ThornAttack()
    {
        Debug.Log(_movePlant);
        this.GetComponent<SpriteRenderer>().color = Color.yellow;
        yield return new WaitForSeconds(0.5f);
        Vector3 spawn = new Vector3((this.transform.position.x), 0.2f, 0);
        GameObject Thorn_instance = (GameObject) Instantiate(Thorn_prefab, spawn, Quaternion.identity);
        this.GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(1f);
        _movePlant = MovePlant.None;
        _moving = null;
    }

    IEnumerator EatAttack()
    {
        Debug.Log(_movePlant);
        Vector3 spawn = new Vector3((int)player.transform.position.x, (int)player.transform.position.y + 2.0f, 0);
        if (spawn.x >= 5) spawn -= new Vector3(0, 1f, 0);
        if (spawn.x <= -5) spawn += new Vector3(0, 1f, 0);
        GameObject trap_instance = (GameObject)Instantiate(trap_prefab, spawn, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        _movePlant = MovePlant.None;
        _moving = null;
    }
    
    IEnumerator WhipAttack()
    {
        Debug.Log(_movePlant);
        int spawntimes = 2;
        if (_bossState != BossState.First)
        {
            spawntimes = 3;
        }

        for (int i = 0; i < spawntimes; i++)
        {
            randomInt = random.Next(-1, 2);
            int spawn = (int)player.transform.position.x + randomInt;
            if (spawn > 5) spawn = 10 - spawn;
            if (spawn < -5) spawn = -10 - spawn;

            GameObject whip_instance = (GameObject)Instantiate(whip_prefab, new Vector3(spawn, 3.0f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        _movePlant = MovePlant.None;
        _moving = null;
    }
    
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 20, 100, 30),$"EnemyHP:{bHP}");
    }
}
