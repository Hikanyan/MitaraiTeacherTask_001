using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

namespace Hikanyan_Assets.ActionGame.Script
{
    public class Bullet : MonoBehaviour, IAttack
    {
        [SerializeField] private int _attackDamage;
        [SerializeField] public float _speed;
        [SerializeField] Transform _bulletPos;
        [SerializeField] Transform _playerPos;
        [SerializeField] List<Transform> _enemyPos;

        [SerializeField] List<Transform> target;

        void Start()
        {
            _bulletPos = GetComponent<Transform>();
            _playerPos = GameObject.Find("Player").transform;

            for (int i = 1; i <= 5; i++)
            {
                _enemyPos[i - 1] = GameObject.Find($"Enemy_{i}").transform;
            }
            target.Add(_playerPos);
            foreach (var p in _enemyPos)
            {
                target.Add(p);
            }

        }
        public int AddAttack()
        {
            return _attackDamage;
        }
        private void Update()
        {
            transform.position += new Vector3(_speed * Time.deltaTime, 0, 0);

            if (50 < transform.position.x || transform.position.x < -20)
            {
                Destroy(gameObject);
            }
            foreach (Transform obj in target)
            {
                if (CollisionDetector(obj))
                {
                    Debug.Log("hit");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("no");
                }
            }

        }
        float targetX;
        float targetY;
        float bulletX;
        float bulletY;
        float halfWidth = 0.5f;
        float halfHeight = 0.5f;
        bool CollisionDetector(Transform target)
        {
            targetX = target.position.x;
            targetY = target.position.y;
            bulletX = _bulletPos.position.x;
            bulletY = _bulletPos.position.y;
            Debug.DrawLine(new Vector3(bulletX - halfWidth, bulletY - halfHeight / 2), new Vector3(bulletX + halfWidth, bulletY - halfHeight / 2), Color.red);
            Debug.DrawLine(new Vector3(bulletX + halfWidth, bulletY - halfHeight / 2), new Vector3(bulletX + halfWidth, bulletY + halfHeight / 2), Color.red);
            Debug.DrawLine(new Vector3(bulletX + halfWidth, bulletY + halfHeight / 2), new Vector3(bulletX - halfWidth, bulletY + halfHeight / 2), Color.red);
            Debug.DrawLine(new Vector3(bulletX - halfWidth, bulletY + halfHeight / 2), new Vector3(bulletX - halfWidth, bulletY - halfHeight / 2), Color.red);

            Debug.DrawLine(new Vector3(targetX - halfWidth, targetY - halfHeight), new Vector3(targetX + halfWidth, targetY - halfHeight), Color.red);
            Debug.DrawLine(new Vector3(targetX + halfWidth, targetY - halfHeight), new Vector3(targetX + halfWidth, targetY + halfHeight), Color.red);
            Debug.DrawLine(new Vector3(targetX + halfWidth, targetY + halfHeight), new Vector3(targetX - halfWidth, targetY + halfHeight), Color.red);
            Debug.DrawLine(new Vector3(targetX - halfWidth, targetY + halfHeight), new Vector3(targetX - halfWidth, targetY - halfHeight), Color.red);
            if (targetX < bulletX + halfWidth && targetX + halfWidth > bulletX &&
                targetY < bulletY + halfHeight / 2 && targetY + halfHeight / 2 > bulletY)
            {
                return true; // 当たり判定あり
            }

            return false; // 当たり判定なし
        }

    }
}