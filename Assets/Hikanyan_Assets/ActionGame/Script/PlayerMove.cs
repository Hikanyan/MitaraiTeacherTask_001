using System;
using UnityEngine;

namespace Hikanyan_Assets.ActionGame.Script
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private GameObject _pointer;
        private Transform _transform;

        private void Start()
        {
            _transform = this.transform;
            _pointer = Instantiate(_pointer, new Vector3(this.transform.position.x, this.transform.position.y, 10),
                Quaternion.identity, _transform);
        }

        private void Update()
        {
            Pointer();
        }

        void BulletShot()
        {
            
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