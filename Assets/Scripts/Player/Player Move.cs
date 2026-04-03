using UnityEngine;
using ProjectTwo.Manager;

namespace ProjectTwo.Player
{
    public class PlayerMove : MonoBehaviour
    {
        
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float runSpeed = 20f;
        [SerializeField] float rotateSpeed = 20f;
        [SerializeField] float jumpHeight = 0.5f;
        [SerializeField] float groundingForce = -1f;
        [SerializeField] Transform cameraFocus;

        CharacterController characterController;
        Inputs input;
        Animator anim;
        float playerSpeed;
        float gravity = -15f;
        float gravityForce;
        //조준 상태에서의 이동 상태
        public bool isAimingMove = false;

        //문자열을 해싱
        readonly int hashJump = Animator.StringToHash("Jump");
        readonly int hashMoveSpeed = Animator.StringToHash("MoveSpeed");
        
        //내부 초기화를 Awke에서 수행해서 참조 오류 방지
        void Awake()
        {
            characterController = GetComponent<CharacterController>();
            input = GetComponent<Inputs>();
            anim = GetComponent<Animator>(); 
        }

        void Update()
        {
            Move();
            GravityAndJump();
        }

        void GravityAndJump()
        {
            if (characterController.isGrounded == false)
            {
                gravityForce += gravity * Time.deltaTime;           
            }
            else
            {
                gravityForce = groundingForce;

                if (input.jump)
                {
                    anim.SetTrigger(hashJump);
                    //gravity는 음수, 루트 안에 음수가 있으면 안되서 -2f 곱해줌
                    gravityForce = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    input.ResetJump();
                }
            }
        }

        void Move()
        {
            Vector3 direction = new Vector3(input.movement.x, 0, input.movement.y);

            playerSpeed = input.run ? runSpeed : moveSpeed;

            if (isAimingMove)
            {
                playerSpeed = moveSpeed;
            }

            if (direction == Vector3.zero)
            {
                playerSpeed = 0f;
            }
            
            //targetDir을 0,0,0으로 초기화
            Vector3 targetDir = Vector3.zero;
            
            //입력값이 있을 때만 처리하기 위한 조건문
            if (direction != Vector3.zero)
            {
                //카메라의 방향을 기준으로 이동 방향 설정
                Vector3 cameraForward = new Vector3(cameraFocus.forward.x, 0f, cameraFocus.forward.z).normalized;
                Vector3 cameraRight = new Vector3(cameraFocus.right.x, 0f, cameraFocus.right.z).normalized;

                //그걸 위해서는 카메라의 로컬 좌표로 변환하여 적용
                targetDir = cameraForward * direction.z + cameraRight * direction.x;

                //이동하려는 방향을 정면으로 바라보게 하는 코드
                Quaternion targetRot = Quaternion.LookRotation(targetDir, Vector3.up);

                if (!isAimingMove)
                {
                    //현재 회전에서 목표 회전까지 부드럽게 회전하게 해서 자연스럽게 회전하는 움직임 구현
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
                }

            }

            Vector3 velocity = targetDir * playerSpeed + Vector3.up * gravityForce;

            characterController.Move(velocity * Time.deltaTime);
            
            anim.SetFloat(hashMoveSpeed, playerSpeed);

        }
    }
}