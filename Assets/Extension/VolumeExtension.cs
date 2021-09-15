using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Rendering.Universal.Extension
{
    public static class VolumeExtension
    {
        public static Tweener DOFloat_DofFocusDistance(this DepthOfField target,float endValue,float duration)
        {
            return DOTween.To(() => target.focusDistance.value, x => target.focusDistance.value = x, endValue, duration).SetTarget(target);
        }

        public static Tweener DOFloat_DofFocalLength(this DepthOfField target, float endValue, float duration)
        {
            return DOTween.To(() => target.focalLength.value, x => target.focalLength.value = x, endValue, duration).SetTarget(target);
        }

        public static Tweener DOFloat_DofAperture(this DepthOfField target, float endValue, float duration)
        {
            return DOTween.To(() => target.aperture.value, x => target.aperture.value = x, endValue, duration).SetTarget(target);
        }
    }
}

