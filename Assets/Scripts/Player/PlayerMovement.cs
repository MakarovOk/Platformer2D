using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        private const float GroundCheckDistance = 0.1f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
        [SerializeField] private LayerMask _groundLayer;

        private Rigidbody2D _rb;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider;
        private RaycastHit2D _groundRaycastHit;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            var moveX = Input.GetAxis("Horizontal");
            FlipSprite(moveX);
            Run(moveX);
            if (Input.GetKeyDown(_jumpKey) && IsGrounded())
                Jump();
            _animator.SetBool("IsGround", IsGrounded());
        }

        private void Run(float moveX)
        {
            if (moveX != 0)
                _animator.SetBool("RunLR", true);
            else
                _animator.SetBool("RunLR", false);
            var movement = new Vector2(moveX, transform.position.y);
            _rb.velocity = new Vector2(movement.x * _moveSpeed, _rb.velocity.y);
        }

        private void FlipSprite(float moveX)
        {
            if(moveX != 0)
                _spriteRenderer.flipX = moveX > 0;
        }

        private void Jump()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _animator.SetTrigger("Jump");
        }

        private bool IsGrounded()
        {
            _groundRaycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f,
                Vector2.down, GroundCheckDistance, _groundLayer);
            return _groundRaycastHit.collider;
        }
    }
}