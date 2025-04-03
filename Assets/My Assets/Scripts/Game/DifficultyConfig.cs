using UnityEngine;

namespace NeuroDerby.Game
{
    [CreateAssetMenu(fileName = "DifficultyConfig", menuName = "NeuroDerby/DifficultyConfig", order = 0)]
    public class DifficultyConfig : ScriptableObject
    {
        [SerializeField][Range(0,2)] private float bulletSpeed;
        [SerializeField][Range(1,20)] private float bulletDamage;

        public float BulletSpeed => bulletSpeed;
        public float BulletDamage => bulletDamage;
    }
}