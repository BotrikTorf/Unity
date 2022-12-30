using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInitial : MonoBehaviour
{
    [SerializeField] private Material _material;
    void Start()
    {
        _material.color = Color.green;
    }
}
