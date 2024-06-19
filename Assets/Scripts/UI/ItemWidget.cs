using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemWidget : MonoBehaviour
{
    [SerializeField] private Image _img;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Button _button;
    private PlayerDataStruct _playerData;
    private ItemDefData _itemDefData;
    private int _id;

    private bool IsSelected=>_playerData.IsSelected(_name.text);
    private bool IsInList => _playerData.IsInList(_name.text);

      WidgetState buyState;
      WidgetState selectState;
      WidgetState selectedState;

    public void Set(ItemDefData data,PlayerDataStruct playerData)
    {
        SetStates();
        _itemDefData =data;
        _img.sprite = data.Icon;
        _name.text=data.Name;
        _price.text = data.Price.ToString();
        _id= data.Id;
         _playerData = playerData;
        playerData.OnChanged += UpdateView;
        UpdateView();
        
    }
    private void SetStates()
    {
        buyState = new WidgetState(Buy, Color.yellow);
        selectState = new WidgetState(Select, Color.green);
        selectedState = new WidgetState(null, Color.white);
    }

    public void UpdateView()
    {
        _button.interactable = true;
        if (IsSelected)
        {
            SetWidgetState(selectedState);
            _button.interactable = false;
        }
        else if(IsInList)
            SetWidgetState(selectState);
        else
            SetWidgetState(buyState);
    }
    public  void Buy()
    {
        Account.Buy(_itemDefData.Name,_itemDefData.Price);
    }
    public  void Select()
    {
        _playerData.SetSelected(_name.text);
    }
    private void SetWidgetState(WidgetState state)
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(state.Act);
        _button.image.color = state.Color;

    }
    private void OnDestroy()
    {
        _playerData.OnChanged -= UpdateView;
    }
}

public struct WidgetState
{
    public UnityAction Act;
    public Color Color;

    public WidgetState(UnityAction act,Color color )
    {
        Act=act;
        Color=color;
    }
}

