using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sensors provide the agent program with information about the environment.
/// </summary>
public class Sensor : MonoBehaviour
{

    protected Agent agent
    {
        get;
        private set;
    }

    protected virtual void Awake()
    {
        agent = GetComponent<Agent>();
    }
}
