using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ResourceManager : MonoBehaviour, IResourceManager
{
    [FormerlySerializedAs("startMoneyAmount")] [SerializeField]
    private int _startMoneyAmount = 5000;
    [FormerlySerializedAs("demolitionPrice")] [SerializeField]
    private int _demolitionPrice = 20;
    [FormerlySerializedAs("moneyCalculationInterval")] [SerializeField]
    private float _moneyCalculationInterval = 2;

    private MoneyHelper m_MoneyHelper;
    private PopulationHelper m_PopulationHelper;
    private BuildingManager m_BuildingManger;
    [FormerlySerializedAs("uiController")] public UiController _uiController;

    public int StartMoneyAmount { get => _startMoneyAmount;}
    public float MoneyCalculationInterval { get => _moneyCalculationInterval;}

    public int DemolitionPrice => _demolitionPrice;

    // Start is called before the first frame update
    private void Start()
    {
        m_MoneyHelper = new MoneyHelper(_startMoneyAmount);
        m_PopulationHelper = new PopulationHelper();
        UpdateUI();
    }

    public void PrepareResourceManager(BuildingManager buildingManager)
    {
        this.m_BuildingManger = buildingManager;
        InvokeRepeating("CalculateTownIncome",0,MoneyCalculationInterval);
    }

    public bool SpendMoney(int amount)
    {
        if (CanIBuyIt(amount))
        {
            try
            {
                m_MoneyHelper.ReduceMoney(amount);
                UpdateUI();
                return true;
            }
            catch (MoneyException)
            {

                ReloadGame();
            }
        }
        return false;
    }

    private void ReloadGame()
    {
        Debug.Log("End the game");
    }

    public bool CanIBuyIt(int amount)
    {
        return m_MoneyHelper.Money >= amount;
    }

    public void CalculateTownIncome()
    {
        try
        {
            m_MoneyHelper.CalculateMoney(m_BuildingManger.GetAllStructures());
            UpdateUI();
        }
        catch (MoneyException)
        {
            ReloadGame();
        }
    }

    private void OnDisable()
    {
        CancelInvoke();  
    }

    public void AddMoney(int amount)
    {
        m_MoneyHelper.AddMoney(amount);
        UpdateUI();
    }

    private void UpdateUI()
    {
        _uiController.SetMoneyValue(m_MoneyHelper.Money);
        _uiController.SetPopulationValue(m_PopulationHelper.Population);
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public int HowManyStructuresCanIPlace(int placementCost, int numberOfStructures)
    {
        int amount = (int)(m_MoneyHelper.Money / placementCost);
        return amount > numberOfStructures ? numberOfStructures : amount;
    }

    public void AddToPopulation(int value)
    {
        m_PopulationHelper.AddToPopulation(value);
        UpdateUI();
    }

    public void ReducePopulation(int value)
    {
        m_PopulationHelper.ReducePopulation(value);
        UpdateUI();

    }
}
