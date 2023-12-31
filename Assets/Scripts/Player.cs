using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private float _doubleJumpSpeed;
    [SerializeField] private float _jumpButtonTime = 0.5f;
    [SerializeField] private float _jumpHeight = 2;
    [SerializeField] private float _jumpCancelGravity = 3f;
    [SerializeField] private float _fallGravity = 2f;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _deathForce;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _oneWayLayer;
    [SerializeField] private bool _hasDoubleJump;
    [SerializeField] private bool _hasDash;
    [SerializeField] private bool _hasLight;
    [SerializeField] private bool _hasKey1;
    [SerializeField] private Collider2D _footCollider;
    private Collider2D _bodyCollider;
    private Rigidbody2D _rb;
    private Sprite _sprite;
    private float _dashTime;
    private float _directionX;

    private float _dashX;
    private float _jumpTime;
    private bool _isGrounded;

    private bool _isFacingRight = false;
    
    private bool _canMove = true;
    private bool _isAlive = true;

    private bool _jumping;
    private bool _jumpCancelled;

    private bool _doubleJumped = false;
    private bool _dashed = false;
    private bool _dashing = false;
    public delegate void PickupDelegate(Interaction.PICKUPS pickup);
    public static PickupDelegate pickupDelegate;
    private RespawnManager respawnManager;
    [SerializeField] private float respawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<Sprite>();
        _bodyCollider = GetComponent<Collider2D>();

        pickupDelegate += CheckPickup;
        DialogueUI.initiateDialogueDelegate += LockCharacterMovement;
        DialogueUI.endDialogueDelegate += UnlockCharacterMovement;
        PickupInfoUI.initiatePickupDelegate += LockCharacterMovement;
        PickupInfoUI.endPickupDelegate += UnlockCharacterMovement;
        EndGameUI.initiateGameEndDelegate += HandleGameEnd;

        SplashScreenUI.startGameDelegate += UnlockCharacterMovement;
        _rb.gravityScale = 0f;
        _canMove = false;

        respawnManager = GameObject.FindObjectOfType<RespawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAlive) return;

        CheckGrounded();
        CheckInput();

        // CheckJumping();


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Death
        if (collision.gameObject.tag == "Enemy" && _isAlive)
        {
            StartCoroutine(Death(collision));
        }
    }

    private IEnumerator Death(Collision2D collision)
    {
        Debug.Log("die");
        _isAlive = false;
        // _animator.SetBool("isDying", true);
        _animator.SetBool("isJumping", false);
        _animator.SetTrigger("died");

        ContactPoint2D contact = collision.contacts[0];

        Vector2 deathDir = (_rb.position - contact.point).normalized;


        _rb.AddForce(deathDir * _deathForce);


        yield return new WaitForSeconds(respawnTimer);
        respawnManager.RespawnPlayer();
        _isAlive = true;
        _animator.SetTrigger("respawn");
        // _animator.SetBool("isDying", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            respawnManager.SetRespawnPoint(other.transform.position);
        }
    }
    public bool getHasLight()
    {
        return _hasLight;
    }

    public bool isAlive()
    {
        return _isAlive;
    }

    public bool isDashing()
    {
        return _dashing;
    }

    public bool isGrounded()
    {
        return _isGrounded;
    }

    private void CheckInput()
    {
        _directionX = Input.GetAxisRaw("Horizontal");

        if (_canMove){
            // float speedDif = targetSpeed - _rb.velocity.x;
            // float movement = speedDif * accelRate;
            float speedDif = (_speed * _directionX) - _rb.velocity.x;
            float movement = speedDif * 1.5f;
            // Horizontal Movement
            if (!_dashed)
            {
                // _rb.velocity = new Vector2(_directionX * _speed, _rb.velocity.y);
                _rb.AddForce(movement * Vector2.right);
            }

            FlipSprite();

            if (Input.GetButtonDown("Jump"))
            {
                if (_isGrounded)
                {
                    // Vector2 jumpVelocity = new Vector2(0f, _jumpSpeed);
                    // _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
                    float jumpForce = Mathf.Sqrt(_jumpHeight * -2 * (Physics2D.gravity.y));
                    _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    _jumping = true;
                    _jumpCancelled = false;
                    _jumpTime = 0;
                    // _animator.SetBool("isJumping", true);
                }
                else if (_hasDoubleJump && !_doubleJumped)
                {
                    _doubleJumped = true;
                    // Vector2 jumpVelocity = new Vector2(0f, _jumpSpeed);
                    _rb.gravityScale = 1f;
                    _rb.velocity = new Vector2(_rb.velocity.x, _doubleJumpSpeed);
                    // _animator.SetBool("isJumping", true);
                    // float jumpForce = Mathf.Sqrt(_jumpHeight * -1 * (Physics2D.gravity.y));
                    // _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }

            }

            if (_jumping){
                _jumpTime += Time.deltaTime;
                if (Input.GetButtonUp("Jump")){
                    _jumpCancelled = true;
                }
                if (_jumpTime > _jumpButtonTime){
                    _jumping = false;
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (_hasDash && !_dashed && !_isGrounded)
                {
                    _dashed = true;
                    _dashing = true;
                    _dashX = (_isFacingRight ? 1f : -1f) * _dashSpeed;
                    _dashTime = 0f;
                }
            }
        } else {
            _rb.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate() {
        if (_dashing)
        {
            // _rb.AddForce(new Vector2(_dashX, 0f));
            _dashTime += Time.deltaTime;
            if (_dashTime < 0.25f){
                _rb.gravityScale = 0f;
                _rb.velocity = new Vector2(_dashX, 0);
            } else {
                _rb.gravityScale = 1f;
                _dashing = false;
            }
        }

        if (_isGrounded)
        {
            _doubleJumped = false;
            _dashed = false;
            _rb.gravityScale = 1f;
            _animator.SetBool("isJumping", false);
        } else {
            _animator.SetBool("isJumping", true);
        }

        if(_jumpCancelled && _jumping && _rb.velocity.y > 0)
        {
            _rb.gravityScale = _jumpCancelGravity;
        } else if (_rb.velocity.y < 0){
            _rb.gravityScale = _fallGravity;
        }
        
        if (_directionX != 0 && _canMove){
            _animator.SetBool("isMoving", true);
        } else {
            _animator.SetBool("isMoving", false);
        }

        _animator.SetFloat("yVelocity", _rb.velocity.y);
    }

    private void FlipSprite()
    {
        if (_canMove && _isFacingRight && _directionX < 0f || !_isFacingRight && _directionX > 0f)
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
                break;
            case Interaction.PICKUPS.LIGHT:
                _hasLight = true;
                break;
            case Interaction.PICKUPS.BOOTS:
                _hasDoubleJump = true;
                break;
            case Interaction.PICKUPS.KEY1:
                _hasKey1 = true;
                BouncerInteractable.soulSwapDialogueDelegate?.Invoke();
                break;
        }
    }
    private void LockCharacterMovement(DialogueObject dialogueObject){
        _rb.gravityScale = 0f;
        _canMove = false;
    }

    private void UnlockCharacterMovement(){
        _rb.gravityScale = 1f;
        _canMove = true;
    }

    private void HandleGameEnd(DialogueObject dialogueObject, EndGameUI.ENDINGS ending){
        _rb.gravityScale = 0f;
        _canMove = false;
    }

    private void CheckGrounded(){
        // _isGrounded = Physics2D.BoxCast(_bodyCollider.bounds.center, _bodyCollider.bounds.size, 0f, Vector2.down, .1f, _groundLayer);
        _isGrounded = _footCollider.IsTouchingLayers(_groundLayer) || _footCollider.IsTouchingLayers(_oneWayLayer);
    }
}
