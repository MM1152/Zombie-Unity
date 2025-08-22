using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;

    private static readonly int hashMove = Animator.StringToHash("Move"); // 필수구문

    private Animator animator;
    private PlayerInput input;
    private Rigidbody rb;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        var rotate = Quaternion.Euler(0f, input.Rotate * rotateSpeed * Time.deltaTime , 0f);
        rb.MoveRotation(rb.rotation * rotate);

        var delta = input.Move * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + transform.forward * delta);

        animator.SetFloat(hashMove, input.Move);
    }
}
