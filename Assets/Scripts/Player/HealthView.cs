using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [DisallowMultipleComponent]
    public sealed class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private float _changFillAnimSpeed = 10f;

        private Health _health;
        private float _targetValue;

        private void Start() => _health.OnHealthChanged += SetTargetToChangeView;

        private void OnDestroy() => _health.OnHealthChanged -= SetTargetToChangeView;

        private void Update()
        {
            if (_healthSlider.value != _targetValue) ChangeFill();
        }

        private void ChangeFill() => _healthSlider.value = Mathf.MoveTowards(_healthSlider.value, _targetValue, _changFillAnimSpeed * Time.deltaTime);

        private void SetTargetToChangeView(int newHealth) => _targetValue = newHealth;

        public void SetHealthController(Health health)
        {
            _health = health;
            _healthSlider.value = _health.CurrentHealth;
            _healthSlider.maxValue = _health.MaxHealth;
        }
    }
}