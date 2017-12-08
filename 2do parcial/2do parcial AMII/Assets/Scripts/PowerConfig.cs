using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerConfig : ScriptableObject
{
    public string nameOfPower;
    public float damage;
    public float criticalHit;
    public float coolDown;
    public float costoDeUso;
    public float rangoDeAlcance;
    public PowerConfig(string name, float _damage, float ch, float cd, float uso, float rango)
    {
        nameOfPower = name;
        damage = _damage;
        criticalHit = ch;
        coolDown = cd;
        costoDeUso = uso;
        rangoDeAlcance = rango;
    }
}
