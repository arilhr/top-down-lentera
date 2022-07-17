using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace TopDownLentera
{

    public class UIManager : MonoBehaviour
    {

        #region Variables

        public static UIManager Instance;

        public TMP_Text healthText;
        public GameObject pausePanel;

        public GameObject winPanel;

        private Player _player;

        public UnityEvent OnExitGame;

        #endregion

        #region Mono Behaviours

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

        #endregion

        #region Methods

        public void Pause()
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }

        public void Resume()
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }

        public void Win()
        {
            winPanel.SetActive(true);
        }

        public void Exit()
        {
            Time.timeScale = 1;
            OnExitGame?.Invoke();
        }

        #endregion
    }
}
