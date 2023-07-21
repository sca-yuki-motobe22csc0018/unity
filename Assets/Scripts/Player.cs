using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float jumpHeight=4;
    public float timeToJumpApex=.4f;
    float accelerationTimeAirborne=.2f;
    float accelerationTimeGrounded=.1f;
    float moveSpeed=6;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    // Start is called before the first frame update
    void Start()
    {
        controller=GetComponent<Controller2D>();

        gravity=-(2*jumpHeight)/Mathf.Pow(timeToJumpApex,2);
        jumpVelocity=Mathf.Abs(gravity)*timeToJumpApex;
        print("Gravity: "+gravity+" Jump Velocity: "+jumpVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y=0;
        }

        Vector2 input=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y=jumpVelocity;
        }

        float targetVelocityX=input.x*moveSpeed;
        velocity.x=Mathf.SmoothDamp(velocity.x,targetVelocityX,ref velocityXSmoothing,(controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        velocity.x=input.x*moveSpeed;
        velocity.y+=gravity*Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);

        if (transform.position.y < -10)
        {
            transform.position= new Vector2(transform.position.x, 10);
        }
        if (transform.position.x > 7)
        {
            transform.position= new Vector2(-6, transform.position.y);
        }
        if (transform.position.x < -7)
        {
            transform.position = new Vector2(6, transform.position.y);
        }
    }
    
}
