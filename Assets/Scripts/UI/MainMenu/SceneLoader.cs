using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    [System.Serializable]
    public struct SceneButtonPair
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private Button _activateButton;
        [SerializeField] private bool _isButtonForQuit;
        public string SceneName => _sceneName;
        public Button ActivateButton => _activateButton;
        public bool IsButtonForQuit => _isButtonForQuit;
    }
    public sealed class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneButtonPair[] _sceneButtonPairs;

        private void OnEnable()
        {
            foreach (var pair in _sceneButtonPairs)
            {
                if (pair.ActivateButton == null) continue;

                pair.ActivateButton.onClick.RemoveAllListeners();

                if (pair.IsButtonForQuit)
                    pair.ActivateButton.onClick.AddListener(QuitApplication); 
                else
                    pair.ActivateButton.onClick.AddListener(() => LoadScene(pair.SceneName));
            }
        }

        private void OnDisable()
        {
            foreach (var sceneButtonPair in _sceneButtonPairs)
                sceneButtonPair.ActivateButton?.onClick.RemoveAllListeners();
        }

        private void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

        private void QuitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}