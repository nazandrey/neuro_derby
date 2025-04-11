using UnityEngine;

namespace NeuroDerby.Game
{
    [CreateAssetMenu(fileName = "BulletSpawnerConfig", menuName = "NeuroDerby/BulletSpawnerConfig", order = 0)]
    public class BulletSpawnerConfig : ScriptableObject
    {
        [SerializeField] private bool isEnabled;
        [SerializeField][Range(0,10)] private float spawnRateSeconds;

        public bool IsEnabled => isEnabled;
        public float SpawnRateSeconds => spawnRateSeconds;
    }
}