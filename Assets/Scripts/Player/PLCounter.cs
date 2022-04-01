using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLCounter : MonoBehaviour
{
    public GameObject player;
    private bool cooltime = true;
    private bool CanCounter = false;
    private MovingObject _movingObject;
    private float movedefault;

    [SerializeField] private float counterInterval = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        _movingObject = GetComponent<MovingObject>();
        movedefault = _movingObject.moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && cooltime)
        {
            StartCoroutine(Counter());
        }
        //Debug.Log(_movingObject.moveTime);
    }

    IEnumerator Counter()
    {
        Debug.Log(cooltime);
        cooltime = false;
        Debug.Log("カウンター");
        Debug.Log(CanCounter);
        if (CanCounter)
        {
            _movingObject.moveTime = 0.1f;
            yield return new WaitForSeconds(0.3f);
            _movingObject.moveTime = movedefault;
        }
        yield return new WaitForSeconds(counterInterval);
        CanCounter = false;
        cooltime = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "CounterArea")
        {
            CanCounter = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "CounterArea")
        {
            CanCounter = false;
        }
    }
}
