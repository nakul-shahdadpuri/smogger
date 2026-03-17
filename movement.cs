using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        
        if (animator == null)
        {
            Debug.LogError("No Animator component found!");
        }
        else
        {
            Debug.Log("Animator found. Apply Root Motion: " + animator.applyRootMotion);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if W key is being held down using new Input System
        if (Keyboard.current != null && Keyboard.current.wKey.isPressed)
        {
            animator.SetBool("IsWalking", true);
            Debug.Log("IsWalking set to true. Current value: " + animator.GetBool("IsWalking"));
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
