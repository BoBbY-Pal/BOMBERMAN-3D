using System;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.EnemyAI
{
    public class Patrolling : EnemyStateBase
    {
        private Vector3 _currentDirection;

        protected override void Start()
        {
            base.Start();
            _currentDirection = Vector3.right;
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
            // if (EnemyService.Instance.enemies.Count <= 2)
            // {
            //     enemyView.currentEnemyState.ChangeCurrentState(enemyView.hidingState);
            // }
            // else
            // {
            //     
            // }
            Patrol();
        }

        private void Patrol()
        {
            // switch (_currentDirection)
            // {
            //     
            // }
            Vector3 currentPosition = enemyView.GetPosition();
            bool isThereObstacle = Physics.Raycast(currentPosition, _currentDirection, 1,
                                                EnemyService.Instance.obstaclesLayerMask);
            
            Color rayColor = isThereObstacle ? Color.cyan : Color.red;
            Debug.DrawRay(currentPosition, _currentDirection * 1, rayColor, Time.deltaTime);
            
            if (isThereObstacle)
            {
                SearchWalkPoint();
            }
            if(!isThereObstacle)
            {
                enemyView.Move(_currentDirection);
            }
        }

        private void SearchWalkPoint()
        {
            int lengthOfEnum = Enum.GetValues(typeof(Direction)).Length;
            Direction randomDirection = (Direction)Random.Range(0, lengthOfEnum);

            switch (randomDirection)
            {
                case Direction.Forward:
                {
                    _currentDirection = Vector3.forward;
                    break;
                }
                case Direction.Backward:
                {
                    _currentDirection = Vector3.back;
                    break;
                }
                case Direction.Left:
                {
                    _currentDirection = Vector3.left;
                    break;
                }
                case Direction.Right:
                {
                    _currentDirection = Vector3.right;
                    break;
                }
            }
        }
    }
}