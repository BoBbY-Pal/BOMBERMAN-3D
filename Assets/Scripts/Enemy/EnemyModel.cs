using ScriptableObjects;

namespace Enemy
{
    public class EnemyModel
    {
        public float MovementSpeed
        {
            get;
        }

        public float WalkPointRange
        {
            get;
        }

        public float PatrollingRange
        {
            get;
        }

        public float PatrolTime
        {
            get;
        }

        public EnemyModel(EnemSO enemySO)
        {
            MovementSpeed = enemySO.movementSpeed;
            WalkPointRange = enemySO.walkPointRange;
            PatrollingRange = enemySO.patrollingRange;
            PatrolTime = enemySO.patrolTime;
        }
    }
}