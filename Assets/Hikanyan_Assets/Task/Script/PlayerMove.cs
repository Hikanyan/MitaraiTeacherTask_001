using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hikanyan_Assets.Task.Script
{
    public class PlayerMove : MonoBehaviour
    {
        //プレイヤー状態定義
        public enum PLAYER_STATE
        {
            RUN,        //走る
            SHIFT,      //横移動
            STOP,       //停止
        }
        //横移動方向定義
        public enum SHIFT_DIR
        {
            NONE,       //通常
            LEFT,       //左側
            RIGHT       //右側
        }
        //Playerの直線移動スピード
        [SerializeField]
        float _playerSpeed = 1;
        //横移動量
        [SerializeField]
        float _shiftValue = 1;
        //現在のエリアラインのインデックス
        int _currentAreaLineIdx = 0;
        SHIFT_DIR _shiftDir = SHIFT_DIR.NONE;
        void Awake()
        {
            
        }
        void Update()
        {
            
        }

        void Front()
        {
            transform.position += new Vector3(0, 0, _playerSpeed * Time.deltaTime);
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Goal")
            {
                
            }
        }
    }
}