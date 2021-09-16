using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.Universal.Extension;
using DG.Tweening;

public class GlobalVolumeController : UnitySingleton<GlobalVolumeController>
{

    private Volume volume;
    private DepthOfField dof;

    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet<DepthOfField>(out dof);
    }

    public void ShiftFocusDistance(float target_distance,float duration)
    {
        if (dof && dof.active)
            dof.DOFloat_DofFocusDistance(target_distance,duration);
        else
            Debug.LogWarning("Depth Of Field in GlobalVolume Not be Active ! ");
    }
    public void ShiftFocalLength(float focallength,float duration)
    {
        if (dof && dof.active && (focallength >= 1 && focallength <= 300))
            dof.DOFloat_DofFocalLength(focallength, duration);
        else
            Debug.LogWarning("Depth Of Field in GlobalVolume Problem ! ");
    }
    public void ShiftAperture(float aperture,float duration)
    {
        if (dof && dof.active && (aperture >= 1 && aperture <= 32))
            dof.DOFloat_DofAperture(aperture, duration);
        else
            Debug.LogWarning("Depth Of Field in GlobalVolume Problem ! ");
    }
}
