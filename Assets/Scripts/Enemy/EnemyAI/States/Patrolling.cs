using System;
using System.Collections;
using Enums;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Enemy.EnemyAI
{
    public class Patrolling : EnemyStateBase
    {
        private bool b_CanChangDir;
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
            // StartCoroutine(Patrol());
            Patrol();
        }

        private void Patrol()
        {
            Vector3 currentPosition = enemyView.GetPosition();
            bool isThereObstacle = Physics.Raycast(currentPosition, enemyModel.CurrentDirection, 1,
                                                EnemyService.Instance.obstaclesLayerMask);
            
            Color rayColor = isThereObstacle ? Color.cyan : Color.red;
            Debug.DrawRay(currentPosition, enemyModel.CurrentDirection * 1, rayColor, Time.deltaTime);
            
            if (isThereObstacle)
            {
                SearchWalkPoint();
            }
            if(!isThereObstacle)
            {
                enemyView.Move(enemyModel.CurrentDirection);
                // yield return new WaitForSeconds(5fenemyModel.CurrentDirection
                
                if (b_CanChangDir)
                {   
                    GameLogManager.CustomLog("Before" + enemyModel.CurrentDirection);
                    StartCoroutine(TimerRoutine(1));

                    SearchWalkPoint();
                    
                    enemyView.Move(enemyModel.CurrentDirection);
                    GameLogManager.CustomLog("After" + enemyModel.CurrentDirection);
                }
            }
        }

        private IEnumerator TimerRoutine(float secs)
        {
            b_CanChangDir = false;
            yield return new WaitForSeconds(secs);
            b_CanChangDir = true;
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