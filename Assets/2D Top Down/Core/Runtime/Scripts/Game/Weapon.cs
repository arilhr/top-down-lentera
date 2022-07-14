using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownLentera
{
    public abstract class Weapon : MonoBehaviour
    {
        public Sprite weaponSprite;

        public abstract void Attack(Character owner, string target, Vector2 direction);
    }
}
