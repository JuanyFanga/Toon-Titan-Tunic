using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Shoot();
    void Reload();

    bool HasBullet();
    //void Parry();
}
