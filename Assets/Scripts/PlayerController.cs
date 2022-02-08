using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    Animator animator;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public int numKeys;
    
    // Update is called once per frame
    void Update()
    {
        animator = GetComponent<Animator>();
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        if(Time.timeScale != 0)
        {
            Vector3 direction = new Vector3(0f, 0f, vertical).normalized;
        
            animator.SetFloat("Speed", direction.magnitude);

            if(direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle,0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                
                if(vertical >= 0)
                    controller.Move(moveDir * speed * Time.deltaTime);
                else
                    controller.Move(moveDir * -speed * Time.deltaTime);
            }
        }
    }

    public void changeNumKeys(int num) 
    {
        numKeys += num;
    }

    public bool hasKeys()
    {
        if(numKeys > 0) 
            return true;
        
        else return false;
    }
}
