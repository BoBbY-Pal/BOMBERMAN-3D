using ScriptableObjects;
using UnityEngine;

namespace Enemy
{
    public class EnemyModel
    {
        public float MovementSpeed { get; }

        public Vector3 CurrentDirection { get; set; }
        public bool b_CanChangeDirection { get; set; }
        
        public float PatrolTime { get; }

        public EnemyModel(EnemSO enemySO)
        {
            MovementSpeed = enemySO.movementSpeed;
            
            PatrolTime = enemySO.patrolTime;
        }
    }
}