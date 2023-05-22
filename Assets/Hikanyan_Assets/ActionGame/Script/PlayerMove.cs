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
        float jumpHeight = 5f;
        float jumpDuration = 1f;
        private float jumpStartTime;
        private bool isJumping = false;

        private float _timer;
        private float _count = 1;

        [SerializeField] float _offset = 2;
        private void Start()
        {
            _transform = this.transform;
            _pointer = Instantiate(_pointer, new Vector3(this.transform.position.x+2, this.transform.position.y, 10),
                Quaternion.identity, _transform);
        }

        private void Update()
        {
            Move();
            Pointer();
            _timer += Time.deltaTime;
            if (_timer >= _count)
            {
                BulletShot();
                _timer = 0;
            }
        }

        void Move()
        {
            var posY = transform.position.y;
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                StartJump();
            }
            if (isJumping)
            {
                PerformJump();
            }
            else
            {
                if (transform.position.y > 0.0f)
                {
                    posY -= 9.8f;
                }
                else
                {
                    posY = 0;
                }
            }
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), posY).normalized * (_moveSpeed * Time.deltaTime);
        }
        private void StartJump()
        {
            isJumping = true;
            jumpStartTime = Time.time;
        }

        private void PerformJump()
        {
            float timeSinceJump = Time.time - jumpStartTime;
            if (timeSinceJump >= jumpDuration)
            {
                isJumping = false;
            }
            else
            {
                float jumpHeightValue = CalculateJumpHeight(timeSinceJump);

                Vector3 newPosition = transform.position;
                newPosition.y = jumpHeightValue;
                transform.position = newPosition;
            }
        }

        private float CalculateJumpHeight(float time)
        {
            float g = Mathf.Abs(-9.8f);
            float v0 = (2f * jumpHeight) / jumpDuration;
            float h = v0 * time - (0.5f * g * time * time);

            return h;
        }



        void BulletShot()
        {
            Instantiate(_bullet, new Vector3(_transform.position.x+ _offset, _transform.position.y, _transform.position.z), Quaternion.identity);
        }

        void Pointer()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
            _pointer.transform.position = target;
        }
    }
}