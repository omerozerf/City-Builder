using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _buildResidentialAreaButton;
    [SerializeField] private Button _cancelActionButton;
    [SerializeField] private GameObject _cancelActionPanel;

    public event Action OnBuildResidentialAreaButtonClicked;
    public event Action OnCancelActionButtonClicked;
        
    private void Awake()
    {
        _cancelActionPanel.SetActive(false);
            
        _buildResidentialAreaButton.onClick.AddListener(OnBuildAreaCallback);
        _cancelActionButton.onClick.AddListener(OnCancelActionCallback);
    }
        
    private void OnDestroy()
    {
        _buildResidentialAreaButton.onClick.RemoveListener(OnBuildAreaCallback);
        _cancelActionButton.onClick.RemoveListener(OnCancelActionCallback);
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