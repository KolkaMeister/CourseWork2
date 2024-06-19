using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npgsql;
using System.Data;
using Unity.VisualScripting;

public static class DBManager
{
    private static string _conStr = "Host=localhost;Username=postgres;password=12345;Database=game";

    private static NpgsqlConnection conn = new NpgsqlConnection(_conStr);

    static DBManager()
    {
           
    }
    public static ReturnPlayerData GetData(string login)
    {
        if (conn.State!=ConnectionState.Open) {conn.Open();}
        var BalCom  = new NpgsqlCommand($"SELECT balance FROM player where login='{login}'", conn);
        var balRead= BalCom.ExecuteReader();
        balRead.Read();
        var bal = balRead.GetInt32(0);
        balRead.Close();

        var HeroCom = new NpgsqlCommand($"SELECT name FROM product JOIN purchases ON product.id=purchases.pr_id JOIN player ON purchases.pl_id=player.id WHERE player.login='{login}' AND typeid='1'", conn);
        var HeroRead = HeroCom.ExecuteReader();
        var Heroes = new List<string>();
        while(HeroRead.Read()){
        Heroes.Add(HeroRead.GetString(0));
        }
        HeroRead.Close();

        var WeaponCom = new NpgsqlCommand($"SELECT name FROM product JOIN purchases ON product.id=purchases.pr_id JOIN player ON purchases.pl_id=player.id WHERE player.login='{login}' AND typeid='2'", conn);
        var WeaponRead = WeaponCom.ExecuteReader();
        var Weapons = new List<string>();
        while (WeaponRead.Read())
        {
            Weapons.Add(WeaponRead.GetString(0));
        }
        WeaponRead.Close();
        Debug.Log(Heroes);
        return new ReturnPlayerData(bal,Heroes.ToArray(),Weapons.ToArray());
    }
    public static bool Verify(string login, string password)
    {
        if (conn.State != ConnectionState.Open) { conn.Open(); }

        var CheckLogin = new NpgsqlCommand($"SELECT EXISTS(select id from player where login='{login}')",conn);
        var reader = CheckLogin.ExecuteReader();
        reader.Read();
        var isExists = reader.GetBoolean(0);
        reader.Close();
        if (isExists) 
        {
            var CheckPass = new NpgsqlCommand($"SELECT EXISTS(select id from player where login='{login}' and password='{password}')", conn);
            var reader2 = CheckPass.ExecuteReader();
            reader2.Read();
            var isLogged=reader2.GetBoolean(0);
            reader2.Close();
            return isLogged;
        }
        else 
        {
            var registerNew = new NpgsqlCommand($"INSERT INTO player(login,password,balance) VALUES('{login}','{password}',0)",conn);
            var reader2 = registerNew.ExecuteNonQuery();
            return true;
        }
    }
    public static void Buy(string _productName, string _playerLogin)
    {
        if (conn.State != ConnectionState.Open){conn.Open();}
        var com = new NpgsqlCommand($"INSERT INTO purchases(pl_id,pr_id) VALUES((select id FROM player where login='{_playerLogin}' ),(select id FROM product where name='{_productName}'))", conn);
        com.ExecuteNonQuery();
        var com2 = new NpgsqlCommand($"UPDATE player SET balance=balance-(SELECT price FROM product where name='{_productName}') WHERE login='{_playerLogin}' ", conn);
        com2.ExecuteNonQuery();
    }

    public static void AddCoins(string _playerLogin,int coins)
    {
        if (conn.State != ConnectionState.Open) { conn.Open(); }
        var com2 = new NpgsqlCommand($"UPDATE player SET balance=balance+{coins} WHERE login='{_playerLogin}'", conn);
        com2.ExecuteNonQuery();
    }
    public struct ReturnPlayerData
    {
        public int Balance;
        public string[] Heroes;
        public string[] Weapons;

        public ReturnPlayerData(int balance, string[] heroes, string[] weapons)
        {
            Balance = balance;
            Heroes = heroes;
            Weapons = weapons;

        }
    }
}
