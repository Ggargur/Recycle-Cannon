using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetallicEnemy : Enemy
{
    public override MaterialType Type
    {
        get => MaterialType.Metallic;
    }
}
