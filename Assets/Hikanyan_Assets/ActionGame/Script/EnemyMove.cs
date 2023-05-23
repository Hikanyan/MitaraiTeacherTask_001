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

        private void Start()
        {
            _transform = this.transform;
            _playerTarget = GameObject.Find("Player").transform;
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
            if (CheckCollision(_transform.transform.position, 10, rect) && _timer >= _count)
            {
                BulletShot();
                _timer = 0;
            }
        }
        void BulletShot()
        {
            var bullet = Instantiate(_bullet, new Vector3(_transform.position.x - _offset, _transform.position.y, _transform.position.z), Quaternion.identity);
        }
        // 円と矩形の判定
        bool CheckCollision(Vector2 circleCenter, float circleRadius, Rect rectangle)
        {
            // 円の中心座標を矩形の領域内に制約する
            float constrainedX = Mathf.Clamp(circleCenter.x, rectangle.x, rectangle.x + rectangle.width);
            float constrainedY = Mathf.Clamp(circleCenter.y, rectangle.y, rectangle.y + rectangle.height);

            // 制約された座標と円の中心座標の距離を計算
            float distance = Vector2.Distance(circleCenter, new Vector2(constrainedX, constrainedY));

            // 距離が円の半径以下であれば、円と矩形は重なっている
            if (distance <= circleRadius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}