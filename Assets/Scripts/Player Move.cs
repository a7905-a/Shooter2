using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float runSpeed = 20f;
    [SerializeField] float rotateSpeed = 20f;
    [SerializeField] Transform cameraFocus;
    [SerializeField]float jumpHeight = 0.5f;

    CharacterController characterController;
    Inputs input;
    Animator anim;
    float playerSpeed;
    float gravity = -15f;
    float gravityForce;
    public bool isAimingMove = false;

    

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
            gravityForce = -1f;

            if (input.jump)
            {
                anim.SetTrigger("Jump");
                gravityForce = Mathf.Sqrt(jumpHeight * -2f * gravity);

                input.jump = false;
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


        Vector3 targetDir = Vector3.zero;
        if (direction != Vector3.zero)
        {
            Vector3 cameraForward = new Vector3(cameraFocus.forward.x, 0f, cameraFocus.forward.z).normalized;
            Vector3 cameraRight = new Vector3(cameraFocus.right.x, 0f, cameraFocus.right.z).normalized;

            targetDir = cameraForward * direction.z + cameraRight * direction.x;

            Quaternion targetRot = Quaternion.LookRotation(targetDir, Vector3.up);

            if (!isAimingMove)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
            }

            

        }
            Vector3 velocity = targetDir * playerSpeed + Vector3.up * gravityForce;

            characterController.Move(velocity * Time.deltaTime);
        
        anim.SetFloat("MoveSpeed", playerSpeed);

        
        
    }
}