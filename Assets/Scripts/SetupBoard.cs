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
            for (int x = 0; x < width; x++) 
            {
                GameObject downWall = Instantiate(wall, new Vector3(x, 0, -1), wall.transform.rotation);
                downWall.transform.SetParent(gameObject.transform);
                downWall.name = $"BottomWall ({x},{-1})";
                
                GameObject obj = Instantiate(wall, new Vector3(x, 0, height+1), wall.transform.rotation);
                obj.transform.SetParent(gameObject.transform);
                obj.name = $"TopWall ({x},{height+1})";
            }
            
            for (int z = 0; z < height; z++) 
            {
                GameObject leftWall = Instantiate(wall, new Vector3(-1, 0, z), wall.transform.rotation);
                leftWall.transform.SetParent(gameObject.transform);
                leftWall.name = $"LeftWall ({-1},{z})";
                
                GameObject obj = Instantiate(wall, new Vector3(width+1, 0, z), wall.transform.rotation);
                obj.transform.SetParent(gameObject.transform);
                obj.name = $" RightWall ({width+1},{z})";
            }
        }
    }
}