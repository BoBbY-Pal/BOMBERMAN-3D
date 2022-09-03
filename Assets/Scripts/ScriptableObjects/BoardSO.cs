using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewBoard", menuName = "ScriptableObject/Board", order = 1)]
    public class BoardSO : ScriptableObject
    {
        [Header("Board Properties")]
        public int width;
        public int height;
        
        [Header("Prefabs")]
        public GameObject cellPrefab;
        public GameObject wallPrefab;
        public GameObject playerPrefab;
        public GameObject enemyPrefab;
        
        [Tooltip("Boxes that will be placed on the cells grid that can be Destructible/NonDestructible.")]
        public GameObject[] boxes;
    }
}