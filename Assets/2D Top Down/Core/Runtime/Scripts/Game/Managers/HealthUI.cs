using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownLentera
{
    public class HealthUI : MonoBehaviour
    {
        #region Variables

        public TMP_Text healthText;

        private Character _character;

        #endregion

        #region Mono Behaviours

        private void Start()
        {
            _character = GetComponentInParent<Character>();
        }

        private void Update()
        {
            SetHealth();
        }

        #endregion

        #region Methods

        public void SetHealth()
        {
            if (_character == null) return;
            
            healthText.text = _character.health.ToString();
        }

        #endregion
    }
}
