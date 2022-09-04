using System;
using Enemy.EnemyAI;
using Enums;
using UnityEngine;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyController _enemyController;
        private Rigidbody _rigidbody;
        
        [SerializeField] private EnemyState initialState;
        [HideInInspector] public EnemyState activeState;
        [HideInInspector] public EnemyStateBase currentEnemyState;
        
        public Patrolling patrollingState;
        public Hiding hidingState;
        
        private void Awake()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
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
            currentEnemyState.OnStateEnter();
        }

        private void Update()
        {
            
        }

        public void Initialise(EnemyController enemyController)
        {
            _enemyController = enemyController;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void Move(Vector3 direction)
        {
            _rigidbody.velocity = direction * _enemyController.GetModel().MovementSpeed;
        }
    }
}