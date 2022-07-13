using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TopDownLentera
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        public TMP_Text healthText;

        private Player _player;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }


        private void Start()
        {
            _player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            healthText.text = "HP: " + _player.health.ToString();
        }
    }
}
