using System.Collections;
using System.Collections.Generic;
using KittenState;
using UnityEngine;

public class FollowPlayerState : CatState
{

    Vector3 playerPosition;
    float direction;
    public override void Start(BarkingKitten cat)
    {
        
        

    }

    public override void Update(BarkingKitten cat)
    {
        playerPosition = cat.player.transform.position;
        direction = playerPosition.x - cat.transform.position.x;
        cat.transform.Translate(Vector3.right * (direction * (Time.deltaTime * 5f)));

        if (Vector3.Distance(playerPosition, cat.transform.position) < 1f)
        {
            cat.SwitchState(new IdleCatState());
        }
    }

    public override void End(BarkingKitten cat)
    {
        
    }
}
