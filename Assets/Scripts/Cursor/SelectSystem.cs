using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSystem : MonoBehaviour
{
    public int SelectStts = 0;
    public GameStart gameStart;
    public GameEnd gameEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectStts++;
        }

        SelectStts %= 2; 

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (SelectStts == 0)
            {
                gameStart.BattleStart();
            }
            else
            {
                gameEnd.Quit();
            }
        }
    }
    
    
}
