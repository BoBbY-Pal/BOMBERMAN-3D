using Enums;
using UnityEngine;

namespace Enemy.EnemyAI
{
    // Enemy will try to run farther from the player...
    public class RunningAway : EnemyStateBase
    {
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
        }

        private void RunAway()
        {
            Vector3 distance = (enemyView.GetPosition() - EnemyService.Instance.playerTransform.position).normalized;
            
        }
    }
}
