using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*To dos:
*    - Player Movement
*       - Left and Right
*        - Jump
*        - Morph
*    - Lives 
*/

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    [SerializeField] LayerMask groundLayer;

    [SerializeField] Transform groundCheck;
    
    private PlayerActions playerInputActions;
    [SerializeField] int lives = 9;

    [SerializeField] float moveSpeed = 0f;

    [SerializeField] float jumpSpeed = 0f;

    private float move;

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();

        playerInputActions = new PlayerActions();
        playerInputActions.Gameplay.Enable();
        playerInputActions.Gameplay.Jump.performed += Jump;
        playerInputActions.Gameplay.Move.performed += ctx => move = ctx.ReadValue<float>();
        playerInputActions.Gameplay.Move.canceled += ctx => move = 0;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Player Movement
        rigidBody.velocity = new Vector2(move * moveSpeed,rigidBody.velocity.y);

        //Debug Mode
        Debug.DrawRay(groundCheck.position,Vector2.down*3,Color.red);

    }

    bool IsGrounded() {
    Vector2 position = groundCheck.position;
    Vector2 direction = Vector2.down;
    float distance = 3.0f;
    RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
    if (hit.collider != null) {
        return true;
    }
    
    return false;
    }

    public void Jump(InputAction.CallbackContext context){
        if(context.performed){
            if(IsGrounded()){
                rigidBody.AddForce(new Vector2(0f,jumpSpeed*10));
            }
        }
    }
}
