using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform cameraTransform;

    [Header("Debug")]
    [SerializeField] private bool forceAim = false;

    private Animator animator;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsAiming = Animator.StringToHash("IsAiming");
    // private static readonly int IsDodging = Animator.StringToHash("IsDodging");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

        Vector3 inputDirection = (camForward * vertical + camRight * horizontal).normalized;
        bool hasInput = inputDirection.magnitude > 0f;

        bool isRunning = hasInput && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey("joystick button 8"));

        // Debug: detect which joystick button index L2 is using
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick button " + i))
            {
                Debug.Log("Joystick button " + i + " DOWN");
            }
        }

        // Hold-to-aim: true while LMB or L2 (joystick button 4) is held, false when released
        bool isAiming = forceAim || Input.GetMouseButton(0) || Input.GetKey("joystick button 4");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left Mouse Button DOWN for aiming");
        }

        // Optional: log current aiming state (comment out if too spammy)
        // Debug.Log($"IsAiming: {isAiming}");

        animator.SetBool(IsWalking, hasInput);
        animator.SetBool(IsRunning, isRunning);
        animator.SetBool(IsAiming, isAiming);

        // bool isDodging = Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown("joystick button 1");
        // if (isDodging)
        // {
        //     Debug.Log($"IsDodging triggered | Q: {Input.GetKeyDown(KeyCode.Q)} | B Button: {Input.GetKeyDown("joystick button 1")}");
        //     StartCoroutine(DodgeCoroutine());
        // }

        if (hasInput)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // private IEnumerator DodgeCoroutine()
    // {
    //     animator.SetBool(IsDodging, true);
    //     yield return null;
    //     animator.SetBool(IsDodging, false);
    // }
}
