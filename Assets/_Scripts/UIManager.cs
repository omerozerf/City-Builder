using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button _buildResidentialAreaButton;
        [SerializeField] private Button _cancelActionButton;
        [SerializeField] private GameObject _cancelActionPanel;

        public event Action OnBuildResidentialAreaButtonClicked;
        public event Action OnCancelActionButtonClicked;
        
        private void Start()
        {
            _cancelActionPanel.SetActive(false);
        }

        private void OnBuildAreaCallback()
        {
            _cancelActionPanel.SetActive(true);
            
            OnBuildResidentialAreaButtonClicked?.Invoke();
        }
        
        private void OnCancelActionCallback()
        {
            _cancelActionPanel.SetActive(false);
            
            OnCancelActionButtonClicked?.Invoke();
        }
    }
}
