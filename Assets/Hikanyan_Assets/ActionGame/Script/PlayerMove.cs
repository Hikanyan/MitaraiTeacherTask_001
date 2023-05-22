using System;
using UnityEngine;
using System.Collections; 

namespace Hikanyan_Assets.ActionGame.Script
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] float _moveSpeed = 5.0f;
        
        [SerializeField] private GameObject _pointer;
        [SerializeField] private GameObject _bullet;
        private Transform _transform;
        private float _gravity = -9.8f;

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
            Move();
            Pointer();
            _timer += Time.deltaTime;
            if (_timer  >= _count)
            {
                BulletShot();
                _timer = 0;
            }
        }

        void Move()
        {
            var posY = _transform.position.y;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                posY += _moveSpeed * Time.deltaTime;
            }
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"),posY).normalized * (_moveSpeed * Time.deltaTime);
        }

        void Gravity()
        {
            var posY = _transform.position.y;
            if (transform.position.y > 0)
            {
                posY += _gravity* Time.deltaTime;
                transform.position += new Vector3(transform.position.x,posY );
                
                Gravity();
            }
            else
            {
                posY = 0;
                transform.position += new Vector3(transform.position.x,posY );
            }
        }

        

        void BulletShot()
        {
            Instantiate(_bullet,new Vector3(_transform.position.x,_transform.position.y,_transform.position.z),Quaternion.identity);
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