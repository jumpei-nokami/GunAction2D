using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAnimationController : MonoBehaviour
{
    Animator animator;
    //public MovingObject movingobject;
    public Player player;

    enum BodyFront
    {
        None,
        Down,
        Up,
        Right,
        Left
    }

    private BodyFront Fstts;
    
    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        // コントローラをセットしたオブジェクトに紐付いている
        // Animatorを取得する
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) {
            /*  */
            Vector2? action = this.actionKeyDown();
            if (action.HasValue) {
                // キー入力があればAnimatorにstateをセットする
                setStateToAnimator(vector: action.Value);
                return;
            }
        }
        /*  */
        // 入力からVector2インスタンスを作成
        Vector2? vector = this.actionKeyDown();

        // キー入力が続いている場合は、入力から作成したVector2を渡す
        // キー入力がなければ null
        setStateToAnimator(vector: vector != Vector2.zero? vector : (Vector2?)null);
        
    }

    /**
     * Animatorに状態をセットする
     *    
     */
    private void setStateToAnimator(Vector2? vector) {
        if (!vector.HasValue) {
            this.animator.speed = 0.0f;
            return;
        }

        //Debug.Log(vector.Value);
        this.animator.speed = 1.0f;
        this.animator.SetFloat("x", vector.Value.x);
        this.animator.SetFloat("y", vector.Value.y);

    }

    /**
     * 特定のキーの入力があればキーにあわせたVector2インスタンスを返す
     * なければnullを返す   
     */
    private Vector2? actionKeyDown()
    {
        /*return Vector2.down;
        return (movingobject.end - movingobject.start);*/
        
        if (player.vertical == 0 && player.horizontal == 0) Fstts = BodyFront.None;
        if (player.vertical == -1) Fstts = BodyFront.Down;
        if (player.vertical == 1) Fstts = BodyFront.Up;
        if (player.horizontal == 1) Fstts = BodyFront.Right;
        if (player.horizontal == -1) Fstts = BodyFront.Left;

        if (Fstts == BodyFront.None)
        {
            return null;
        }
        if (Fstts == BodyFront.Down)
        {
            return Vector2.down;
        }
        if (Fstts == BodyFront.Up)
        {
            return Vector2.up;
        }
        if (Fstts == BodyFront.Right)
        {
            return Vector2.right;
        }
        if (Fstts == BodyFront.Left)
        {
            return Vector2.left;
        }
        return null;
        
    }
    
    
}
