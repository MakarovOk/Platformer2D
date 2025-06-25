using System;
using Player;
using UnityEngine;

namespace CoinCollect
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
    public class Coin : MonoBehaviour
    {
        public event Action OnCoinCollected;
        private CircleCollider2D _collider2D;

        private void Awake() => Init();

        private void OnDisable() => OnCoinCollected = null;

        private void Init()
        {
            _collider2D = GetComponent<CircleCollider2D>();
            _collider2D.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerMovement _))
            {
                OnCoinCollected?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}