using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;
    [HideInInspector] bool facingRight = true;
    bool isGrounded = false;
    float lastTimeGrounded;

    [Header("Jump Settings:")]
    public float jumpForce;
    public float bonusJumpCount;
    private int jumps = 0;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Ground Stuff:")]
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float remeberGroundedFor;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        if (moveBy < 0){
            if (facingRight == true) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                facingRight = false;
            }
        } else if (moveBy > 0){
            if (facingRight == false){
                transform.eulerAngles = new Vector3(0, 0, 0);
                facingRight = true;
            }
        }
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= remeberGroundedFor) && jumps != bonusJumpCount) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumps += 1;
        }
    }

    void BetterJump()
    {
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;

        } else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void CheckIfGrounded() {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if (collider != null) {
            isGrounded = true;
            jumps = 0;
        } else {
            if (isGrounded) {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }

    }
}
