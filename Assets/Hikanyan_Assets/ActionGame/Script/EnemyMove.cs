using System;
using UnityEngine;

namespace Hikanyan_Assets.ActionGame.Script
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;

        void BulletShot()
        {
            Instantiate(_bullet);
        }
    }
}