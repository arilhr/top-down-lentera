using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Sprite weaponSprite;

    public abstract void Attack(Character owner, string target);
}
