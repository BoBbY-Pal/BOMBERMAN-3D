using System;
using Enemy.EnemyAI;
using Enums;
using UnityEngine;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyController _enemyController;
        
        [SerializeField] private EnemyState initialState;
        [HideInInspector] public EnemyState activeState;
        
        public EnemyStateBase currentEnemyState;
        public Patrolling patrollingState;
        public Hiding hidingState;
        
        private void Awake()
        {
            throw new NotImplementedException();
        }

        private void Start()
        {
            InitialiseState();
        }

        private void InitialiseState()
        {
            switch (initialState)
            {
                case EnemyState.Patrolling:
                {
                    currentEnemyState = patrollingState;
                    break;
                }

                case EnemyState.Hiding:
                {
                    currentEnemyState = hidingState;
                    break;
                }
                default:
                {
                    currentEnemyState = null;
                    break;
                }
                
            }
        }

        private void Update()
        {
            
        }

        public void Initialise(EnemyController enemyController)
        {
            _enemyController = enemyController;
        }

        public Vector3 GetCurrentPosition()
        {
            return transform.position;
        }
    }
}