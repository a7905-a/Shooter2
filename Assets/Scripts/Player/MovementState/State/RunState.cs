using UnityEngine;

namespace ProjectTwo.Player
{
    public class RunState : MovementBaseState
    {
        public override void EnterState(PlayerMove movement)
        {
            
        }

        public override void UpdateState(PlayerMove movement)
        {
            if (movement.Direction == Vector3.zero)
            {
                movement.SwitchState(movement.Idle);
                return;
            }
            movement.Anim.SetFloat(movement.HashMoveSpeed, movement.PlayerSpeed);
        }
    }
}

