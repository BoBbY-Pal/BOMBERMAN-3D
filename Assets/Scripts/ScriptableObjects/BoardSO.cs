using UnityEngine;
using Walls;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewBoard", menuName = "ScriptableObject/Board", order = 1)]
    public class BoardSO : ScriptableObject
    {
        [Header("Board Properties")]
        public int width;
        public int height;
        [Tooltip("Number of enemies to spawn on the board.")]
        public int numberOfEnemies;
        
        [Header("Prefabs")]
        public GameObject cellPrefab;
        public GameObject wallPrefab;
        public GameObject playerPrefab;
        
        [Tooltip("Boxes that will be placed on the cells grid that can be Destructible/NonDestructible.")]
        public Wall[] boxes;
        public Wall destructibleWall;
        
    }
}