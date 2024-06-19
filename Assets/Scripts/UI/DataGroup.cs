using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGroup
{
    private Transform _container;
    private ItemWidget _prefab;
    private PlayerDataStruct _playerData;
    private List<ItemWidget> _itemWidgets = new List<ItemWidget>();
    public DataGroup(Transform container, ItemWidget prefab,PlayerDataStruct playerData)
    {
        _container = container;
        _prefab = prefab;
        _playerData = playerData;
    }
    
    public void SetData(ItemDefData[] data)
    {
        for (int i = 0; _itemWidgets.Count < data.Length; i++)
        {
            _itemWidgets.Add(Object.Instantiate<ItemWidget>(_prefab,_container));
        }
        for (int i = 0; i < data.Length; i++)
        {
            _itemWidgets[i].Set(data[i],_playerData);
            _itemWidgets[i].gameObject.SetActive(true);
            
            
        }
        for (int i = data.Length; i < _itemWidgets.Count; i++)
        {
            _itemWidgets[i].gameObject.SetActive(false);
        }
    }
}
