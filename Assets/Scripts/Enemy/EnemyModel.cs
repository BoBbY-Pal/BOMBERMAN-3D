using ScriptableObjects;
using UnityEngine;

namespace Enemy
{
    public class EnemyModel
    {
        public float MovementSpeed { get; }

        public float WalkPointRangeOnX { get; }

        public float WalkPointRangeOnZ { get; }
    
        public Vector3 walkPoint { get; set; }
        public float PatrolTime { get; }

        public EnemyModel(EnemSO enemySO)
        {
            MovementSpeed = enemySO.movementSpeed;
            WalkPointRangeOnX = enemySO.walkPointRangeOnX;
            WalkPointRangeOnZ = enemySO.WalkPointRangeOnZ;
            PatrolTime = enemySO.patrolTime;
        }
    }
}