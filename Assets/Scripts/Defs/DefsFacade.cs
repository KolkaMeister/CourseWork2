using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Defs/Facade", fileName ="Facade")]
public class DefsFacade : ScriptableObject
{

    private static DefsFacade _instance;
    public static DefsFacade Instance=> _instance!=null?_instance:_instance=Load();

    [SerializeField] private ItemDefs _weapons;

    [SerializeField] private ItemDefs _heroes;
    public ItemDefs Weapons => _weapons;

    public ItemDefs Heroes => _heroes;

    private static DefsFacade Load()
    {
        return Resources.Load<DefsFacade>("Facade");
    }
}
