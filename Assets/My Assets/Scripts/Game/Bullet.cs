using NeuroDerby.Extensions;
using UnityEngine;
using Zenject;

namespace NeuroDerby.Game
{
    public class Bullet : MonoBehaviour
    {
        private float _speed;
        private float _damage;
        private Bullet.Pool _bulletPool;
        private bool _isActive;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        [Inject]
        public void Construct(DifficultyConfig difficultyConfig, Bullet.Pool bulletPool)
        {
            _speed = difficultyConfig.BulletSpeed;
            _damage = difficultyConfig.BulletDamage;
            _bulletPool = bulletPool;
            _isActive = false;
        }

        public void Initialize()
        {
            _isActive = false;
        }

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            var playerHealth = collision2D.gameObject.GetComponent<Health>();
            var wall = collision2D.gameObject.GetComponent<Wall>();
            if (playerHealth != null)
            {
                if (!GameState.IsOver)
                    playerHealth.Hurt(_damage);
                _bulletPool.Despawn(this);
            }
            else if (wall != null)
            {
                var point = collision2D.contacts[0];
                var currDirection = transform.TransformDirection(Vector2.up);
                var newDirection = Vector2.Reflect(currDirection, point.normal);
                transform.rotation = Quaternion.FromToRotation(Vector2.up, newDirection);
            }
        }

        private void FixedUpdate()
        {
            if (_isActive)
            {
                transform.position += transform.up * _speed;
            }
            else if (_spriteRenderer.color.a >= 0.99f)
            {
                _isActive = true;
                _collider.enabled = true;
            }
            else
            {
                _spriteRenderer.ChangeAlpha(_spriteRenderer.color.a + 0.01f);
            }
        }

        public class Pool : MonoMemoryPool<Bullet>
        {
            protected override void OnSpawned(Bullet item)
            {
                base.OnSpawned(item);
                var spriteRenderer = item.GetComponent<SpriteRenderer>();
                spriteRenderer.ChangeAlpha(0);
                var collider = item.GetComponent<Collider2D>();
                collider.enabled = false;
            }
        }
    }
}