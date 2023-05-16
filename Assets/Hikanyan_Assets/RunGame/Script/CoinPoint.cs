using System;
using System.Collections.Generic;
using UnityEngine;
using Hikanyan_Assets.RunGame.Script;

namespace Hikanyan_Assets.RunGame.Script
{
    public class CoinPoint : MonoBehaviour, IPoint
    {
        [SerializeField] private int _point = 10;
        public int AddCoinPoint()
        {
            GameManager.Instance.Point += _point;
            return _point;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Destroy(this.gameObject);
            }
        }
    }
}