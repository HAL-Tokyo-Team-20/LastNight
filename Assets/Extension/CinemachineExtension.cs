using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

namespace Cinemachine.Extension
{
    public static class CinemachineExtension
    {
        public static Tweener DOVector3_FollowOffset(this CinemachineTransposer target, Vector3 endValue, float duration)
        {
            return DOTween.To(() => target.m_FollowOffset, x => target.m_FollowOffset = x, endValue, duration).SetTarget(target);
        }

        public static Tweener DOFloat_DollyPathPosition(this CinemachineTrackedDolly target, float endValue, float duration)
        {
            return DOTween.To(() => target.m_PathPosition, x => target.m_PathPosition = x, endValue, duration).SetTarget(target);
        }
    }
}
