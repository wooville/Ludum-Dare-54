using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    private Rigidbody2D _rb;
    private Sprite _sprite;
    private Collider2D _bodyCollider;
    [SerializeField] private Collider2D _footCollider;
    private float _direction;
    private bool _isFacingRight = true;

    private bool _hasDoubleJump;
    private bool _hasDash;
    private bool _hasLight;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<Sprite>();
        _bodyCollider = GetComponent<Collider2D>();

        _hasDoubleJump = false;
        _hasDash = false;
        _hasLight = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        _direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && _footCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            Vector2 jumpVelocity = new Vector2(0f, _jumpSpeed);
            _rb.velocity += jumpVelocity;
        }
    }

    private void FixedUpdate() {
        _rb.velocity = new Vector2(_direction * _speed, _rb.velocity.y);

        FlipSprite();
        
        if (_direction != 0){
            _animator.SetBool("isMoving", true);
        } else {
            _animator.SetBool("isMoving", false);
        }
    }

    private void FlipSprite()
    {
        if (_isFacingRight && _direction < 0f || !_isFacingRight && _direction > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}
