using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    public float velocity = 5;
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * velocity;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * velocity;
        if(moveX != 0 || moveZ != 0)
        {
            animator.SetBool("Iswalking", true);
        }
        else if(moveX == 0 && moveZ == 0)
        {
            animator.SetBool("Iswalking", false);
        }
        transform.Translate(moveX, 0, moveZ);
      
    }
}
