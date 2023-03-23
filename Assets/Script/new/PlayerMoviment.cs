using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviment : MonoBehaviour
{
    private float velocity = 5;
    private bool isMoving;
    private float rotation;
    private Animator animator;
    private PlayerMovement playerMoviment;

    private Vector2 Chmoviment;
    private Vector3 Chmovimentv3;
    private CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerMoviment = new PlayerMovement();

        playerMoviment.Movement.Walk.started += Onmoviment;

        playerMoviment.Movement.Walk.canceled += Onmoviment;       

        playerMoviment.Movement.Walk.performed += Onmoviment;

        animator = GetComponent<Animator>();    
       
    }
    private void Onmoviment(InputAction.CallbackContext context)
    {
        Chmoviment = context.ReadValue<Vector2>();
        Chmovimentv3.x = Chmoviment.x;
        Chmovimentv3.y = 0f;
        Chmovimentv3.z = Chmoviment.y;
        
    }
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        AnimatonHandler();
        PlayerRotationHandler();
    }
    private void PlayerRotationHandler()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = Chmovimentv3.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = Chmovimentv3.z;
        Quaternion currentRotation = transform.rotation;

        Quaternion rotation;

        if(isMoving)
        {
            Quaternion lookAtRotation = Quaternion.LookRotation(positionToLookAt);

        }
    }
    private void MovePlayer()
    {
        characterController.Move(Chmoviment * Time.deltaTime * velocity);
    }
    private void AnimatonHandler()
    {
        if(!animator.GetBool("iswalking")&& isMoving)
        {
            animator.SetBool("iswalking", true);
        }
        if (animator.GetBool("iswalking") && !isMoving)
        {
            animator.SetBool("iswalking", false);
        }
    }
    private void OnEnable()
    {
        playerMoviment.Movement.Enable();
    }
    private void OnDisable()
    { 
        playerMoviment.Movement.Disable();
    }
}
