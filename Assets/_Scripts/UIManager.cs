using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _buildResidentialAreaButton;
    [SerializeField] private GameObject _cancelActionPanel;
    [SerializeField] private Button _cancelActionButton;
    [SerializeField] private GameObject _buildingMenuPanel;
    [SerializeField] private Button _openBuildMenuButton;
    [SerializeField] private Button _demolishButton;

    public event Action OnBuildResidentialAreaButtonClicked;
    public event Action OnCancelActionButtonClicked;
    public event Action OnDemolishButtonClicked;
        
    private void Awake()
    {
        _cancelActionPanel.SetActive(false);
        _buildingMenuPanel.SetActive(false);
        
        InitializeListeners();
    }
    
    private void OnDestroy()
    {
        RemoveListeners();
    }
    

    private void InitializeListeners()
    {
        _buildResidentialAreaButton.onClick.AddListener(OnBuildAreaCallback);
        _cancelActionButton.onClick.AddListener(OnCancelActionCallback);
        _openBuildMenuButton.onClick.AddListener(OnOpenBuildMenuCallback);
        _demolishButton.onClick.AddListener(OnDemolishButtonCallback);
    }
    
    private void RemoveListeners()
    {
        _buildResidentialAreaButton.onClick.RemoveListener(OnBuildAreaCallback);
        _cancelActionButton.onClick.RemoveListener(OnCancelActionCallback);
        _openBuildMenuButton.onClick.RemoveListener(OnOpenBuildMenuCallback);
        _demolishButton.onClick.RemoveListener(OnDemolishButtonCallback);
    }
    
    
    private void OnDemolishButtonCallback()
    {
        OnDemolishButtonClicked?.Invoke();
        _cancelActionPanel.SetActive(true);
        _buildingMenuPanel.SetActive(false);
    }

    private void OnOpenBuildMenuCallback()
    {
        _buildingMenuPanel.SetActive(true);
    }    

    private void OnBuildAreaCallback()
    {
        _cancelActionPanel.SetActive(true);
        _buildingMenuPanel.SetActive(false);    
        
        OnBuildResidentialAreaButtonClicked?.Invoke();
    }
        
    private void OnCancelActionCallback()
    {
        _cancelActionPanel.SetActive(false);
            
        OnCancelActionButtonClicked?.Invoke();
    }
}