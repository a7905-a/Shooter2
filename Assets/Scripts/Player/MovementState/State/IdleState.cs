using UnityEngine;
using ProjectTwo.Manager;

namespace ProjectTwo.Player
{
    public class IdleState : MovementBaseState
    {
        public override void EnterState(PlayerMove movement)
        {

        }

        public override void UpdateState(PlayerMove movement)
        {
            movement.Anim.SetFloat(movement.HashMoveSpeed, movement.PlayerSpeed);
            
            if (movement.Direction != Vector3.zero)
            {
                
                // 달리기 Input키를 누르면 달리기 아니면 걷기
                if (movement.IsRunning)
                {
                    movement.SwitchState(movement.Run);
                }
                else
                {
                    movement.SwitchState(movement.Walk);
                }
            }
        }
    }
}
