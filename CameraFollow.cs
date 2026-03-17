using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float heightOffset = 2f;
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float smoothSpeed = 5f;

    private float yaw;

    private void Start()
    {
        yaw = transform.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (player == null) return;

        float mouseX = Input.GetAxisRaw("Mouse X") + Input.GetAxisRaw("RightStickX");
        yaw += mouseX * mouseSensitivity;

        Vector3 offset = Quaternion.Euler(0f, yaw, 0f) * new Vector3(0f, 0f, -distance);
        Vector3 targetPosition = player.position + Vector3.up * heightOffset + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(player.position + Vector3.up * heightOffset);
    }
}
