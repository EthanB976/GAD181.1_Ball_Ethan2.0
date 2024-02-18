using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private Collider2D _collider;
    private Vector2 _respawnPoint;

    [SerializeField] private bool _active = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        SetRespawnPoint((Vector2)transform.position);
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

        if (!_active)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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

    private void MiniJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower / 2);
    }

    public void Die()
    {
        _active = false;
        _collider.enabled = false;
        Debug.Log("Die");
        MiniJump();
        StartCoroutine(Respawn());
        
    }

    public void SetRespawnPoint(Vector2 position)
    {
        _respawnPoint = (Vector2)position;
    }

    private IEnumerator Respawn()
    {
        Debug.Log("Respawn");
        yield return new WaitForSeconds(1f);
        transform.position = (Vector2)_respawnPoint;
        _active = true;
        _collider.enabled = true;
        MiniJump();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            SetRespawnPoint((Vector2)transform.position);
        }
    }

}
