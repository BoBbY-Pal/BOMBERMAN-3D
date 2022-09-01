using UnityEngine;

namespace Utilities
{
    public class SetupBoard : MonoGenericSingleton<SetupBoard>
    {
        public int width, height;

        public GameObject cellPrefab1, cellPrefab2, wall;

        public int[,] cellGrid;

        private void Start()
        {
            cellGrid = new int[width, height];
            SetupCellGrid();
            SetupWalls();
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
        
        private void SetupWalls()
        {
            // Top Down Walls
            for (int x = 0; x < width; x++) 
            {
                GameObject downWall = Instantiate(wall, new Vector3(x, 0, -1), wall.transform.rotation);
                downWall.transform.SetParent(gameObject.transform);
                downWall.name = $"BottomWall ({x})";
                
                GameObject topWall = Instantiate(wall, new Vector3(x, 0, height), wall.transform.rotation);
                topWall.transform.SetParent(gameObject.transform);
                topWall.name = $"TopWall ({x})";
            }
            // Left Right Walls
            for (int z = 0; z < height; z++) 
            {
                GameObject leftWall = Instantiate(wall, new Vector3(-1, 0, z), wall.transform.rotation);
                leftWall.transform.SetParent(gameObject.transform);
                leftWall.name = $"LeftWall ({z})";
                
                GameObject rightWall = Instantiate(wall, new Vector3(width, 0, z), wall.transform.rotation);
                rightWall.transform.SetParent(gameObject.transform);
                rightWall.name = $" RightWall ({z})";
            }
        }
    }
}