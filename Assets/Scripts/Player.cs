using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private bool isCrouching;
    private SpriteRenderer capsule;
    float move;

    public float speed;
    public float jumpSpeed;

    public float crouch;

    //Awake is called before the game even starts.
    void Awake(){
        isCrouching = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       move =Input.GetAxisRaw("Horizontal");
       rb.velocity = new Vector2(move * speed,rb.velocity.y);

       if(Input.GetButtonDown("Jump")&&rb.IsTouchingLayers(3)){
            if(isCrouching){
                this.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                this.transform.position += Vector3.up * crouch;
                speed = speed/crouch;
                isCrouching = false;
            }
            rb.AddForce(new Vector2(0f,jumpSpeed*10));
       }
       else if(Input.GetButtonDown("Crouch")&&rb.IsTouchingLayers(3)){
            if(!isCrouching){
                this.transform.localScale = new Vector3(0.5f,0.25f,1);
                this.transform.position += Vector3.down * crouch;
                isCrouching = true;
                speed = speed*crouch;
            }
            else{
                this.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                this.transform.position += Vector3.up * crouch;
                isCrouching = false;
                speed = speed/crouch;
            }
       }
    }
}
