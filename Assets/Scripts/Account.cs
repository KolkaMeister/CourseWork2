using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Account
{
    private static string _login;
    private static string _password;


    private static int _coins;

    public static event Action OnCoinsChanged;
    public static int Coins => _coins;

    private static PlayerDataStruct _heroesData= new PlayerDataStruct("Mike");

    private static PlayerDataStruct _weaponsData = new PlayerDataStruct("Pistol");

    public static PlayerDataStruct HeroesData => _heroesData;

    public static PlayerDataStruct WeaponsData => _weaponsData;

    public static bool SignIn(string login, string password)
    {
        var b = DBManager.Verify(login, password);
        if (b)
        {
            _login = login;
            _password = password;
            GetData();
        }
            return b;
    }

    private static void GetData()
    {
       var data= DBManager.GetData(_login);
        _heroesData.SetList(data.Heroes);
        _weaponsData.SetList(data.Weapons);
        _coins = data.Balance;
        OnCoinsChanged?.Invoke();
    }



    public static void EarnCoins(int count)
    {
        DBManager.AddCoins(_login, count);
        GetData();
    }
    public static void Buy(string name,int price)
    {
        if (price > _coins) return;
        DBManager.Buy(name, _login);
        GetData();
    }
}
