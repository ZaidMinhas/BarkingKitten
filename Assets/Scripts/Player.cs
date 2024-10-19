using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    [SerializeField] BarkingKitten agentBK;

    PlayerControls controls;
    private Rigidbody2D rb;
    private bool isCrouching;

    private bool isHitting;
    [SerializeField] GameObject pivot;

    [SerializeField] GameObject meleeObject;

    Melee melee;

    [SerializeField] Gun gun;
    float move;

    private Vector2 aimInput;

    float angle;

    float speed;
    [SerializeField] float jumpSpeed;

    [SerializeField] float moveSpeed; 

    bool canBark;

    float barkCooldown;

    [SerializeField] float barkCooldownReset;

    [SerializeField] HUD HUD;

    Animator animator;

    SpriteRenderer spriteRenderer;

    bool CrouchHappened;

    //Awake is called before the game even starts.
    void Awake(){
        canBark = true;
        
        isCrouching = false;
        controls = new PlayerControls();

        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        speed = moveSpeed;

        CrouchHappened = false;

        melee = meleeObject.GetComponent<Melee>();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<float>();
        controls.Gameplay.Move.canceled += ctx =>move = 0;

        controls.Gameplay.Jump.performed += ctx => Jump();

        controls.Gameplay.Jump.performed += ctx =>agentBK.Jump();

        controls.Gameplay.Crouch.performed += ctx => Crouch();

        controls.Gameplay.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Aim.canceled += ctx => aimInput = new Vector2(aimInput.x,aimInput.y);

        controls.Gameplay.Call.performed += ctx => Call();

        controls.Gameplay.Shoot.performed += ctx => Shoot();

        controls.Gameplay.Melee.performed += ctx => Melee();

        controls.Gameplay.Bark.performed += ctx => Bark();
    }

    void Jump(){
        if(rb.velocity.y==0){
            if(isCrouching){
                speed = moveSpeed;
                isCrouching = false;
                animator.SetBool("isJumping",true);
                animator.SetBool("isCrouching",false);
            }
            animator.SetBool("isJumping",true);
            rb.AddForce(new Vector2(0f,jumpSpeed*10));
        }
    }

    private void Crouch(){
        if(rb.velocity.y==0){
            if(!isCrouching){
                isCrouching = true;
                speed = 0;
                animator.SetBool("isCrouching",true);
            }
            else{
                isCrouching = false;
                speed = moveSpeed;
                animator.SetBool("isCrouching",false);
            }
       }
    }

    public void movePivotCrouch(){
        if(!CrouchHappened){
            pivot.transform.localPosition = new Vector3(0,pivot.transform.localPosition.y-2.7f,0);
            CrouchHappened = true;
        }
    }

    public void movePivotUnCrouch(){
        if(CrouchHappened){
            pivot.transform.localPosition = new Vector3(0,pivot.transform.localPosition.y+2.7f,0);
            CrouchHappened = false;
        }
    }

    void Call(){
        agentBK.transform.position = this.transform.position;
    }

    void Shoot(){
        gun.Shoot();
    }

    void Melee(){
        //Disable pivot,Enable Melee, Does animation, if enemy tagged object is in front of hitbox, it kills the enemy.
        if(!melee.getisAttacking()){
            pivot.SetActive(false);
            meleeObject.SetActive(true);
            melee.Attack();
        }
    }

    void Bark(){
        if(canBark){
            HUD.barked();
            agentBK.Bark();
            barkCooldown = barkCooldownReset;
            canBark = false;
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
    }

    // Update is called once per frame
    void Update()
    {
    //Movement
        rb.velocity = new Vector2(move * speed,rb.velocity.y);
        if(rb.velocity.x!=0){
            animator.SetBool("isWalking",true);
        }
        else{
            animator.SetBool("isWalking",false);
        }
        if(rb.velocity.y!=0){
                animator.SetBool("isJumping",true);
            }
        else{
                animator.SetBool("isJumping",false);
        }
    //Aiming
        if(Mathf.Abs(aimInput.x)+Mathf.Abs(aimInput.y)>0.9){
            angle = Mathf.Atan2(aimInput.y,aimInput.x) * Mathf.Rad2Deg;
            pivot.transform.rotation = Quaternion.Euler(0,0,angle);
        }
        if(angle>90||angle<-90){
            if(!spriteRenderer.flipX){
                spriteRenderer.flipX = true;
                melee.flipMeleeCapsuleCenter();
            }
        }
        else if(spriteRenderer.flipX){
            spriteRenderer.flipX = false;
            melee.unFlipMeleeCapsuleCenter();
        }

    //BarkCooldown
        if(!canBark){
            barkCooldown -= Time.deltaTime;
            if(barkCooldown<=0){
                HUD.canBark();
                canBark = true;
            }
        }
    //Ending Attack
        if(!melee.getisAttacking()){
            meleeObject.SetActive(false);
            pivot.SetActive(true);
        }
    }
}
