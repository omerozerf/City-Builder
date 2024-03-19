using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    private Action<string> m_OnBuildAreaHandler;
    private Action<string> m_OnBuildSingleStructureHandler;
    private Action<string> m_OnBuildRoadHandler;

    private Action m_OnCancleActionHandler;
    private Action m_OnConfirmActionHandler;
    private Action m_OnDemolishActionHandler;

    [FormerlySerializedAs("structureRepository")] public StructureRepository _structureRepository;
    [FormerlySerializedAs("buildResidentialAreaBtn")] public Button _buildResidentialAreaBtn;
    [FormerlySerializedAs("cancleActionBtn")] public Button _cancleActionBtn;
    [FormerlySerializedAs("confirmActionBtn")] public Button _confirmActionBtn;
    [FormerlySerializedAs("cancleActionPanel")] public GameObject _cancleActionPanel;

    [FormerlySerializedAs("buildingMenuPanel")] public GameObject _buildingMenuPanel;
    [FormerlySerializedAs("openBuildMenuBtn")] public Button _openBuildMenuBtn;
    [FormerlySerializedAs("demolishBtn")] public Button _demolishBtn;

    [FormerlySerializedAs("zonesPanel")] public GameObject _zonesPanel;
    [FormerlySerializedAs("facilitiesPanel")] public GameObject _facilitiesPanel;
    [FormerlySerializedAs("roadsPanel")] public GameObject _roadsPanel;
    [FormerlySerializedAs("closeBuildMenuBtn")] public Button _closeBuildMenuBtn;

    [FormerlySerializedAs("buildButtonPrefab")] public GameObject _buildButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _cancleActionPanel.SetActive(false);
        _buildingMenuPanel.SetActive(false);
        //buildResidentialAreaBtn.onClick.AddListener(OnBuildAreaCallback);
        _cancleActionBtn.onClick.AddListener(OnCancleActionCallback);
        _confirmActionBtn.onClick.AddListener(OnConfirmActionCallback);
        _openBuildMenuBtn.onClick.AddListener(OnOpenBuildMenu);
        _demolishBtn.onClick.AddListener(OnDemolishHandler);
        _closeBuildMenuBtn.onClick.AddListener(OnCloseMenuHandler);
    }

    private void OnConfirmActionCallback()
    {
        _cancleActionPanel.SetActive(false);
        m_OnConfirmActionHandler?.Invoke();
    }

    private void OnCloseMenuHandler()
    {
        _buildingMenuPanel.SetActive(false);
    }

    private void OnDemolishHandler()
    {
        m_OnDemolishActionHandler?.Invoke();
        _cancleActionPanel.SetActive(true);
        OnCloseMenuHandler();
    }

    private void OnOpenBuildMenu()
    {
        _buildingMenuPanel.SetActive(true);
        PrepareBuildMenu();
    }

    private void PrepareBuildMenu()
    {
        CreateButtonsInPanel(_zonesPanel.transform, _structureRepository.GetZoneNames(), OnBuildAreaCallback);
        CreateButtonsInPanel(_facilitiesPanel.transform, _structureRepository.GetSingleStructureNames(), OnBuildSingleStructureCallback);
        CreateButtonsInPanel(_roadsPanel.transform, new List<string>() { _structureRepository.GetRoadStructureName() }, OnBuildRoadCallback);
    }

    private void OnBuildRoadCallback(string nameOfStructure)
    {
        PrepareUIForBuilding();
        m_OnBuildRoadHandler?.Invoke(nameOfStructure);
    }

    private void OnBuildSingleStructureCallback(string nameOfStructure)
    {
        PrepareUIForBuilding();
        m_OnBuildSingleStructureHandler?.Invoke(nameOfStructure);
    }

    private void PrepareUIForBuilding()
    {
        _cancleActionPanel.SetActive(true);
        OnCloseMenuHandler();
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
            if (button != null)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = dataToShow[i];
                button.onClick.AddListener(() => callback(button.GetComponentInChildren<TextMeshProUGUI>().text));
            }
        }
    }

    private void OnBuildAreaCallback(string nameOfStructure)
    {
        PrepareUIForBuilding();
        m_OnBuildAreaHandler?.Invoke(nameOfStructure);
    }

    private void OnCancleActionCallback()
    {
        _cancleActionPanel.SetActive(false);
        m_OnCancleActionHandler?.Invoke();
    }

    public void AddListenerOnBuildAreaEvent(Action<string> listener)
    {
        m_OnBuildAreaHandler += listener;
    }

    public void RemoveListenerOnBuildAreaEvent(Action<string> listener)
    {
        m_OnBuildAreaHandler -= listener;
    }
    public void AddListenerOnCancleActionEvent(Action listener)
    {
        m_OnCancleActionHandler += listener;
    }

    public void RemoveListenerOnCancleActionEvent(Action listener)
    {
        m_OnCancleActionHandler -= listener;
    }

    public void AddListenerOnDemolishActionEvent(Action listener)
    {
        m_OnDemolishActionHandler += listener;
    }

    public void RemoveListenerOnDemolishActionEvent(Action listener)
    {
        m_OnDemolishActionHandler -= listener;
    }
    public void AddListenerOnBuildSingleStructureEvent(Action<string> listener)
    {
        m_OnBuildSingleStructureHandler += listener;
    }

    public void RemoveListenerOnBuildSingleStructureEvent(Action<string> listener)
    {
        m_OnBuildSingleStructureHandler -= listener;
    }

    public void AddListenerOnBuildRoadEvent(Action<string> listener)
    {
        m_OnBuildRoadHandler += listener;
    }

    public void RemoveListenerOnBuildRoadEvent(Action<string> listener)
    {
        m_OnBuildRoadHandler -= listener;
    }

    public void AddListenerOnConfirmActionEvent(Action listener)
    {
        m_OnConfirmActionHandler += listener;
    }

    public void RemoveListenerOnConfirmActionEvent(Action listener)
    {
        m_OnConfirmActionHandler -= listener;
    }
}
