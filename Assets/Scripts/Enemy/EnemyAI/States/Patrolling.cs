using Enums;
using UnityEngine;


namespace Enemy.EnemyAI
{
    public class Patrolling : EnemyStateBase
    {
        // after how many sec enemy should change the direction.
        private float enemyMoveTimer = 1; 
        
        protected override void Start()
        {
            base.Start();
            enemyModel.CurrentDirection = Vector3.right;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemyView.activeState = EnemyState.Patrolling;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        private void Update()
        {
            if (_enemyService.enemies.Count <= 2)
            {
                enemyView.currentEnemyState.ChangeCurrentState(enemyView.runningAwayState);
            }
            else
            {
                Patrol();
            }

            enemyMoveTimer -= Time.deltaTime;
            if (enemyMoveTimer <= 0)
            {
                enemyModel.b_CanChangeDirection = true;
                enemyMoveTimer = 1;
            }
            else
            {
                enemyModel.b_CanChangeDirection = false;
            }
        }

        private void Patrol()
        {
            Vector3 currentPosition = enemyView.transform.position;
            bool isThereObstacle = Physics.Raycast(currentPosition, enemyModel.CurrentDirection, 1,
                _enemyService.obstaclesLayerMask);
            // Just for debugging ray..
            // Color rayColor = isThereObstacle ? Color.cyan : Color.red;
            // Debug.DrawRay(currentPosition, enemyModel.CurrentDirection * 1, rayColor, Time.deltaTime);
            
            switch (isThereObstacle)
            {
                case true:
                    SearchWalkPoint();
                    break;
                case false:
                {
                    enemyView.Move(enemyModel.CurrentDirection);

                    if (enemyModel.b_CanChangeDirection)
                    {
                        SearchWalkPoint();
                        enemyView.Move(enemyModel.CurrentDirection);
                    }

                    break;
                }
            }
        }
    }
}