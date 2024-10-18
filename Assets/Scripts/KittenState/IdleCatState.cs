using UnityEngine;

namespace KittenState
{
    public class IdleCatState : CatState
    {
        
        
        public override void Start(BarkingKitten cat)
        {
            
        }

        public override void Update(BarkingKitten cat)
        {
            float distance = cat.transform.position.x - cat.player.transform.position.x;
            distance = Mathf.Abs(distance); 
            if (distance > 2f)
            {
                cat.SwitchState(new FollowPlayerState());
            }
        }

        public override void End(BarkingKitten cat)
        {
            
        }
    }
}
