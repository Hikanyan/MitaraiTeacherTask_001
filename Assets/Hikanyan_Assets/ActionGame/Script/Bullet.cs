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
        [SerializeField] public float _speed;
        [SerializeField] private int _attackDamage = 1;
        private Vector3 _velocity = new Vector3(1, 0, 0);

        // void Start()
        // {
        //     _bulletPos = GetComponent<Transform>();
        //     _playerPos = GameObject.Find("Player").transform;
        //
        //     for (int i = 1; i <= 5; i++)
        //     {
        //         _enemyPos[i - 1] = GameObject.Find($"Enemy_{i}").transform;
        //     }
        //     target.Add(_playerPos);
        //     foreach (var p in _enemyPos)
        //     {
        //         target.Add(p);
        //     }
        //
        // }
        public int AddAttack()
        {
            return _attackDamage;
        }
        // private void Update()
        // {
        //     float angle = transform.rotation.eulerAngles.z;
        //     Vector3 moveDirection = Quaternion.Euler(0f, 0f, angle) * Vector3.right;
        //     transform.position += moveDirection * _speed * Time.deltaTime;
        //
        //
        //     if (50 < transform.position.x || transform.position.x < -20)
        //     {
        //         Destroy(gameObject);
        //     }
        //     foreach (Transform obj in target)
        //     {
        //         if (CollisionDetector(obj))
        //         {
        //             Debug.Log("hit");
        //             if (obj.gameObject.tag == "Player"||obj.gameObject.tag =="Enemy")
        //             {
        //                 if (obj.TryGetComponent<HealthComponent>(out HealthComponent damage))
        //                 {
        //                     damage.Damage(AddAttack());
        //                 }
        //             }
        //             Destroy(gameObject);
        //         }
        //         else
        //         {
        //             //Debug.Log("no");
        //         }
        //     }
        //
        // }
        // float targetX;
        // float targetY;
        // float bulletX;
        // float bulletY;
        // float halfWidth = 0.5f;
        // float halfHeight = 0.5f;
        // bool CollisionDetector(Transform target)
        // {
        //     targetX = target.position.x;
        //     targetY = target.position.y;
        //     bulletX = _bulletPos.position.x;
        //     bulletY = _bulletPos.position.y;
        //     Debug.DrawLine(new Vector3(bulletX - halfWidth, bulletY - halfHeight / 2), new Vector3(bulletX + halfWidth, bulletY - halfHeight / 2), Color.red);
        //     Debug.DrawLine(new Vector3(bulletX + halfWidth, bulletY - halfHeight / 2), new Vector3(bulletX + halfWidth, bulletY + halfHeight / 2), Color.red);
        //     Debug.DrawLine(new Vector3(bulletX + halfWidth, bulletY + halfHeight / 2), new Vector3(bulletX - halfWidth, bulletY + halfHeight / 2), Color.red);
        //     Debug.DrawLine(new Vector3(bulletX - halfWidth, bulletY + halfHeight / 2), new Vector3(bulletX - halfWidth, bulletY - halfHeight / 2), Color.red);
        //
        //     Debug.DrawLine(new Vector3(targetX - halfWidth, targetY - halfHeight), new Vector3(targetX + halfWidth, targetY - halfHeight), Color.red);
        //     Debug.DrawLine(new Vector3(targetX + halfWidth, targetY - halfHeight), new Vector3(targetX + halfWidth, targetY + halfHeight), Color.red);
        //     Debug.DrawLine(new Vector3(targetX + halfWidth, targetY + halfHeight), new Vector3(targetX - halfWidth, targetY + halfHeight), Color.red);
        //     Debug.DrawLine(new Vector3(targetX - halfWidth, targetY + halfHeight), new Vector3(targetX - halfWidth, targetY - halfHeight), Color.red);
        //     if (targetX < bulletX + halfWidth && targetX + halfWidth > bulletX &&
        //         targetY < bulletY + halfHeight / 2 && targetY + halfHeight / 2 > bulletY)
        //     {
        //         return true; // 当たり判定あり
        //     }
        //
        //     return false; // 当たり判定なし
        // }

        //様々な計算が終わった後に呼ばれる
        private void LateUpdate()
        {
            HandleCollision(transform.position, transform.localScale);

            transform.position += _speed * Time.deltaTime * transform.right;

            if (transform.position.x > 40 || transform.position.x < -20)
                Destroy(gameObject);
        }

        private void HandleCollision(Vector3 position, Vector3 scale)
        {
            CheckRectangularCollision(position, scale);
            //CheckCircularCollision(position, scale);
        }

        private void CheckRectangularCollision(Vector3 position, Vector3 scale)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject laser =GameObject.FindGameObjectWithTag("Laser");

            if (player == null) return;
            int allTargetsLength = targets.Length;
            if (laser != null)
            {
                allTargetsLength++;
            }
            GameObject[] allTargets = new GameObject[allTargetsLength + 1];
            targets.CopyTo(allTargets, 0);
            allTargets[allTargets.Length - 1] = player;
            if (laser != null)
            {
                allTargets[allTargets.Length - 2] = laser;
            }
            for (int i = 0; i < allTargets.Length; i++)
            {
                Vector3 targetPosition = allTargets[i].transform.position;
                Vector3 targetScale = allTargets[i].transform.localScale;

                bool isHitX = Mathf.Abs(position.x - targetPosition.x) <= scale.x / 2 + targetScale.x / 2;
                bool isHitY = Mathf.Abs(position.y - targetPosition.y) <= scale.y / 2 + targetScale.y / 2;

                if (isHitX && isHitY)
                {
                    allTargets[i].TryGetComponent<HealthComponent>(out HealthComponent damage);
                    damage.Damage(AddAttack());
                    Destroy(gameObject);
                }
            }
        }

        private void CheckCircularCollision(Vector3 position, Vector3 scale)
        {
            GameObject[] circles = GameObject.FindGameObjectsWithTag("EnemyCircle");

            for (int i = 0; i < circles.Length; i++)
            {
                Vector2 ul = position + new Vector3(-scale.x / 2, scale.y / 2);
                Vector2 ur = position + new Vector3(scale.x / 2, scale.y / 2);
                Vector2 dl = position + new Vector3(-scale.x / 2, -scale.y / 2);
                Vector2 dr = position + new Vector3(scale.x / 2, -scale.y / 2);

                bool isHitting = Mathf.Pow(ul.x - circles[i].transform.position.x, 2) +
                                 Mathf.Pow(ul.y - circles[i].transform.position.y, 2)
                                 <= Mathf.Pow(circles[i].transform.localScale.x / 2, 2)
                                 || Mathf.Pow(ur.x - circles[i].transform.position.x, 2) +
                                 Mathf.Pow(ur.y - circles[i].transform.position.y, 2)
                                 <= Mathf.Pow(circles[i].transform.localScale.x / 2, 2)
                                 || Mathf.Pow(dl.x - circles[i].transform.position.x, 2) +
                                 Mathf.Pow(dl.y - circles[i].transform.position.y, 2)
                                 <= Mathf.Pow(circles[i].transform.localScale.x / 2, 2)
                                 || Mathf.Pow(dr.x - circles[i].transform.position.x, 2) +
                                 Mathf.Pow(dr.y - circles[i].transform.position.y, 2)
                                 <= Mathf.Pow(circles[i].transform.localScale.x / 2, 2);

                if (isHitting)
                {
                    circles[i].TryGetComponent<HealthComponent>(out HealthComponent damage);
                    damage.Damage(AddAttack());
                    Destroy(gameObject);
                }
            }
        }
    }
}