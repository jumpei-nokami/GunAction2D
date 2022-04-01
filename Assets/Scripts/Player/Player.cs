using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    private enum DirectionState
    {
        None,
        Horizontal,
        Vertical
    }

    public int horizontal;
    public int vertical;
    private DirectionState Dstts;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Dstts = DirectionState.None;
        Debug.Log(rb2d);
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        horizontal = (int) (Input.GetAxisRaw("Horizontal"));
        vertical = (int) (Input.GetAxisRaw("Vertical"));
        
        if (horizontal == 0 && vertical == 0)
        {
            Dstts = DirectionState.None;
            return;
        }
        
        //Debug.Log(Dstts);
        
        if (Input.GetKeyDown("up") || Input.GetKeyDown("down"))
        {
            Dstts = DirectionState.Vertical;
        }
        else if (Input.GetKeyDown("right") || Input.GetKeyDown("left"))
        {
            Dstts = DirectionState.Horizontal;
        }
        
        if (Dstts == DirectionState.Horizontal)
        {
            if (Input.GetKey("right") || Input.GetKey("left"))
            {
                vertical = 0;
            }
            else
            {
                Dstts = DirectionState.Vertical;
            }
        }
        else if (Dstts == DirectionState.Vertical)
        {
            if (Input.GetKey("up") || Input.GetKey("down"))
            {
                horizontal = 0;
            }
            else
            {
                Dstts = DirectionState.Horizontal;
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            base.AttemptMove(horizontal, vertical);
        }
        
    }

    protected override void AttemptMove(int xDir, int yDir)
    {
        base.AttemptMove(xDir, yDir);
        //GameManager.instance.playersTurn = false;
    }
}
