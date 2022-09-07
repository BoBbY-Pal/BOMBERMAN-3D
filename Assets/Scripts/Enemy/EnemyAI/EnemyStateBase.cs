using System;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        public void SearchWalkPoint()
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