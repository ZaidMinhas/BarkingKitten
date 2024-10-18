using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    public override void Start(Player player) { }

    public override void Update(Player player)
    {
        float direction = Input.GetAxisRaw("Horizontal");
        
        player.Move(direction);
        
        if (direction == 0)
        {
            player.SwitchState(new IdleState());
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
