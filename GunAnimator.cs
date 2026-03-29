using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    [Header("Anchors")]
    public Transform spineAnchor;
    public Transform gunHolder;

    [Header("Aim Offset")]
    [SerializeField] private Vector3 aimLocalPosition = new Vector3(-0.002f, 0.321f, 0.056f);
    [SerializeField] private Vector3 aimLocalRotation = new Vector3(359.822f, 0.401f, 98.978f);

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 10f;

    [Header("References")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Camera aimCamera;
    [SerializeField] private Transform nozzle;

    [Header("Debug")]
    [SerializeField] private bool forceAim = false;

    private bool isAiming;
    private static readonly int IsAimingHash = Animator.StringToHash("IsAiming");
    private static readonly int IsShootingHash = Animator.StringToHash("IsShooting");

    private Vector3 spineLocalPos;
    private Quaternion spineLocalRot;

    private float nextFireTime;

    private void Start()
    {
        if (spineAnchor != null)
        {
            transform.SetParent(spineAnchor, false);
            spineLocalPos = transform.localPosition;
            spineLocalRot = transform.localRotation;
        }
    }

    private void Update()
    {
        if (playerAnimator == null) return;

        if (forceAim) playerAnimator.SetBool(IsAimingHash, true);
        bool aimingState = forceAim || playerAnimator.GetBool(IsAimingHash);
        if (aimingState != isAiming)
        {
            isAiming = aimingState;

            if (isAiming && gunHolder != null)
            {
                transform.SetParent(gunHolder, false);
                transform.localPosition = aimLocalPosition;
                transform.localEulerAngles = aimLocalRotation;
            }
            else if (!isAiming && spineAnchor != null)
            {
                transform.SetParent(spineAnchor, false);
                transform.localPosition = spineLocalPos;
                transform.localRotation = spineLocalRot;
            }
        }

        bool isShooting = playerAnimator.GetBool(IsShootingHash);
        if (isShooting && isAiming && bulletPrefab != null && nozzle != null && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Instantiate(bulletPrefab, nozzle.position, nozzle.rotation);
        }
    }
}
