using UnityEngine;

namespace Utilities
{
    public class BoardManager : MonoGenericSingleton<BoardManager>
    {
        public int width, height;

        public GameObject cellPrefab1, cellPrefab2;

        public int[,] cellGrid;

        private void Start()
        {
            cellGrid = new int[width, height];
            SetupCellGrid();
        }

        private void SetupCellGrid()
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GameObject obj = Instantiate(cellPrefab1, new Vector3(x, 0, z), cellPrefab1.transform.rotation);
                    obj.transform.SetParent(gameObject.transform);
                    obj.name = $"Cell ({x},{z})";
                    //ToDo: Store it in 2D store for future references but the ques is how?
                }
            }
        }
    }
}