using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed;
    public float distance;
    private bool movingRight = true;
    public Transform groundDetection;

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);  // start moving sprite to the right

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);  // ray for detecting edge
        if (groundInfo.collider == false){  // if the collider does not detect any ground beneath it...
            if (movingRight == true){  // ...and the sprite is currwently moving to the right...
                transform.eulerAngles = new Vector3(0, -180, 0);  // ...flip sprite around...
                movingRight = false;  // ...and start moving left
            } else { //...and the sprite is currently moving left...
                transform.eulerAngles = new Vector3(0, 0, 0);  // ...flip the sprite...
                movingRight = true;  // ...and start moving right
            }
        }
    }

}
