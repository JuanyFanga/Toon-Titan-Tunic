using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    void Move();
    void Dash();
    void Interact();
    void PickupBullet();
    void Parry();
    void Die();
}