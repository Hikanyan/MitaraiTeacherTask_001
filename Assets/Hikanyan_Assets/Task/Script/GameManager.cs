using System;
using UnityEngine;

namespace Hikanyan_Assets.Task.Script
{
    public class GameManager : AbstractSingleton<GameManager>
    {
        public enum GAME_STATE
        {
            NONE,
            TITEL,
            STRAT,
            CLEAR,
            GAMEOVER
        }

        public int Point;

        public GAME_STATE GameState = GAME_STATE.NONE;
        //[SerializeField]

        private void Start()
        {
            
        }

        private void Update()
        {
            switch (GameState)
            {
                case GAME_STATE.STRAT:
                    Debug.Log(Point);
                    break;
            }
        }
    }
}