using System;
using UnityEngine;

[Serializable]
public struct NewAnimal
{
    public string name;
    public int difficulty;
    public int speed;
    public int score;
}
public class Animal : MonoBehaviour
{
    public NewAnimal animal;
}
