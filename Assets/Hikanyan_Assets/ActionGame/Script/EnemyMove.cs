using System;
using UnityEngine;

namespace Hikanyan_Assets.ActionGame.Script
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;

        private Transform _transform;
        private float _timer;
        private float _count = 1;
        private float _distance = 10;
        [SerializeField] float _offset = 2;
        Transform _playerTarget;
        [SerializeField] CheckCollision checkCollision;
        private void Start()
        {
            _transform = this.transform;
            _playerTarget = GameObject.Find("Player").transform;
            checkCollision = GetComponent<CheckCollision>();
        }
        private void Update()
        {
            Rect rect = new Rect(
                    _playerTarget.transform.position.x - _playerTarget.transform.localScale.x / 2f,
                    _playerTarget.transform.position.y - _playerTarget.transform.localScale.y / 2f,
                    _playerTarget.transform.localScale.x,
                    _playerTarget.transform.localScale.y
                    );

            _timer += Time.deltaTime;
            if (checkCollision.CollisionCheck(_transform.transform.position, 10, rect) && _timer >= _count)
            {
                BulletShot();
                _timer = 0;
            }
        }
        void BulletShot()
        {
            var bullet = Instantiate(_bullet, new Vector3(_transform.position.x - _offset, _transform.position.y, _transform.position.z), Quaternion.identity);
        }
    }
}