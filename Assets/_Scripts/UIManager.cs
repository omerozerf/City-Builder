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
    [SerializeField] private GameObject _zonesPanel;
    [SerializeField] private GameObject _facilitiesPanel;
    [SerializeField] private GameObject _roadsPanel;
    [SerializeField] private Button _closeBuildMenuButton;
    [SerializeField] private GameObject _buildButtonPrefab;
    

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
        // _buildResidentialAreaButton.onClick.AddListener(OnBuildAreaCallback);
        _cancelActionButton.onClick.AddListener(OnCancelActionCallback);
        _openBuildMenuButton.onClick.AddListener(OnOpenBuildMenuCallback);
        _demolishButton.onClick.AddListener(OnDemolishButtonCallback);
        _closeBuildMenuButton.onClick.AddListener(OnCloseMenuCallback);
    }
    
    private void RemoveListeners()
    {
        // _buildResidentialAreaButton.onClick.RemoveListener(OnBuildAreaCallback);
        _cancelActionButton.onClick.RemoveListener(OnCancelActionCallback);
        _openBuildMenuButton.onClick.RemoveListener(OnOpenBuildMenuCallback);
        _demolishButton.onClick.RemoveListener(OnDemolishButtonCallback);
        _closeBuildMenuButton.onClick.RemoveListener(OnCloseMenuCallback);
    }
    
    
    private void OnCloseMenuCallback()
    {
        _buildingMenuPanel.SetActive(false);
        
    }
    
    private void OnDemolishButtonCallback()
    {
        OnDemolishButtonClicked?.Invoke();
        _cancelActionPanel.SetActive(true);
        OnCloseMenuCallback();
    }

    private void OnOpenBuildMenuCallback()
    {
        _buildingMenuPanel.SetActive(true);
        
        PrepareBuildMenu();
    }    

    private void OnBuildAreaCallback()
    {
        _cancelActionPanel.SetActive(true);
        OnCloseMenuCallback();   
        OnBuildResidentialAreaButtonClicked?.Invoke();
    }
        
    private void OnCancelActionCallback()
    {
        _cancelActionPanel.SetActive(false);
            
        OnCancelActionButtonClicked?.Invoke();
    }


    private void PrepareBuildMenu()
    {
        CreateButtonsInPanel(_zonesPanel.transform);
        CreateButtonsInPanel(_facilitiesPanel.transform);
        CreateButtonsInPanel(_roadsPanel.transform);
    }

    private void CreateButtonsInPanel(Transform panelTransform)
    {
        foreach (Transform childTransform in panelTransform)
        {
            Button button = childTransform.GetComponent<Button>();

            if (button)
            {
                button.onClick.AddListener(OnBuildAreaCallback);
            }
        }
    }
}