﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hikanyan_Assets.RunGame.Script
{
    public class CoinGenerator : MonoBehaviour
    {
        [SerializeField] GameObject _coinPrefab;
        List<GameObject> _list = new List<GameObject>();
        
        [SerializeField] List<Transform> linePoint = new List<Transform>();
        [SerializeField] private float offset = 1;
        [SerializeField] private int num = 10;
        private void Start()
        {
            var parent = this.transform;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    Instantiate(_coinPrefab, new Vector3(linePoint[i].position.x, 1, -47+(j * offset)), Quaternion.identity,parent);
                }
            }
        }
    }
}