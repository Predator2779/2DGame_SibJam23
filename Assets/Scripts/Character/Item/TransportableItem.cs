using System;
using UnityEngine;

public class TransportableItem : MonoBehaviour
{
    private bool _isNotTaken = true;
    // private Rigidbody2D _rbody;
    //
    // private void Start() => _rbody = GetComponent<Rigidbody2D>();

    public bool IsNotTaken
    {
        get => _isNotTaken;
        set => _isNotTaken = value;
    }

    public void PickUp(Transform parent)
    {
        if (IsNotTaken)
        {
            // _rbody.simulated = false;
            transform.SetParent(parent.transform);

            IsNotTaken = false;
        }
    }

    public void Put()
    {
        if (!IsNotTaken)
        {
            transform.SetParent(null);
            // _rbody.simulated = true;
            
            IsNotTaken = true;
        }
    }
}