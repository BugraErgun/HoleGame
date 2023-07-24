using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float size;

    private void Awake()
    {
        size = transform.localScale.y;
    }
    private void Start()
    {
        GetComponent<Rigidbody>().sleepThreshold = 0;
    }
    public float GetSize()
    {
        return size;
    }
}
