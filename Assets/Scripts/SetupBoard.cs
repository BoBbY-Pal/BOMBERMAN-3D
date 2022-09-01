using UnityEngine;

namespace Utilities
{
    public class SetupBoard : MonoGenericSingleton<SetupBoard>
    {
        public int width, height;

        public GameObject cellPrefab1, cellPrefab2, wall;
        public GameObject[] boxes;
        public GameObject[,] cellGrid;

        private void Start()
        {
            cellGrid = new GameObject[width, height];
            SetupCellGrid();
            SetupWalls();
            SetupBoxes();
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
                }
            }
        }
        
        private void SetupWalls()
        {
            // Top Down Walls
            for (int x = 0; x < width; x++) 
            {
                GameObject downWall = Instantiate(wall, new Vector3(x, 1, -1), wall.transform.rotation);
                downWall.transform.SetParent(gameObject.transform);
                downWall.name = $"BottomWall ({x})";
                
                GameObject topWall = Instantiate(wall, new Vector3(x, 1, height), wall.transform.rotation);
                topWall.transform.SetParent(gameObject.transform);
                topWall.name = $"TopWall ({x})";
            }
            // Left Right Walls
            for (int z = 0; z < height; z++) 
            {
                GameObject leftWall = Instantiate(wall, new Vector3(-1, 1, z), Quaternion.identity);
                leftWall.transform.SetParent(gameObject.transform);
                leftWall.name = $"LeftWall ({z})";
                
                GameObject rightWall = Instantiate(wall, new Vector3(width, 1, z), Quaternion.identity);
                rightWall.transform.SetParent(gameObject.transform);
                rightWall.name = $" RightWall ({z})";
            }
        }

        private void SetupBoxes()
        {
            
            for (int x = 1; x < width; x++)
            {
                for (int z = 1; z < height; z++)
                {
                    int boxToSpawn = Random.Range(0, boxes.Length); // Get a random box from the array of boxes.
                    GameObject box = Instantiate(boxes[boxToSpawn], new Vector3(x, 0.5f, z), Quaternion.identity);
                    cellGrid[x, z] = box;
                    box.transform.SetParent(gameObject.transform);
                    box.name = $"Box ({x},{z})";
                    z++;
                }
                x++;
            }
        }
    }
}