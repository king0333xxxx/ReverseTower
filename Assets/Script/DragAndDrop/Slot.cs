using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject currentItem; //item currenley held
    public bool hasItem => currentItem != null; // Auto-update hasItem berdasarkan currentItem

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

internal class serializefieldAttribute : Attribute
{
}