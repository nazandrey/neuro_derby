using UnityEngine;
using Zenject;

namespace NeuroDerby.Game
{
    public class Bullet : MonoBehaviour
    {
        private float _speed;

        [Inject]
        public void Construct(DifficultyConfig difficultyConfig)
        {
            _speed = difficultyConfig.BulletSpeed;
        }
        
        private void FixedUpdate()
        {
            transform.position += transform.up * _speed;
        }

        public class Pool : MonoMemoryPool<Bullet>
        {
        }
    }
}