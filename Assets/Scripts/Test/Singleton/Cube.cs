using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoSingleton<Cube>, IMonoSingleton
{
    private void Awake()
    {
        Debug.LogError("Awark");
    }

    private void Start()
    {
        Debug.LogError("Start");
    }

    public void SingletonInit()
    {

        Debug.LogError("Init");
    }

    private void OnEnable()
    {

        Debug.LogError("On Enable");
    }

    private void OnDisable()
    {
        Debug.LogError("OnDisable");
    }

    private void OnDestroy()
    {
        Debug.LogError("OnDestroy");
    }
}
