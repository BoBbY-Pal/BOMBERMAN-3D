using System;
using Bomb;
using Enemy;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IDestructible
    {
        public float playerMoveSpeed;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        private void Update()
        {
            EnemyService.Instance.playerTransform = transform;
            MovePlayer();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                BombSpawner.Instance.SpawnBomb(transform.position);
            }
        }

        private void MovePlayer()
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal") * playerMoveSpeed * Time.deltaTime;
            float verticalMovement = Input.GetAxisRaw("Vertical") * playerMoveSpeed * Time.deltaTime;

            if (horizontalMovement is > 0 or < 0)
            {
                _rigidbody.velocity = new Vector3(horizontalMovement, 0, 0);
            }
            else if (verticalMovement is > 0 or < 0)
            {
                _rigidbody.velocity = new Vector3(0, 0, verticalMovement);
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
            }
        }
        
        public void DestroyObject()
        {
            EventService.GameOver?.Invoke();
            Destroy(gameObject);
        }
    }
}
