using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    void Move(Vector3 dir);
    void Look(Vector3 dir);
    void Dash();
    void Interact();
    void PickupBullet();
    void Parry();
    void Die();
}