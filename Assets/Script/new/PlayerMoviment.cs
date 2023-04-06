using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviment : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInput playerInput;
    private Animator animator;

    private bool a_isWalking;
    private bool a_isRuning;


    Vector2 currentMovimentInput;
    Vector3 currentMoviment;
    bool isMovimentPressed;
    float rotationFactorPerFrame = 10f;

    [SerializeField] private float velocity;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInput.CharacterControls.Move.started += onMovimentInput;
        playerInput.CharacterControls.Move.canceled += onMovimentInput;
        playerInput.CharacterControls.Move.performed += onMovimentInput;



    }

    void onMovimentInput(InputAction.CallbackContext context)
    {
        currentMovimentInput = context.ReadValue<Vector2>();
        currentMoviment.x = currentMovimentInput.x;
        currentMoviment.z = currentMovimentInput.y;
        isMovimentPressed = currentMovimentInput.x != 0 || currentMovimentInput.y != 0;
    }



    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        AnimationHandler();
        RotationHandler();
    }

    private void RotationHandler()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMoviment.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = currentMoviment.z;
        Quaternion currentRotation = transform.rotation;

        Debug.Log(isMovimentPressed);
        if (isMovimentPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void AnimationHandler()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if (isMovimentPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else if (!isMovimentPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void MovePlayer()
    {
        characterController.Move(currentMoviment * Time.deltaTime * velocity);
    }




    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
