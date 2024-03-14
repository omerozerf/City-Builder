using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button _buildResidentialAreaButton;
        [SerializeField] private GameObject _cancelActionPanel;
        [SerializeField] private Button _confirmActionButton;
        [SerializeField] private Button _cancelActionButton;
        [SerializeField] private GameObject _buildingMenuPanel;
        [SerializeField] private Button _openBuildMenuButton;
        [SerializeField] private Button _demolishButton;
        [SerializeField] private GameObject _zonesPanel;
        [SerializeField] private GameObject _facilitiesPanel;
        [SerializeField] private GameObject _roadsPanel;
        [SerializeField] private Button _closeBuildMenuButton;
        [SerializeField] private GameObject _buildButtonPrefab;
        [SerializeField] private StructureRepository _structureRepository;
    

        public event Action<string> OnBuildAreaButtonClicked;
        public event Action<string> OnBuildSingleStructureButtonClicked;
        public event Action<string> OnBuildRoadButtonClicked;
        public event Action OnConfirmActionButtonClicked;
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
            _confirmActionButton.onClick.AddListener(OnConfirmActionCallback);
        }
        
        private void RemoveListeners()
        {
            // _buildResidentialAreaButton.onClick.RemoveListener(OnBuildAreaCallback);
            _cancelActionButton.onClick.RemoveListener(OnCancelActionCallback);
            _openBuildMenuButton.onClick.RemoveListener(OnOpenBuildMenuCallback);
            _demolishButton.onClick.RemoveListener(OnDemolishButtonCallback);
            _closeBuildMenuButton.onClick.RemoveListener(OnCloseMenuCallback);
            _confirmActionButton.onClick.RemoveListener(OnConfirmActionCallback);
        }
        
        
        private void OnConfirmActionCallback()
        {
            _cancelActionPanel.SetActive(false);
            
            OnConfirmActionButtonClicked?.Invoke();
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

        private void OnBuildAreaCallback(string nameOfStructure)
        {
            PrepareUIForBuilding();
            OnBuildAreaButtonClicked?.Invoke(nameOfStructure);
        }
        
        private void OnBuildSingleStructureCallback(string nameOfStructure)
        {
            PrepareUIForBuilding();
            OnBuildSingleStructureButtonClicked?.Invoke(nameOfStructure);
        }
        
        private void OnBuildRoadCallback(string nameOfStructure)
        {
            PrepareUIForBuilding();
            OnBuildRoadButtonClicked?.Invoke(nameOfStructure);
        }

        private void PrepareUIForBuilding()
        {
            _cancelActionPanel.SetActive(true);
            OnCloseMenuCallback();
        }

        private void OnCancelActionCallback()
        {
            _cancelActionPanel.SetActive(false);
            
            OnCancelActionButtonClicked?.Invoke();
        }


        private void PrepareBuildMenu()
        {
            CreateButtonsInPanel(_zonesPanel.transform,
                _structureRepository.GetZonesNameList(), OnBuildAreaCallback);
            
            CreateButtonsInPanel(_facilitiesPanel.transform,
                _structureRepository.GetFacilityNameList(), OnBuildSingleStructureCallback);
            
            CreateButtonsInPanel(_roadsPanel.transform,
                new List<string> {_structureRepository.GetRoadStructureName()}, OnBuildRoadCallback);
        }

        private void CreateButtonsInPanel(Transform panelTransform, List<string> dataToShow, Action<string> callback)
        {
            if (dataToShow.Count > panelTransform.childCount)
            {
                int quantityDifference = dataToShow.Count - panelTransform.childCount;
                
                for (int i = 0; i < quantityDifference; i++)
                {
                    Instantiate(_buildButtonPrefab, panelTransform);
                }
            }
            
            for (int i = 0; i < panelTransform.childCount; i++)
            {
                var button = panelTransform.GetChild(i).GetComponent<Button>();
                if (button)
                {
                    button.onClick.RemoveAllListeners();
                    
                    button.GetComponentInChildren<TextMeshProUGUI>().text = dataToShow[i];
                    button.onClick.AddListener(() => callback(button.name));
                }
            }
        }
    }
}