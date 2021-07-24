using UnityEngine;

namespace NeuroDerby.FileOperations
{
    [CreateAssetMenu(fileName = "PathConfig", menuName = "NeuroDerby/PathConfig", order = 0)]
    public class PathConfig : ScriptableObject
    {
        [SerializeField] private string persistentPlayerDataPathPostfix;

        public string PersistentPlayerDataPathPostfix => persistentPlayerDataPathPostfix;
    }
}