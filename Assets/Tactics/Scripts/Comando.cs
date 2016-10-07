using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Comando", menuName = "TPG2016Tactics/Novo Comando")]
public class Comando : ScriptableObject
{
    public string nome;
    public int alcance;
    public int dano;
    public float chanceDeAcerto;
}

