using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public sealed class MainMenuController : MonoBehaviour
    {
        [SerializeField] private RectTransform _mainMenuPanel;
        [SerializeField] private RectTransform _abountAuthorsPanel;
        [SerializeField] private Button _openAboutAuthor;
        [SerializeField] private Button _closeAboutAuthor;
        private void Awake()
        {
            _openAboutAuthor.onClick.AddListener(() => SetStatePanels(false));
            _closeAboutAuthor.onClick.AddListener(() => SetStatePanels(true));
            SetStatePanels(true);
        }

        private void OnDestroy()
        {
            _openAboutAuthor.onClick.RemoveAllListeners();
            _closeAboutAuthor.onClick.RemoveAllListeners();
        }

        private void SetStatePanels(bool value)
        {
            _mainMenuPanel.gameObject.SetActive(value);
            _abountAuthorsPanel.gameObject.SetActive(!value);
        }
    }
}