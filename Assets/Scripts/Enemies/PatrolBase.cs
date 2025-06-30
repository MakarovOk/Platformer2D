using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(SpriteRenderer))]
    public abstract class PatrolBase : MonoBehaviour
    {
        private const float MinNeededDistance = 0.1f;
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private Transform _pointA;
        [SerializeField] private Transform _pointB;
        [SerializeField] private float _pausePatrolTime = 1f;

        private Transform _currentTargetPoint;
        private SpriteRenderer _spriteRenderer;
        private float _waitTimer;
        private bool _isWaiting;
        private int _directionSign;  
        protected Rigidbody2D _rigidbody2D;

        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _currentTargetPoint = _pointA;
            _isWaiting = false;
            _waitTimer = 0f;

            CalculateDirectionSign();
            FlipSprite();
        }

        protected virtual void Update()
        {
            if (_isWaiting)
                HandleWaiting();
        }

        private void FixedUpdate()
        {
            if (!_isWaiting)
            {
                Movement();
                CheckArrival();
            }
        }

        private void CalculateDirectionSign()
        {
            float directionX = _currentTargetPoint.position.x - transform.position.x;
            _directionSign = directionX >= 0f ? 1 : -1;
        }

        private void Movement() => _rigidbody2D.velocity = new Vector2(_moveSpeed * _directionSign, _rigidbody2D.velocity.y);

        private void CheckArrival()
        {
            if (Mathf.Abs(transform.position.x - _currentTargetPoint.position.x) < MinNeededDistance)
                StartWaiting();
        }

        private void StartWaiting()
        {
            _isWaiting = true;
            _waitTimer = _pausePatrolTime;
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        }

        private void HandleWaiting()
        {
            _waitTimer -= Time.deltaTime;
            if (_waitTimer <= 0f)
                EndWaiting();
        }

        private void EndWaiting()
        {
            _isWaiting = false;
            _currentTargetPoint = _currentTargetPoint == _pointA ? _pointB : _pointA;

            CalculateDirectionSign();
            FlipSprite();
        }

        private void FlipSprite() => _spriteRenderer.flipX = (_directionSign < 0);
    }
}
