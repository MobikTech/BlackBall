using UnityEngine;

namespace BlackBall.Platforms
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlatformMoving : PlatformBase
    {
        public override string GetItemTypeKey => "Moving";
        
        [SerializeField] private float _speed = 0.5f;
        [SerializeField] private string _fieldBorderTag = null!;
        // [SerializeField] private string _ballTag = null!;
        
        private float _direction;
        private Rigidbody2D _rigidbody = null!;
        private readonly Vector2 _multiplierVector = new Vector2(-1f, 1f);

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag(_fieldBorderTag))
            {
                _rigidbody.velocity *= _multiplierVector;
            }
        }
    }
}