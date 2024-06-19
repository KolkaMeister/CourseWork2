using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSession : MonoBehaviour
{
    [SerializeField] private int _earnedCoins;
    public event Action<int> OnCoinsChanged;

    private int totalEn;

    private int currentEnDead;
    private void Start()
    {
        SpawnHero();
        var h = FindObjectOfType<Hero>();
        totalEn= FindAnyObjectByType<Spawner>().TotalMons;
    }
    private void SpawnHero()
    {
        var h = Instantiate(DefsFacade.Instance.Heroes.GetByName(Account.HeroesData.Current).Prefab,Vector3.zero,Quaternion.identity).GetComponent<Hero>() ;
        Instantiate(DefsFacade.Instance.Weapons.GetByName(Account.WeaponsData.Current).Prefab, h.Hand.transform);

    }
    public void AddCoins(int coins)
    {
        _earnedCoins += coins;
        OnCoinsChanged?.Invoke(_earnedCoins);
        currentEnDead++;
        if (currentEnDead >= totalEn)
            LevelEnded();
    }
    public void LevelEnded()
    {
        Account.EarnCoins(_earnedCoins);
        SceneManager.LoadScene(1);
    }
    public void PlayerDeath()
    {
        LevelEnded();
    }
}
