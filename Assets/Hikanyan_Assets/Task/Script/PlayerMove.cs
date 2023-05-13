using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hikanyan_Assets.Task.Script
{
    public class PlayerMove : MonoBehaviour
    {
        //プレイヤー状態定義
        public enum PLAYER_STATE
        {
            IDLE, //停止
            RUN, //走る
            SHIFT, //横移動
        }

        //横移動方向定義
        public enum SHIFT_DIR
        {
            NONE, //通常
            LEFT, //左側
            RIGHT //右側
        }

        //Playerの直線移動スピード
        [SerializeField] float _playerSpeed = 1;

        //横移動量
        [SerializeField] float _shiftValue = 1;

        //レーンのポイント
        [SerializeField] List<Transform> linePoint = new List<Transform>();

        //現在のエリアラインのインデックス
        int _currentAreaLineIdx = 1;
        PLAYER_STATE _playerState = PLAYER_STATE.IDLE;
        SHIFT_DIR _shiftDir = SHIFT_DIR.NONE;

        void Awake()
        {
        }

        void Update()
        {
            InputMove();
            if (_playerState == PLAYER_STATE.RUN)
            {
                Front();
            }


            if (_playerState == PLAYER_STATE.SHIFT)
            {
                Shift();
            }
        }

        void Front()
        {
            transform.position += new Vector3(0, 0, -_playerSpeed * Time.deltaTime);
        }

        void Shift()
        {
            switch (_shiftDir)
            {
                case SHIFT_DIR.LEFT:
                    if (_currentAreaLineIdx > 0)
                    {
                        _currentAreaLineIdx--;
                        transform.position = new Vector3(linePoint[_currentAreaLineIdx].position.x,
                            transform.position.y, transform.position.z);
                    }

                    break;
                case SHIFT_DIR.RIGHT:
                    if (_currentAreaLineIdx < 2)
                    {
                        _currentAreaLineIdx++;
                        transform.position = new Vector3(linePoint[_currentAreaLineIdx].position.x,
                            transform.position.y, transform.position.z);
                    }

                    break;
            }

            _shiftDir = SHIFT_DIR.NONE;
            _playerState = PLAYER_STATE.RUN;
        }

        void InputMove()
        {
            switch (_playerState)
            {
                case PLAYER_STATE.IDLE:
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        _playerState = PLAYER_STATE.RUN;
                    }

                    break;
                case PLAYER_STATE.RUN:
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        _playerState = PLAYER_STATE.SHIFT;
                        _shiftDir = SHIFT_DIR.LEFT;
                    }

                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        _playerState = PLAYER_STATE.SHIFT;
                        _shiftDir = SHIFT_DIR.RIGHT;
                    }

                    break;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Goal")
            {
                _playerState = PLAYER_STATE.IDLE;
            }
        }
    }
}