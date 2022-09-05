using System;
using System.Collections;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.EnemyAI
{
    public class Patrolling : EnemyStateBase
    {
        
        protected override void Start()
        {
            base.Start();
            enemyModel.CurrentDirection = Vector3.right;
            StartCoroutine(TimerRoutine(5));
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

        private void FixedUpdate()
        {
            if (_enemyService.enemies.Count <= 2)
            {
                enemyView.currentEnemyState.ChangeCurrentState(enemyView.runningAwayState);
            }
            else
            {
                Patrol();
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
                    // GameLogManager.CustomLog("Before" + enemyModel.CurrentDirection);
                    StartCoroutine(TimerRoutine(1));

                    SearchWalkPoint();
                    
                    enemyView.Move(enemyModel.CurrentDirection);
                    // GameLogManager.CustomLog("After" + enemyModel.CurrentDirection);
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