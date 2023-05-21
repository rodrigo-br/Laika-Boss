using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage();
}

public interface ICollisive
{
    void Collided();
}

public interface IDimensionTraveler
{
    bool IsMainDimension { get; }

    void DimensionChecker();
}
