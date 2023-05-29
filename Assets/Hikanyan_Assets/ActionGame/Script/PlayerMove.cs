using System;
using UnityEngine;
using System.Collections;

namespace Hikanyan_Assets.ActionGame.Script
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] float _moveSpeed = 5.0f;

        [SerializeField] private GameObject _pointer;
        [SerializeField] private GameObject _bullet;
        private Transform _transform;
        [SerializeField] float _jumpHeight = 5f;
        private float _nowJump;
        private float _jumpStartTime;
        private float _gravity = 9.8f;

        private bool isJumping = false;

        private float _timer;
        private float _count = 1;

        [SerializeField] float _offset = 2;
        [SerializeField] Transform _target;

        private void Start()
        {
            _transform = this.transform;
            _pointer = Instantiate(_pointer, new Vector3(this.transform.position.x + 2, this.transform.position.y, 10),
                Quaternion.identity, _transform);
        }

        private void Update()
        {
            Move();
            Pointer();
            _timer += Time.deltaTime;
            if (_timer >= _count && Input.GetKeyDown(KeyCode.F))
            {
                BulletShot();
                _timer = 0;
            }
        }

        void Move()
        {
            float posX = transform.position.x + Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.deltaTime;
            float posY = transform.position.y;
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                isJumping = true;
            }

            if (isJumping)
            {
                posY += _nowJump * Time.deltaTime;
                _nowJump -= _gravity * Time.deltaTime;

                if (transform.position.y < 0)
                {
                    posY = 0;
                    _nowJump = _jumpHeight;
                    isJumping = false;
                }
            }

            transform.position = new Vector3(posX, posY, 0);
        }


        float angle;

        void BulletShot()
        {
            var bullet = Instantiate(_bullet, new Vector3(_target.position.x, _target.position.y, _target.position.z),
                Quaternion.identity);
            bullet.transform.rotation = transform.rotation;
            //bullet.transform.position =_transform.position - _target.position;
        }

        void Pointer()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = mousePosition - playerPosition;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            _pointer.transform.position = target;
        }
    }
}