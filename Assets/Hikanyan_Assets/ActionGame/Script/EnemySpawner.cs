using System.Collections.Generic;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Hikanyan_Assets.ActionGame.Script
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab; // 敵のプレハブ
        public List<GameObject> spawnPoints; // 敵の出現位置のリスト
        public float spawnInterval = 3f; // スポーンの間隔（秒）

        private async void Start()
        {
            // スポーンを開始するためのAsyncSubjectを作成
            var spawnSubject = new AsyncSubject<Unit>();

            // スポーンの間隔ごとに敵をスポーン
            Observable.Interval(System.TimeSpan.FromSeconds(spawnInterval))
                .TakeUntilDestroy(this)
                .Subscribe(_ =>
                {
                    SpawnEnemy();
                    spawnSubject.OnNext(Unit.Default);
                });

            // 敵がいない場合にのみスポーンを待つ
            while (true)
            {
                await spawnSubject;
                if (!IsEnemyPresent())
                {
                    // 敵がいない場合は次のスポーンまで待機
                    await UniTask.Delay(System.TimeSpan.FromSeconds(spawnInterval));
                }

                spawnSubject = new AsyncSubject<Unit>();
            }
        }

        private bool IsEnemyPresent()
        {
            // 敵の存在をチェックするロジックを実装
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            return enemies.Length > 0;
        }

        private void SpawnEnemy()
        {
            // ランダムな出現位置を選択
            GameObject spawnPoint = GetRandomSpawnPoint();

            if (spawnPoint != null)
            {
                // 敵を生成して出現させる
                Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            }
        }

        private GameObject GetRandomSpawnPoint()
        {
            // 出現位置のリストからランダムに位置を選択
            if (spawnPoints.Count == 0)
            {
                Debug.LogWarning("No spawn points available!");
                return null;
            }

            int randomIndex = Random.Range(0, spawnPoints.Count);
            return spawnPoints[randomIndex];
        }
    }
}
