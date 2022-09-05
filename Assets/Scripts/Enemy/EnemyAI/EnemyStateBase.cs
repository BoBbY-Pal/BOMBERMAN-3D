using UnityEngine;

namespace Enemy.EnemyAI
{
    [RequireComponent(typeof(EnemyView))]
    public class EnemyStateBase : MonoBehaviour
    {   
        protected EnemyView enemyView;
        protected EnemyModel enemyModel;
        protected EnemyService _enemyService;

        protected virtual void Awake()
        {
            enemyView = GetComponent<EnemyView>();
            _enemyService = EnemyService.Instance;
        }

        protected virtual void Start()
        {
            enemyModel = enemyView.enemyController.GetModel(); 
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