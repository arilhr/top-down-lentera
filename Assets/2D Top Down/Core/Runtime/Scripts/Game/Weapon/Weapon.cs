using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TopDownLentera
{
    public abstract class Weapon : MonoBehaviour
    {
        public Sprite weaponSprite;
        public UnityEvent OnAttack;
        
        public virtual void Attack(Character owner, string target, Vector2 direction)
        {
            OnAttack?.Invoke();
        }
    }
}
