using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int _startMoneyAmount;
    [SerializeField] private float _moneyCalculationInterval;
    [SerializeField] private BuildingManager _buildingManager;
    [SerializeField] private UiController _uiController;
    
    private MoneyHelper m_MoneyHelper;


    private void Awake()
    { 
        m_MoneyHelper = new MoneyHelper(_startMoneyAmount);
    }

    private void Start()
    {
        UpdateMoneyValueUI();
    }


    private bool CanBuy(int amount)
    {
        return m_MoneyHelper.GetMoneyAmount() >= amount;
    }
    
    private void ReloadGame()
    {
        Debug.Log("End of the game. Reload the game.");
    }
    
    private void UpdateMoneyValueUI()
    {
        _uiController.SetMoneyValue(m_MoneyHelper.GetMoneyAmount());
    }


    public void AddMoney(int amount)
    {
        m_MoneyHelper.AddMoneyAmount(amount);
        UpdateMoneyValueUI();
    }

    public void CalculateTownIncome()
    {
        try
        {
            m_MoneyHelper.CalculateMoney(_buildingManager.GetAllStructures());
            UpdateMoneyValueUI();
        }
        catch (MoneyException)
        {
            ReloadGame();
            throw;
        }
    }
    
    public bool SpendMoney(int amount)
    {
        if (CanBuy(amount))
        {
            try
            {
                m_MoneyHelper.ReduceMoneyAmount(amount);
                UpdateMoneyValueUI();
                
                return true;
            }
            catch (MoneyException)
            {
                ReloadGame();
                throw;
            }
        }

        return false;
    }
}