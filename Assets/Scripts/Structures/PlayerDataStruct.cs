using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class PlayerDataStruct
{

    public string _default;
    private string _selected;
    public string Current => _selected;

    public event Action OnChanged;
    public List<string> list = new List<string>();

    public PlayerDataStruct(string defaultt)
    {
        _default = defaultt;
        _selected = defaultt;
    }
    public bool SetSelected(string name)
    {
        foreach (var item in list)
            if(item==name)
            {
                _selected = name;
                OnChanged?.Invoke();
                return true;
            }
        return false;
    }
    public bool IsInList(string name) 
    {
        if (list.Contains(name)) return true;
        return false;
    }
    public bool IsSelected(string name)
    {
        if (name==_selected) return true;
        return false;
    }
    public void SetList(string[] names)
    {
        list = new List<string>() { _default};
        foreach (var item in names)
            list.Add(item);
        OnChanged?.Invoke();
    }
}
