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
        private void Start()
        {
            _transform = this.transform;
        }
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _count)
            {
                BulletShot();
                _timer = 0;
            }
        }
        void BulletShot()
        {
           var bullet = Instantiate(_bullet, new Vector3(_transform.position.x- _offset, _transform.position.y, _transform.position.z), Quaternion.identity);
        }
    }
}