using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] BarkingKitten agentBK;
    PlayerControls controls;
    private Rigidbody2D rb;
    private bool isCrouching;
    [SerializeField] GameObject pivot;
    float move;

    private Vector2 aimInput;

    float angle;

    public float speed;
    [SerializeField] float jumpSpeed;

    public float crouch;

    //Awake is called before the game even starts.
    void Awake(){
        isCrouching = false;
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<float>();
        controls.Gameplay.Move.canceled += ctx =>move = 0;

        controls.Gameplay.Jump.performed += ctx => Jump();

        controls.Gameplay.Jump.performed += ctx =>agentBK.Jump();

        controls.Gameplay.Crouch.performed += ctx => Crouch();

        controls.Gameplay.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Aim.canceled += ctx => aimInput = new Vector2(aimInput.x,aimInput.y);

        controls.Gameplay.Call.performed += ctx => Call();
    }

    void Jump(){
        if(rb.velocity.y==0){
            if(isCrouching){
                this.transform.localScale = new Vector3(0.4f, 0.3f, 1);
                this.transform.position += Vector3.up * crouch;
                speed = speed/crouch;
                isCrouching = false;
            }
            rb.AddForce(new Vector2(0f,jumpSpeed*10));
        }
    }

    void Crouch(){
        if(rb.velocity.y==0){
            if(!isCrouching){
                this.transform.localScale = new Vector3(0.4f,0.15f,1);
                this.transform.position += Vector3.down * crouch;
                isCrouching = true;
                speed = speed*crouch;
            }
            else{
                this.transform.localScale = new Vector3(0.4f, 0.3f, 1);
                this.transform.position += Vector3.up * crouch;
                isCrouching = false;
                speed = speed/crouch;
            }
       }
    }

    void Call(){
        agentBK.transform.position = this.transform.position;
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
    }

    // Update is called once per frame
    void Update()
    {
    //Movement
    rb.velocity = new Vector2(move * speed,rb.velocity.y);
    //Aiming
    if(Mathf.Abs(aimInput.x)+Mathf.Abs(aimInput.y)>0.9){
        angle = Mathf.Atan2(aimInput.y,aimInput.x) * Mathf.Rad2Deg;
        pivot.transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }
}
