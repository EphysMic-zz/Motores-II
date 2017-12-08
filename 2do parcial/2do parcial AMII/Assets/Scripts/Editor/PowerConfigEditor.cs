using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(PowerConfigEditor))]
public class PowerConfigEditor : Editor
{
    private PowerConfig _target;
    private List<PowerConfig> data;
    

    private void OnEnable()
    {
    //    _target = (PowerConfig)target;
        if (data == null)
            data = new List<PowerConfig>();
    }

    public override void OnInspectorGUI()
    {
        //esssto todavia no se donde ponerlo
        #region Values
      /*  _target.nameOfPower = EditorGUILayout.TextField("Name: ", _target.nameOfPower);
        _target.damage = EditorGUILayout.FloatField("Damage: ", _target.damage);
        _target.criticalHit = EditorGUILayout.FloatField("Critial hit: ", _target.criticalHit);
        _target.coolDown = EditorGUILayout.FloatField("CoolDown: ", _target.coolDown);
        _target.costoDeUso = EditorGUILayout.FloatField("Costo de uso: ", _target.costoDeUso);
        _target.rangoDeAlcance = EditorGUILayout.FloatField("Rango de alcance ", _target.rangoDeAlcance);  */      
        #endregion
    }

    //essto para tener la cant de poderes
    public int countData
    {
        get { return data.Count; }
    }
    
    public int count
    {
        get { return data.Count; }
    }

    public void RemoveAt(int _in)
    {
        data.RemoveAt(_in);
    }

    public PowerConfig Pw(int index)
    {
        return data.ElementAt(index);
    }
    public void Add(PowerConfig pw)
    {
        data.Add(pw);
    }

    public void Orden()
    {
        //por lo que busque...es asi esto para ordenar
        data.Sort((x, y) => string.Compare(x.nameOfPower, y.nameOfPower));
    }
}
