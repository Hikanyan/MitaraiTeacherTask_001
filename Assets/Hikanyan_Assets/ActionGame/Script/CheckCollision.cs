using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hikanyan_Assets.ActionGame.Script
{
    public class CheckCollision:MonoBehaviour
    {
        // 円と矩形の判定
        public bool CollisionCheck(Vector2 circleCenter, float circleRadius, Rect rectangle)
        {
            // 円の中心座標を矩形の領域内に制約する
            float constrainedX = Mathf.Clamp(circleCenter.x, rectangle.x, rectangle.x + rectangle.width);
            float constrainedY = Mathf.Clamp(circleCenter.y, rectangle.y, rectangle.y + rectangle.height);

            // 制約された座標と円の中心座標の距離を計算
            float distance = Vector2.Distance(circleCenter, new Vector2(constrainedX, constrainedY));

            // 距離が円の半径以下であれば、円と矩形は重なっている
            if (distance <= circleRadius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}