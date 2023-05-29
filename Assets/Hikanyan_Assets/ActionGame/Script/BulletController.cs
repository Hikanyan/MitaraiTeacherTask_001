using System;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hikanyan_Assets.ActionGame.Script
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] GameObject _bulletPrefab; // 弾丸のプレハブ
        [SerializeField] GameObject _laserPrefab; // レーザーのプレハブ
        [SerializeField] GameObject _pointerPrefab; // ポインターのプレハブ
        [SerializeField] GameObject _bulletPoint; //球を出現させる場所
        [SerializeField] KeyCode _activationKey = KeyCode.F; // 発射キー
        [SerializeField] float _longPressDuration = 1f; // 長押しの時間（秒）

        private GameObject _pointer; //ポインタのプレハブ

        private async void Start()
        {
            //ポインタの初期化
            var _transform = this.transform;
            _pointer = Instantiate(_pointerPrefab,
                new Vector3(this.transform.position.x + 2, this.transform.position.y, 10),
                Quaternion.identity, _transform);

            // キーの長押しを検出するためのSubjectを作成
            var longPressSubject = new Subject<Unit>();

            // キーの長押しを監視
            var longPressObservable = Observable.EveryUpdate()
                .Where(_ => Input.GetKey(_activationKey))
                .Select(_ => Time.deltaTime)
                .Scan((acc, deltaTime) => acc + deltaTime)
                .TakeWhile(time => time < _longPressDuration)
                .LastOrDefault()
                .Publish();

            // キーが単押しされたら弾丸を発射
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(_activationKey))
                .Subscribe(_ => ShootBullet(_bulletPoint))
                .AddTo(this);
            
            // キーが1秒間長押しされたらレーザーを発射
            longPressObservable
                .Take(1)
                .Subscribe(_ => ShootLaser(_bulletPoint))
                .AddTo(this);

            longPressObservable.Connect();
        }

        private void Update()
        {
            Pointer();
        }

        void Pointer()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = mousePosition - playerPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            _pointer.transform.position = target;
        }

        private async UniTask ShootBullet(GameObject target)
        {
            // 弾丸を生成して発射
            GameObject bullet = Instantiate(_bulletPrefab,
                new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z),
                Quaternion.identity);
            bullet.transform.rotation = transform.rotation;
            await UniTask.Delay(10000);
            Destroy(bullet); // 弾丸を破棄
        }

        private async UniTask ShootLaser(GameObject target)
        {
            // レーザーを生成して発射
            GameObject laser = Instantiate(_laserPrefab,
                new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z),
                Quaternion.identity);
            laser.transform.rotation = transform.rotation;
            await UniTask.DelayFrame(12); // 0.2秒（12フレーム）待機
            Destroy(laser); // レーザーを破棄
        }
    }
}