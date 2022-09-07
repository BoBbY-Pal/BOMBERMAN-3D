using Enemy;
using ScriptableObjects;
using UnityEngine;
using Utilities;
using Walls;

namespace Core
{
    // Creating whole board in the runTime so if in future there's a need of even 50*50 grid
    // we just need to pass in width and height into a board scriptable object and there we go...
    public class BoardManager : MonoGenericSingleton<BoardManager>
    {
        [Tooltip("Scriptable object of a Board that contains all the board properties.")]
        [SerializeField] private BoardSO boardSo;
        
        private int _width, _height;
        private int _enemiesToSpawn;

        private GameObject _cellPrefab, _wallPrefab, _playerPrefab;

        [Tooltip("Boxes that will be placed on the cells grid.")]
        private Wall[] _boxes;

        public Wall[,] _cellGrid;

        protected override void Awake()
        {
            base.Awake();
            _width = boardSo.width;
            _height = boardSo.height;
            _cellPrefab = boardSo.cellPrefab;
            _wallPrefab = boardSo.wallPrefab;
            _playerPrefab = boardSo.playerPrefab;
            _enemiesToSpawn = boardSo.numberOfEnemies;
            _boxes = boardSo.boxes;
        }

        private void Start()
        {
            _cellGrid = new Walls.Wall[_width, _height];
            
            SetupCellGrid();
            SetupWalls();
            SetupBoxes();
            GetGridItem();
            SpawnPlayer();
            SpawnEnemies();
        }

        private void SetupCellGrid()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _height; z++)
                {
                    GameObject obj = Instantiate(_cellPrefab, new Vector3(x, 0, z), _cellPrefab.transform.rotation);
                    obj.transform.SetParent(gameObject.transform);
                    obj.name = $"Cell ({x},{z})";
                }
            }
        }
        
        private void SetupWalls()
        {
            // Top Down Walls
            for (int x = 0; x < _width; x++) 
            {
                GameObject downWall = Instantiate(_wallPrefab, new Vector3(x, 1, -1), _wallPrefab.transform.rotation);
                downWall.transform.SetParent(gameObject.transform);
                downWall.name = $"BottomWall ({x})";
                
                GameObject topWall = Instantiate(_wallPrefab, new Vector3(x, 1, _height), _wallPrefab.transform.rotation);
                topWall.transform.SetParent(gameObject.transform);
                topWall.name = $"TopWall ({x})";
            }
            // Left Right Walls
            for (int z = 0; z < _height; z++) 
            {
                GameObject leftWall = Instantiate(_wallPrefab, new Vector3(-1, 1, z), Quaternion.identity);
                leftWall.transform.SetParent(gameObject.transform);
                leftWall.name = $"LeftWall ({z})";
                
                GameObject rightWall = Instantiate(_wallPrefab, new Vector3(_width, 1, z), Quaternion.identity);
                rightWall.transform.SetParent(gameObject.transform);
                rightWall.name = $" RightWall ({z})";
            }
        }

        private void SetupBoxes()
        {
            
            for (int x = 1; x < _width; x++)
            {
                for (int z = 1; z < _height; z++)
                {
                    int boxToSpawn = Random.Range(0, _boxes.Length); // Get a random box from the array of boxes.
                    Walls.Wall box = Instantiate(_boxes[boxToSpawn], new Vector3(x, 0.5f, z), Quaternion.identity);
                    _cellGrid[x, z] = box;
                    box.transform.SetParent(gameObject.transform);
                    box.name = $"Box ({x},{z})";
                    z++;
                }
                x++;
            }
        }
        
        private void GetGridItem()
        {
            
        }

        private void SpawnPlayer()
        {
            Instantiate(_playerPrefab, new Vector3(0, 1, _height-1), Quaternion.identity);
        }
        
        private void SpawnEnemies()
        {
            for (int i = 0; i < _enemiesToSpawn; i++)
            {
                int randX = Random.Range(0, _width - 1);
                int randZ = Random.Range(0, _height - 1);

                while (_cellGrid[randX, randZ] != null)
                {
                    randX = Random.Range(0, _width - 1);
                    randZ = Random.Range(0, _height - 1);
                }
                EnemyService.Instance.CreateEnemy(new Vector3(randX, 1, randZ));
                GameLogManager.CustomLog("Enemy spawned.");
            }
        }

        public bool IsGridOutOfBound(int x, int z)
        {
            return x < 0 || x > _width - 1 || z < 0 || z > _height - 1;
        }
    }
}