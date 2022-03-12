using System.Collections;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    [SerializeField] private float moveTime = 0.2f;
    protected Rigidbody2D rb2d;
    private float inverseMoveTime;
    private Coroutine _coroutine = null;

    public LayerMask blockingLayer;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.boxCollider = GetComponent<BoxCollider2D>();
        inverseMoveTime = 1.0f / moveTime;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (float.Epsilon < sqrRemainingDistance)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2d.position, end, inverseMoveTime * Time.fixedDeltaTime);
            rb2d.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
        
        _coroutine = null;
    }
    
    /*  */
    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = (start + new Vector2(xDir, yDir));

        this.boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, this.blockingLayer);

        this.boxCollider.enabled = true;

        if (_coroutine == null && hit.transform == null)
        {
            _coroutine = StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }

    protected virtual void AttemptMove(int xDir, int yDir)
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
