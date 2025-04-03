using UnityEngine;
using Zenject;

namespace NeuroDerby.Game
{
    public class Bullet : MonoBehaviour
    {
        public class Pool : MonoMemoryPool<Bullet>
        {
        }
    }
}