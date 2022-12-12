using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganicEnemy : Enemy
{
    public override MaterialType Type
    {
        get => MaterialType.Organic;
    }
}
