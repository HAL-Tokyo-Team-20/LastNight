using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prosthetic
{
    public List<string> ProstheticTypeName = new List<string> {
        "Entry",
        "Entry_0",
        "Entry_1",
        "Entry_2",
    };

    public ProstheticType Type;

    public virtual void SkillActive(Vector3 offset) { }
}