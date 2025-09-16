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

    CharacterController characterController;
    Inputs input;
    Animator anim;
    float playerSpeed;
    float gravity = -9.81f;
    public bool isAimingMove = false;
    float moveDirectionY;

    

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        input = GetComponent<Inputs>();
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {

        Move();

        if (characterController.isGrounded == false)
        {
            moveDirectionY += gravity * Time.deltaTime;
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



        if (direction != Vector3.zero)
        {
            Vector3 cameraForward = new Vector3(cameraFocus.forward.x, 0f, cameraFocus.forward.z).normalized;
            Vector3 cameraRight = new Vector3(cameraFocus.right.x, 0f, cameraFocus.right.z).normalized;

            Vector3 targetDir = cameraForward * direction.z + cameraRight * direction.x;

            Quaternion targetRot = Quaternion.LookRotation(targetDir, Vector3.up);

            if (!isAimingMove)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
            }

            Vector3 velocity = targetDir * playerSpeed + Vector3.up * moveDirectionY;

            characterController.Move(velocity * Time.deltaTime);
            

        }
        
        anim.SetFloat("MoveSpeed", playerSpeed);

        
        
    }
}