using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;

    private float horizontal;
    private float vertical;

    private Vector3 moveDir;

    private Rigidbody rb;
    private Animator animator;

    private bool movementEnabled = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!movementEnabled)
        {
            if (animator != null)
                animator.SetBool("IsWalking", false);

            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        SpeedControl();

        if(animator != null)
            animator.SetBool("IsWalking", horizontal != 0 || vertical != 0);
    }

    private void FixedUpdate()
    {
        if (!movementEnabled)
            return;
        
        MovePlayer();
    }

    //used for move the player
    private void MovePlayer()
    {
        moveDir = orientation.forward * vertical + orientation.right * horizontal;
        rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    //used for limit the player speed
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void EnablePlayerMovement()
    {
        movementEnabled = true;
    }

    public void DisablePlayerMovement()
    {
        movementEnabled = false;
    }
}
