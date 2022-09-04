using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObject/Enemy", order = 1)]
    public class EnemSO : ScriptableObject
    {
        [Header("Movement Parameters")] 
        public float movementSpeed;
        public float walkPointRangeOnX;
        public float WalkPointRangeOnZ;
        public float patrolTime;
    }
}