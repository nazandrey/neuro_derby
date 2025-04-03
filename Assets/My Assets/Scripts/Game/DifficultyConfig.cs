using UnityEngine;

namespace NeuroDerby.Game
{
    [CreateAssetMenu(fileName = "DifficultyConfig", menuName = "NeuroDerby/DifficultyConfig", order = 0)]
    public class DifficultyConfig : ScriptableObject
    {
        [SerializeField][Range(0,2)] private float bulletSpeed;

        public float BulletSpeed => bulletSpeed;
    }
}