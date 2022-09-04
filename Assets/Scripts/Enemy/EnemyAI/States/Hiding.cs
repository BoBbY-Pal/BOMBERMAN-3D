
namespace Enemy.EnemyAI
{
    public class Hiding : EnemyStateBase
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        // private void Update()
        // {
        //     if (EnemyService.Instance.enemies.Count > 2)
        //     {
        //         enemyView.currentEnemyState.ChangeCurrentState(enemyView.patrollingState);
        //     }
        // }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
}