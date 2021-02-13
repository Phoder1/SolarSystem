using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsManager : MonoBehaviour
{
    public static StarsManager _instance;
    [HideInInspector]
    public List<StarController> stars;
    public const float gravityConst = 9.8f;

    public event Action<bool> SetGravityEvent;
    public event Action<bool> SetArrowEvent;
    private void Awake() {
        if(_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
        stars = new List<StarController>();
    }

    public void SetSimulationSpeed(float value) => Time.timeScale = value;
    public void SetGravity(bool value) => SetGravityEvent?.Invoke(value);
    public void SetArrow(bool value) => SetArrowEvent?.Invoke(value);
}
