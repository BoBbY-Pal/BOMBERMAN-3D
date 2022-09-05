using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Utilities;

namespace Enemy
{
    public class EnemyService : MonoGenericSingleton<EnemyService>
    {
        [Header("Prefab")]
        [SerializeField] private EnemyView enemyPrefab;
        
        [Tooltip("Enemy scriptable object that contains properties of enemy.")]
        [SerializeField] private EnemSO enemySO;
        
        public LayerMask obstaclesLayerMask;
        public Transform playerTransform;
        public List<EnemyController> enemies;
        
        protected override void Awake()
        {
            base.Awake();
            enemies = new List<EnemyController>();
        }

        public void CreateEnemy(Vector3 spawnTransform)
       {
           EnemyModel enemyModel = new EnemyModel(enemySO);
           EnemyController enemyController = new EnemyController(enemyPrefab, enemyModel, spawnTransform);
           enemies.Add(enemyController);
       }
    }
}