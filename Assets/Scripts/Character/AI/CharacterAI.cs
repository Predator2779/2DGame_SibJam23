using Scripts.Character.Classes;
using UnityEngine;

[RequireComponent(typeof(Scripts.Character.Classes.Character))]
public class CharacterAI : MonoBehaviour
{
    protected Scripts.Character.Classes.Character _character;

    protected virtual void Awake() => _character = GetComponent<Scripts.Character.Classes.Character>();

    protected virtual void Update()
    {

    }

    protected virtual void Moving()
    {

    }
}