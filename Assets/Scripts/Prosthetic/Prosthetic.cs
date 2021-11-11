using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prosthetic
{
    public ProstheticType Type;

    public virtual void SkillActive(Vector3 offset) { }

    public virtual float GetCoolTime() { return 0.0f; }
}