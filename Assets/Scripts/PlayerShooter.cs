using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public static readonly int IdReload = Animator.StringToHash("Reload");
    public Gun gun;

    public Vector3 gunInitPosition;
    public Quaternion gunInitRotation;

    private Rigidbody gunRb;
    private SphereCollider gunCollider;

    private PlayerInput input;
    private Animator animator;

    public Transform leftHandMount;
    public Transform rightHandMount;
    public Transform gunPivot;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        gunRb = gun.GetComponent<Rigidbody>();
        gunCollider = gun.GetComponent<SphereCollider>();

        gunInitPosition = gun.transform.localPosition;
        gunInitRotation = gun.transform.localRotation;
    }

    private void OnEnable()
    {
        gunRb.isKinematic = true;
        gunCollider.enabled = false;

        gun.transform.localPosition = gunInitPosition;
        gun.transform.localRotation = gunInitRotation;
    }

    private void OnDisable()
    {
        gunRb.isKinematic = false;
        gunCollider.enabled = true;
    }

    private void Update()
    {
        if(input.Fire)
        {
            gun.Fire();
        }
        else if(input.Reload)
        {
            if(gun.Reload())
            {
                animator.SetTrigger(IdReload);
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        gunPivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand , 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand , 1f);

        animator.SetIKPosition(AvatarIKGoal.LeftHand , leftHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand , leftHandMount.rotation);

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }
}
