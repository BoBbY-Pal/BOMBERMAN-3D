using UnityEngine;
using Utilities;

namespace Core
{
    public class BoardManager : MonoGenericSingleton<BoardManager>
    {
        public int width, height;

        public GameObject cellPrefab1, cellPrefab2, wallPrefab, playerPrefab;
        
        [Tooltip("Boxes that will be placed on the cells grid.")]
        public GameObject[] boxes;
        public GameObject[,] cellGrid;  

        private void Start()
        {
            cellGrid = new GameObject[width, height];
            
            SetupCellGrid();
            SetupWalls();
            SetupBoxes();
            IsCellEmpty();
            SpawnPlayer();
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
                GameObject downWall = Instantiate(wallPrefab, new Vector3(x, 1, -1), wallPrefab.transform.rotation);
                downWall.transform.SetParent(gameObject.transform);
                downWall.name = $"BottomWall ({x})";
                
                GameObject topWall = Instantiate(wallPrefab, new Vector3(x, 1, height), wallPrefab.transform.rotation);
                topWall.transform.SetParent(gameObject.transform);
                topWall.name = $"TopWall ({x})";
            }
            // Left Right Walls
            for (int z = 0; z < height; z++) 
            {
                GameObject leftWall = Instantiate(wallPrefab, new Vector3(-1, 1, z), Quaternion.identity);
                leftWall.transform.SetParent(gameObject.transform);
                leftWall.name = $"LeftWall ({z})";
                
                GameObject rightWall = Instantiate(wallPrefab, new Vector3(width, 1, z), Quaternion.identity);
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
        
        private void IsCellEmpty()
        {
            
            for (int x = 1; x < width; x++)
            {
                for (int z = 1; z < height; z++)
                {
                    if (cellGrid[x, z] != null)
                    {
                        GameLogManager.CustomLog("Found something!");
                    }
                }
            }
        }

        private void SpawnPlayer()
        {
            Instantiate(playerPrefab, new Vector3(0, 1, height-1), Quaternion.identity);
        }
    }
}