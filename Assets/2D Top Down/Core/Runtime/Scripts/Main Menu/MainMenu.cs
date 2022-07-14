using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TopDownLentera.UI
{
    public class MainMenu : MonoBehaviour
    {
        #region Variables

        public UnityEvent OnPlay;

        #endregion

        #region Methods

        public void Play()
        {
            OnPlay?.Invoke();
        }

        #endregion
    }
}
