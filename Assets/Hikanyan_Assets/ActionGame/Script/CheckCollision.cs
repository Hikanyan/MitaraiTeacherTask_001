using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hikanyan_Assets.ActionGame.Script
{
    public class CheckCollision:MonoBehaviour
    {
        // �~�Ƌ�`�̔���
        public bool CollisionCheck(Vector2 circleCenter, float circleRadius, Rect rectangle)
        {
            // �~�̒��S���W����`�̗̈���ɐ��񂷂�
            float constrainedX = Mathf.Clamp(circleCenter.x, rectangle.x, rectangle.x + rectangle.width);
            float constrainedY = Mathf.Clamp(circleCenter.y, rectangle.y, rectangle.y + rectangle.height);

            // ���񂳂ꂽ���W�Ɖ~�̒��S���W�̋������v�Z
            float distance = Vector2.Distance(circleCenter, new Vector2(constrainedX, constrainedY));

            // �������~�̔��a�ȉ��ł���΁A�~�Ƌ�`�͏d�Ȃ��Ă���
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