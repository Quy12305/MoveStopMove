using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PantData", menuName = "ScriptableObjects/PantData", order = 2)]

public class PantData : ScriptableObject
{
    [SerializeField] private Material[] pants;

    public Material GetPant(PantType pantType)
    {
        return pants[(int) pantType];
    }
}

public enum PantType
{
    batman = 0,
    chambi = 1,
    comy = 2,
    dabao = 3,
    onion = 4,
    rainbow = 5,
    skull = 6,
    vantim = 7
}
