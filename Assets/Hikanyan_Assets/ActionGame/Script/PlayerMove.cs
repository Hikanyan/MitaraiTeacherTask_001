using System;
using UnityEngine;
using System.Collections; 

namespace Hikanyan_Assets.ActionGame.Script
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private GameObject _pointer;
        private Transform _transform;
        [SerializeField] private GameObject _bullet;

        private float _timer;
        private float _count=1;
        private void Start()
        {
            _transform = this.transform;
            _pointer = Instantiate(_pointer, new Vector3(this.transform.position.x, this.transform.position.y, 10),
                Quaternion.identity, _transform);
        }

        private void Update()
        {
            Pointer();
            _timer += Time.deltaTime;
            if (_timer  >= _count)
            {
                BulletShot();
                _timer = 0;
            }
        }

        void BulletShot()
        {
            Instantiate(_bullet,_transform);
        }

        void Pointer()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
            _pointer.transform.position = target;
        }
    }
}