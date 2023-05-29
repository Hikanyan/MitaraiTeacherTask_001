using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hikanyan_Assets.ActionGame.Script {
    public abstract class HealthComponent : MonoBehaviour
    {
        [SerializeField] protected float _maxHp;
        [SerializeField] protected float _life;
        public float Life => _life;

        public void Awake()
        {
            _life = _maxHp;
        }
        public void Damage(float damegePoint)
        {
            _life -= damegePoint;

            if (_life <= 0)
            {
                _life = 0;
                Destroy(this.gameObject);
            }
        }
    }
}