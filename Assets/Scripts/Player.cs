using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerControls controls;
    private Rigidbody2D rb;
    private bool isCrouching;
    private SpriteRenderer capsule;
    float move;

    private Vector2 aimInput;

    public float speed;
    public float jumpSpeed;

    public float crouch;

    //Awake is called before the game even starts.
    void Awake(){
        isCrouching = false;
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<float>();
        controls.Gameplay.Move.canceled += ctx =>move = 0;

        controls.Gameplay.Jump.performed += ctx => Jump();

        controls.Gameplay.Crouch.performed += ctx => Crouch();
    }

    void Jump(){
        if(rb.IsTouchingLayers(3)){
            if(isCrouching){
                this.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                this.transform.position += Vector3.up * crouch;
                speed = speed/crouch;
                isCrouching = false;
            }
            rb.AddForce(new Vector2(0f,jumpSpeed*10));
        }
    }

    void Crouch(){
        if(rb.IsTouchingLayers(3)){
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

    void OnEnable(){
        controls.Gameplay.Enable();
    }

    void OnDisable(){
        controls.Gameplay.Disable();
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
    //Movement
    rb.velocity = new Vector2(move * speed,rb.velocity.y);
    //Aiming
        
    }
}
