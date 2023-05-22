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
                if (this.gameObject.CompareTag("Player"))
                {
                    this.gameObject.GetComponent<PlayerMove>().enabled = false;
                    this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
                else if (this.gameObject.CompareTag("Enemy"))
                {
                    this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}