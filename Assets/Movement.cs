using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movingSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float invulnerabilityDuration = 2f; // Duration of invulnerability
    [SerializeField] private float flashInterval = 0.1f; // Interval between sprite flashes

    public Animator animator;
    private float horizontal;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isInvulnerable = false;

    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject object3;
    [SerializeField] private Canvas regularCanvas;

    private int damageCount = 0;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        regularCanvas.enabled = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Flip();

        bool isMoving = Mathf.Abs(horizontal) > 0;
        animator.SetBool("isRunning", isMoving);

        CheckIfGrounded();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        bool isJumping = !isGrounded && rb.velocity.y > 0;
        bool isFalling = !isGrounded && rb.velocity.y < 0;

        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isFalling", isFalling);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * movingSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        if (isGrounded)
        {
            Debug.Log("Player is grounded");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable) // Check if player is invulnerable
        {
            ContactPoint2D[] contactPoints = collision.contacts;

            foreach (ContactPoint2D contact in contactPoints)
            {
                if (contact.normal.y > 0.5f)
                {
                    Debug.Log("Enemy Die");
                    MonsterDetectMovement enemyScript = collision.gameObject.GetComponent<MonsterDetectMovement>();
                    if (enemyScript != null)
                    {
                        enemyScript.TakeDamage();
                    }
                    return;
                }
                else
                {
                    Debug.Log("Damage from enemy");
                    TakeDamage();
                    return;
                }
            }
        }

        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("Damage from water");
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        damageCount++;

        switch (damageCount)
        {
            case 1:
                if (object1 != null)
                {
                    Destroy(object1);
                }
                break;
            case 2:
                if (object2 != null)
                {
                    Destroy(object2);
                }
                break;
            case 3:
                if (object3 != null)
                {
                    Destroy(object3);
                    Destroy(this.gameObject);
                }
                regularCanvas.enabled = false;
                break;
            default:
                break;
        }

        StartCoroutine(ApplyInvulnerability());
    }

    public bool IsInvulnerable
    {
        get { return isInvulnerable; }
    }

    private IEnumerator ApplyInvulnerability()
    {
        isInvulnerable = true;

        // Flash sprite
        float timer = 0f;
        while (timer < invulnerabilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval;
        }

        spriteRenderer.enabled = true;
        isInvulnerable = false;
    }
}
