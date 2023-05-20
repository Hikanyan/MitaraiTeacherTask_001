using System;
using UnityEngine;
using UnityEngine.UI;
namespace Hikanyan_Assets.RunGame.Script
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
        [SerializeField] private Text _scoreText;

        private void Start()
        {
            
        }

        private void Update()
        {
            switch (GameState)
            {
                case GAME_STATE.STRAT:
                    Debug.Log(Point);
                    _scoreText.text = Point.ToString();
                    break;
            }
        }
    }
}