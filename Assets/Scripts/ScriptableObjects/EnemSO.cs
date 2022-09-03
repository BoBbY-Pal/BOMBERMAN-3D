using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObject/Enemy", order = 1)]
    public class EnemSO : ScriptableObject
    {
        [Header("Movement Parameters")] 
        public float movementSpeed;
        public float walkPointRange;
        public float patrollingRange;
        public float patrolTime;
    }
}