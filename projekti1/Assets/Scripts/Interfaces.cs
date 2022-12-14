using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float dmg, float kb);
}
public interface IInteractable
{
    void Interact();
}
public interface IHasHealth
{
    float GetHealth();
}

public interface IHasOrigin
{
    void SetOrigin(Spawner origin);
}
