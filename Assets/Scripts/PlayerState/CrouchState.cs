using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : State
{

    public override void Start(Player player)
    {
        player.transform.localScale = Vector3.one;
        player.transform.position += Vector3.down * 0.5f;
    }

    public override void Update(Player player)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.SwitchState(new IdleState());
            return;
        }
        
    }

    public override void End(Player player)
    {
        
        player.transform.localScale = new Vector3(1, 2, 1); 
        player.transform.position += Vector3.up*0.5f;
        
    }
}
