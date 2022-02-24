using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLStatusController : MonoBehaviour
{
    private bool isStop;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitSecond(float Time)
    {
        isStop = true;
        yield return new WaitForSeconds(Time);
        isStop = false;
    }
}
