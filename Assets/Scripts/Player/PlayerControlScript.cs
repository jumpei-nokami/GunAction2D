using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    // 速度
    public GameObject player;
    Vector3 movePosition;
    public int speed = 5;
    public Vector3 moveX = new Vector3(1, 0, 0);
    public Vector3 moveY = new Vector3(0, 1, 0);
    bool moveButtonJudge;

    // Start is called before the first frame update
    void Start()
    {
        moveButtonJudge = false;
        //player.transform.position == (0.5, 0.5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // 移動処理
        Move();
    }

    void Move()
    {
        if(moveButtonJudge == false)
        {
            // 十字キーを押したときの移動処理
            if (Input.GetKey("left"))
            {
                movePosition = player.transform.position - moveX;
                moveButtonJudge = true;
            }
            if (Input.GetKey("right"))
            {
                movePosition = player.transform.position + moveX;
                moveButtonJudge = true;
            }
            if (Input.GetKey("up"))
            {
                movePosition = player.transform.position + moveY;
                moveButtonJudge = true;
            }
            if (Input.GetKey("down"))
            {
                movePosition = player.transform.position - moveY;
                moveButtonJudge = true;
            }
        }

        player.transform.position = Vector3.MoveTowards(player.transform.position, movePosition, speed * Time.deltaTime);

        if (player.transform.position == movePosition) moveButtonJudge = false;

    }
}
