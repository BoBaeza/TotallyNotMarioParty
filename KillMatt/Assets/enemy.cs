using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;


    // bool onEdgeRight = false;
    // bool onEdgeLeft = false;

    int direction = -1;
    public float movingSpeed;
    public Transform edgeCheckerLeft;
    public Transform edgeCheckerRight;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x * speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        checkEdge();
    }

    void checkEdge()
    {
        Collider2D colliderLeft = Physics2D.OverlapCircle(edgeCheckerLeft.position, checkGroundRadius, groundLayer);
        Collider2D colliderRight = Physics2D.OverlapCircle(edgeCheckerRight.position, checkGroundRadius, groundLayer);

        if (colliderLeft != null && direction == -1) {
            rb.velocity = new Vector2(-movingSpeed,GetComponent<Rigidbody2D>().velocity.y);
        }
        else {
            direction = 1;
            rb.velocity = new Vector2(rb.velocity.x * speed, rb.velocity.y);
        }
        if (colliderRight != null && direction == 1) {
            rb.velocity = new Vector2(movingSpeed,GetComponent<Rigidbody2D>().velocity.y);
        }
        else {
            direction = -1;
            rb.velocity = new Vector2(rb.velocity.x * speed, rb.velocity.y);
        }
    }
}
