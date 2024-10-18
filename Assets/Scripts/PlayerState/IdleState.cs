using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void Start(Player player) { }
    public override void Update(Player player) 
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            player.SwitchState(new WalkState());
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.SwitchState(new CrouchState());
            return;
        }
    }

    public override void End(Player player)
    {
        
    }
}
