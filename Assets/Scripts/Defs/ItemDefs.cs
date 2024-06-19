using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemDefs",menuName ="Defs/ItemDefs")]
public class ItemDefs : ScriptableObject
{
    [SerializeField] public ItemDefData[] itemDefs;

    public ItemDefData[] GetAll()
    {
        return itemDefs;
    } 
    public ItemDefData GetById(int id)
    {
        foreach (ItemDefData itemDef in itemDefs)
            if (itemDef.Id==id)
                return itemDef;
        return default;
    }
    public  ItemDefData GetByName(string name)
    {
        foreach (ItemDefData itemDef in itemDefs)
            if (itemDef.Name.Equals(name))
                return itemDef;
        return default;
    }
    public ItemDefData[] GetByType(ProductType type)
    {
        List<ItemDefData> list = new List<ItemDefData>();
        foreach (var item in itemDefs)
            if(item.Type==type)list.Add(item);
        return list.ToArray();
    }
}
[Serializable]
public struct ItemDefData
{
    [SerializeField] private ProductType _type;
    [SerializeField] private int _id;
    [SerializeField] private string _name;

    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;

    public ProductType Type=>_type;
    public int Id => _id;
    public string Name => _name;

    public int Price => _price;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;

}
[Serializable]
public enum ProductType
{
    Hero,
    Weapon
}
