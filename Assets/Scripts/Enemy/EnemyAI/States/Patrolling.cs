using System;
using System.Collections;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

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
            Vector3 currentPosition = enemyView.GetPosition();
            bool isThereObstacle = Physics.Raycast(currentPosition, enemyModel.CurrentDirection, 1,
                _enemyService.obstaclesLayerMask);
            
            Color rayColor = isThereObstacle ? Color.cyan : Color.red;
            Debug.DrawRay(currentPosition, enemyModel.CurrentDirection * 1, rayColor, Time.deltaTime);
            
            if (isThereObstacle)
            {
                SearchWalkPoint();
            }
            if(!isThereObstacle)
            {
                enemyView.Move(enemyModel.CurrentDirection);

                if (enemyModel.b_CanChangeDirection)
                {
                    SearchWalkPoint();
                    enemyView.Move(enemyModel.CurrentDirection);
                }
            }
        }

        private IEnumerator TimerRoutine(float secs)
        {
            enemyModel.b_CanChangeDirection = false;
            yield return new WaitForSeconds(secs);
            enemyModel.b_CanChangeDirection = true;
        }
        
        private void SearchWalkPoint()
        {
            int lengthOfEnum = Enum.GetValues(typeof(Direction)).Length;
            Direction randomDirection = (Direction) Random.Range(0, lengthOfEnum);

            switch (randomDirection)
            {
                case Direction.Forward:
                {
                    enemyModel.CurrentDirection = Vector3.forward;
                    break;
                }
                case Direction.Backward:
                {
                    enemyModel.CurrentDirection = Vector3.back;
                    break;
                }
                case Direction.Left:
                {
                    enemyModel.CurrentDirection = Vector3.left;
                    break;
                }
                case Direction.Right:
                {
                    enemyModel.CurrentDirection = Vector3.right;
                    break;
                }
            }
        }
    }
}