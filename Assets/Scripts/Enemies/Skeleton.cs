using UnityEngine;

namespace Enemies
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public sealed class Skeleton : PatrolBase
    {
        private Animator _animator;
        
        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }
        
        protected override void Update()
        {
            base.Update();
            if(_rigidbody2D.velocity.x != 0)
                _animator.SetBool("IsWalking", true);
            else
                _animator.SetBool("IsWalking", false);
        }
    }
}
