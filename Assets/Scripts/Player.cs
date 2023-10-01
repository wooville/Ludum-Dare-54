using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _dashSpeed;

    private Rigidbody2D _rb;
    private Sprite _sprite;
    private Collider2D _bodyCollider;
    [SerializeField] private Collider2D _footCollider;
    private bool _isGrounded;
    private float _direction;
    private bool _isFacingRight = true;

    private bool _hasDoubleJump;
    private bool _hasDash;
    private bool _hasLight;

    private bool doubleJumped = false;
    private bool dashed = false;
    public delegate void PickupDelegate(Interaction.PICKUPS pickup);
    public static PickupDelegate pickupDelegate;

    private float dashForce;
    private float moveForce;
    private bool applyDash;
    private bool _canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<Sprite>();
        _bodyCollider = GetComponent<Collider2D>();

        _hasDoubleJump = false;
        _hasDash = false;
        _hasLight = false;

        pickupDelegate += CheckPickup;
        DialogueUI.initiateDialogueDelegate += LockCharacterMovement;
        DialogueUI.endDialogueDelegate += UnlockCharacterMovement;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        _direction = Input.GetAxisRaw("Horizontal");

        if (_canMove){
            // Horizontal Movement
            if (!dashed)
            {
                _rb.velocity = new Vector2(_direction * _speed, _rb.velocity.y);
            }
            

            FlipSprite();

            if (Input.GetButtonDown("Jump"))
            {
                if (_footCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
                {
                    Vector2 jumpVelocity = new Vector2(0f, _jumpSpeed);
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
                }
                else if (_hasDoubleJump && !doubleJumped)
                {
                    doubleJumped = true;
                    Vector2 jumpVelocity = new Vector2(0f, _jumpSpeed);
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
                }

            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (_hasDash && !dashed && !_footCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
                {
                    Debug.Log("dashing");

                    dashed = true;
                    applyDash = true;
                    dashForce = (_isFacingRight ? 1f : -1f) * _dashSpeed;
                    Debug.Log(dashForce);
                }
            }
        } else {
            _rb.velocity = Vector2.zero;
        }

        
    }

    private void FixedUpdate() {
        if (applyDash)
        {
            _rb.AddForce(new Vector2(dashForce, 0f));
            applyDash = false;
        }


        if (_footCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            doubleJumped = false;
            dashed = false;
        }
        
        if (_direction != 0 && _canMove){
            _animator.SetBool("isMoving", true);
        } else {
            _animator.SetBool("isMoving", false);
        }
    }

    private void FlipSprite()
    {
        if (_canMove && _isFacingRight && _direction < 0f || !_isFacingRight && _direction > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void CheckPickup(Interaction.PICKUPS pickup){
        switch(pickup){
            case Interaction.PICKUPS.WINGS:
                _hasDash = true;
                Debug.Log(_hasDash);
                break;
            case Interaction.PICKUPS.LIGHT:
                _hasLight = true;
                break;
            case Interaction.PICKUPS.BOOTS:
                _hasDash = true;
                break;
        }
    }
    private void LockCharacterMovement(DialogueObject dialogueObject){
        _canMove = false;
    }

    private void UnlockCharacterMovement(){
        _canMove = true;
    }

}
