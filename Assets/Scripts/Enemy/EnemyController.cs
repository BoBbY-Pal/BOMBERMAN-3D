using UnityEngine;

namespace Enemy
{
    // In case we want to add more functionalities to the enemy this script is always open for extension.
    public class EnemyController
    {
        private EnemyModel _enemyModel { get; }
        private EnemyView _enemyView { get; }
        
        public EnemyController(EnemyView enemyPrefab, EnemyModel enemyModel, Vector3 spawnTransform)
        {
            _enemyView = Object.Instantiate(enemyPrefab, spawnTransform, Quaternion.identity);
            _enemyModel = enemyModel;
            _enemyView.Initialise(this);
        }

        private void FindNewDirection()
        {
            throw new System.NotImplementedException();
        }

        public EnemyModel GetModel()
        {
            return _enemyModel;
        }
    }
}