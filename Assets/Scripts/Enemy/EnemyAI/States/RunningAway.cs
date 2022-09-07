using Enums;
using UnityEngine;


namespace Enemy.EnemyAI
{
    // Enemy will try to run farther from the player...
    public class RunningAway : EnemyStateBase
    {
        private float _enemyMoveTimer = 1;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemyView.activeState = EnemyState.RunningAway;
        }
        
        public override void OnStateExit()
        {
            base.OnStateExit();
        }
        
        private void Update()
        {
            if (EnemyService.Instance.enemies.Count > 2)
            {
                enemyView.currentEnemyState.ChangeCurrentState(enemyView.patrollingState);
            }
            else
            {
                RunAway();
            }

            _enemyMoveTimer = Time.deltaTime;
            if (_enemyMoveTimer <= 0)
            {
                enemyModel.b_CanChangeDirection = true;
                _enemyMoveTimer = 1;
            }
            else
            {
                enemyModel.b_CanChangeDirection = false;
            }
        }

        private void RunAway()
        {
            Vector3 currentPosition = enemyView.transform.position;
            if (_enemyService.playerTransform != null)
            {
                Vector3 distance = (currentPosition - _enemyService.playerTransform.position).normalized;
            }

            bool isThereObstacle = Physics.Raycast(currentPosition, enemyModel.CurrentDirection, 1,
                _enemyService.obstaclesLayerMask);
            // Just for debugging ray..
            // Color rayColor = isThereObstacle ? Color.cyan : Color.red;
            // Debug.DrawRay(currentPosition, enemyModel.CurrentDirection * 1, rayColor, Time.deltaTime);
            
            if (isThereObstacle)
            {
                SearchWalkPoint();
            }
            if(!isThereObstacle)
            {
                enemyView.Move(enemyModel.CurrentDirection);

                if (enemyModel.b_CanChangeDirection)
                {
                    currentPosition = enemyView.transform.position;
                    Direction playerDirection = GetDirection(currentPosition, _enemyService.playerTransform.position);
                    MoveOppositeDirection(playerDirection);
                }
            }
        }
        
        private Direction GetDirection (Vector3 pointOfOrigin, Vector3 vectorToTest) 
        {
            Direction result = Direction.None;
            float shortestDistance = Mathf.Infinity;
            float distance = 0;
         
            Vector3 vectorPosition = pointOfOrigin + vectorToTest;
 
            distance = Mathf.Abs (((pointOfOrigin + Vector3.forward) - vectorToTest).magnitude);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                result = Direction.Forward;
            }
            
            distance = Mathf.Abs (((pointOfOrigin - Vector3.forward) - vectorToTest).magnitude);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                result = Direction.Backward;
            }
            
            distance = Mathf.Abs (((pointOfOrigin + Vector3.left) - vectorToTest).magnitude);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                result = Direction.Left;
            }
            
            distance = Mathf.Abs (((pointOfOrigin + Vector3.right) - vectorToTest).magnitude);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                result = Direction.Right;
            }
 
            return result;
         
        }
        
        

        private void MoveOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Forward:
                {
                    enemyModel.CurrentDirection = -Vector3.forward;
                    enemyView.Move(enemyModel.CurrentDirection);
                    break;
                }
                case Direction.Backward:
                {
                    enemyModel.CurrentDirection = -Vector3.back;
                    enemyView.Move(enemyModel.CurrentDirection);
                    break;
                }
                case Direction.Left:
                {
                    enemyModel.CurrentDirection = -Vector3.left;
                    enemyView.Move(enemyModel.CurrentDirection);
                    break;
                }
                case Direction.Right:
                {
                    enemyModel.CurrentDirection = -Vector3.right;
                    enemyView.Move(enemyModel.CurrentDirection);
                    break;
                }
            }
        }
    }
}
