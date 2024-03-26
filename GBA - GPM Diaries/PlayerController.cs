using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    public bool movementAllowed;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    bool facingRight = true;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            animator.SetBool("isMoving", success);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // Set direction of sprite to movement direction
        if (movementInput.x < 0 && facingRight)
        {
            FlipLeft();
        }
        else if (movementInput.x > 0 && !facingRight)
        {
            FlipRight();
        }
    }

    void FlipLeft()
    {
        facingRight = false;
        spriteRenderer.flipX = true;
        animator.SetTrigger("flipLeft");
    }

    void FlipRight()
    {
        facingRight = true;
        spriteRenderer.flipX = false;
        animator.SetTrigger("flipRight");
    }

    private bool TryMove(Vector2 direction)
    {
        if (movementAllowed)
        {
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                direction.magnitude * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
