using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuCoinsUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    void Start()
    {
        Account.OnCoinsChanged += UpdateView;
        UpdateView();
    }

    public void UpdateView()
    {
        _text.text= Account.Coins.ToString();
    }
}
