using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private GameObject[] weaponList;
    public GameObject GetWeappon(WeaponType weaponType)
    {
        return weaponList[(int)weaponType];
    }
}

public enum WeaponType
{
    arrow = 0,
    axe1 = 1,
    axe2 = 2,
    boomerang1 = 3,
    boomerang2 = 4,
    candy0 = 5, 
    candy1 = 6,
    candy2 = 7,
    candy4 = 8,
    hammer = 9,
    knife = 10,
    uzi = 11
}
