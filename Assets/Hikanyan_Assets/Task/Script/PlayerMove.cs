using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hikanyan_Assets.Task.Script
{
    public class PlayerMove : MonoBehaviour
    {
        Rigidbody _rigidBody;
        [SerializeField]
        Vector3 force = new Vector3(0.0f, 0.0f, -30.0f);
        bool _isStop = false;

        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            //TODO
            if (!_isStop)
            {
                if (_rigidBody.velocity.magnitude < 10.0f)
                {
                    _rigidBody.AddForce(force);
                }
            }
            else
            {
                _rigidBody.velocity = new Vector3(0, 0, 0);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Goal")
            {
                _isStop = true;
                _rigidBody.velocity = new Vector3(0, 0, 0);
            }
        }
    }
}