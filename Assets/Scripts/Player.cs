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
    
    private PlayerActions playerInputActions;
    [SerializeField] int lives = 9;

    [SerializeField] float moveSpeed = 0f;

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
    }

    public void Move(InputAction.CallbackContext context){
        
    }

    public void Jump(InputAction.CallbackContext context){
        if(context.performed){
            Debug.Log("I am jumping " + context.phase);
        }
    }
}
