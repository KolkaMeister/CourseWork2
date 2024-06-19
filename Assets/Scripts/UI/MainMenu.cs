using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform _heroesContainer;
    [SerializeField] private Transform _weaponsContainer;
    [SerializeField] private ItemWidget _itemWidget;
    [SerializeField] private Button _playBut;

    private DataGroup _heroesGroup ;
    private DataGroup _weaponsGroup;
    void Start()
    {
        Init();
        
    }

    private void Init()
    {
        _heroesGroup = new DataGroup(_heroesContainer, _itemWidget,Account.HeroesData);
        Debug.Log(DefsFacade.Instance);
        _heroesGroup.SetData(DefsFacade.Instance.Heroes.GetAll());
        _weaponsGroup = new DataGroup(_weaponsContainer, _itemWidget, Account.WeaponsData);
        _weaponsGroup.SetData(DefsFacade.Instance.Weapons.GetAll());
        _playBut.onClick.AddListener(PlayBut);
    }

    public void PlayBut()
    {
        SceneManager.LoadScene(2);
    }
}
