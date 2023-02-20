using System;
using BlackBall.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BlackBall
{
    [RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D))]
    public class BallController : CoreBehaviour
    {
        public event Action? Died;
        [SerializeField] private float _acceleration = 2f;
        [SerializeField] private ParticleSystem _deathEffect = null!;
        private PlayerInput _playerInput = null!;
        private Rigidbody2D _rigidbody = null!;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float movementValue = _playerInput.actions["Movement"].ReadValue<Vector2>().x;
            float xVel = 0.9f * _rigidbody.velocity.x + movementValue * _acceleration * Time.deltaTime;
            xVel = Mathf.Clamp(xVel, -2f, 2f);
            _rigidbody.velocity = new Vector2(xVel, _rigidbody.velocity.y);
        }

        public void TriggerDeath()
        {
            Died?.Invoke();
            _deathEffect.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
        }
    }
}