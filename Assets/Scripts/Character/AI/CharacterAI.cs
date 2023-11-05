using Scripts.Character.Classes;
using UnityEngine;

[RequireComponent(typeof(Scripts.Character.Classes.Person))]
public class CharacterAI : MonoBehaviour
{
    protected Scripts.Character.Classes.Person Person;

    protected virtual void Awake() => Person = GetComponent<Scripts.Character.Classes.Person>();

    protected virtual void Update()
    {

    }

    protected virtual void Moving()
    {

    }
}