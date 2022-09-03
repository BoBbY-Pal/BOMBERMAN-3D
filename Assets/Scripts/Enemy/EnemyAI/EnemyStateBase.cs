using UnityEngine;

namespace Enemy.EnemyAI
{
    [RequireComponent(typeof(EnemyView))]
    public class EnemyStateBase : MonoBehaviour
    {   
        protected EnemyView enemyView;
        protected EnemyModel enemyModel;

        protected virtual void Awake()
        {
            enemyView = GetComponent<EnemyView>();
        }

        protected virtual void Start()
        {
            // enemyModel = EnemyService.Instance.tankController.GetModel(); 
        }

        public virtual void OnStateEnter()
        {
            this.enabled = true;
        }

        public virtual void OnStateExit()
        {
            this.enabled = false;
        }
        
        // Logic for changing the states..
        public void ChangeCurrentState(EnemyStateBase newEnemyState)        
        {   
            // if something is already in the current state disable it.
            if (enemyView.currentEnemyState != null)
            {
                enemyView.currentEnemyState.OnStateExit();
            }
        
            // else enter new state to current & enable it.
            enemyView.currentEnemyState = newEnemyState;
            enemyView.currentEnemyState.OnStateEnter();
        }
    }
}