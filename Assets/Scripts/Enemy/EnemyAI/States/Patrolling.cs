
using UnityEngine;

namespace Enemy.EnemyAI
{
    public class Patrolling : EnemyStateBase
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            this.enabled = true;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            this.enabled = false;
        }

        private void Update()
        {
            if (EnemyService.Instance.enemies.Count <= 2)
            {
                enemyView.currentEnemyState.ChangeCurrentState(enemyView.hidingState);
            }

            Patrol();
            SearchWalkPoint();
        }

        private void Patrol()
        {
            Vector3 currentPosition = enemyView.GetCurrentPosition();
            bool raycastHit = Physics.Raycast(currentPosition, Vector3.forward, 1,
                                                EnemyService.Instance.obstaclesLayerMask);
            
            Color rayColor = raycastHit ? Color.cyan : Color.red;
            Debug.DrawRay(currentPosition, Vector3.forward * 1, rayColor, Time.deltaTime);
            if (raycastHit)
            {
                
            }
        }

        private void SearchWalkPoint()
        {
            throw new System.NotImplementedException();
        }
    }
}