using Scripts.Character.Classes;
using UnityEngine;

[RequireComponent(typeof(Person))]
public class CharacterAI : MonoBehaviour
{
    protected Person Person;

    protected virtual void Awake() => Person = GetComponent<Person>();

    protected virtual void Update()
    {

    }

    protected virtual void Moving()
    {

    }
}