using System;
using UnityEngine;
namespace Hikanyan_Assets.ActionGame.Script
{
    public class Bullet:MonoBehaviour,IAttack
    {
        [SerializeField] private int _attackDamage;
        [SerializeField] private float _speed;
        private Transform _bulletPos;

        public int AddAttack()
        {
            return _attackDamage;
        } 
        private void Update()
        {
            transform.position += new Vector3(_speed* Time.deltaTime, 0, 0);
        }
    }
}