using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _health = 3;

    [Range(100, 200)]
    public int MaxHealth = 3;

    // Validation at run time
    public int Health { get => _health; set => _health = Mathf.Clamp(value, 0, MaxHealth); }

    // Apply validation rules at editor time. Called when script is loaded or a value is changed in the inspector.
    // Use to ensure that when data is modified in the editor, that the data stays within a certain range.
    // Gates the health value between 0 and MaxHealth;
    private void OnValidate() => Health = _health;

    //// Accessor method. A function that will return health.
    //public int GetHealth() => _health;

    //// Mutator method
    //// Could possibly be an abstract method in a parent class
    //public void SetHealth(int newHealth)
    //{
    //    _health = Mathf.Clamp(newHealth, 0, MaxHealth);
    //}
}
