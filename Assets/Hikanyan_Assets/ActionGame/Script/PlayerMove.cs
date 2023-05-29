using System;
using UnityEngine;
using System.Collections;

namespace Hikanyan_Assets.ActionGame.Script
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] float _moveSpeed = 5.0f;

        private Transform _transform;
        [SerializeField] float _jumpHeight = 5f;
        private float _nowJump;
        private float _jumpStartTime;
        private float _gravity = 9.8f;

        private bool isJumping = false;

        private float _timer;
        private float _count = 1;


        private void Update()
        {
            Move();
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
    }
}