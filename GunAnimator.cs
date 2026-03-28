using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    [Header("Anchors")]
    public Transform spineAnchor;
    public Transform gunHolder;

    [Header("Aim Offset")]
    [SerializeField] private Vector3 aimLocalPosition = new Vector3(-0.002f, 0.321f, 0.056f);
    [SerializeField] private Vector3 aimLocalRotation = new Vector3(359.822f, 0.401f, 98.978f);

    [Header("References")]
    [SerializeField] private Animator playerAnimator;

    [Header("Debug")]
    [SerializeField] private bool forceAim = false;

    private bool isAiming;
    private static readonly int IsAimingHash = Animator.StringToHash("IsAiming");

    private Vector3 spineLocalPos;
    private Quaternion spineLocalRot;

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
        if (aimingState == isAiming) return;

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
}
