using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarsManager : MonoBehaviour
{
    public static StarsManager _instance;
    [SerializeField] private StarController defaultCenterStar;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Slider massSlider;
    [SerializeField] private TextMeshProUGUI massText;
    [HideInInspector]
    private List<StarController> stars;
    private bool showMass = false;
    public List<StarController> GetStars => stars;
    public const float gravityConst = 9.8f;

    public event Action<bool> SetGravityEvent;
    public event Action<bool> SetArrowEvent;
    Camera mainCamera;

    private void Awake() {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        stars = new List<StarController>();
        stars.Add(defaultCenterStar);
        mainCamera = Camera.main;
    }
    private void Start() {
        foreach (StarController star in stars) {
            dropdown.options.Add(new TMP_Dropdown.OptionData(star.gameObject.name));
        }
        dropdown.value = 0;
        SetStar(0);
        dropdown.RefreshShownValue();
        massSlider.value = stars[dropdown.value].GetSetMassMultiplier;
    }
    private void Update() {
        if (showMass) {
            massText.transform.position = mainCamera.WorldToScreenPoint(stars[dropdown.value].transform.position);
            massText.text = stars[dropdown.value].rb.mass.ToString();
        }
    }
    public void SetSimulationSpeed(float value) => Time.timeScale = value;
    public void SetGravity(bool value) => SetGravityEvent?.Invoke(value);
    public void SetArrow(bool value) => SetArrowEvent?.Invoke(value);
    public void SetZoom(float value) => Camera.main.orthographicSize = value;
    public void SetMass(float value) => stars[dropdown.value].GetSetMassMultiplier = value;
    public void ShowMass(bool value) {
        showMass = value;
        massText.gameObject.SetActive(value);
    }
    public void SetStar(int starIndex) {
        Camera cam = Camera.main;
        cam.transform.parent = stars[starIndex].transform;
        cam.transform.localPosition = new Vector3(0, 0, cam.transform.localPosition.z);
        massSlider.value = stars[dropdown.value].GetSetMassMultiplier;
    }
    public void AddStar(StarController star) {
        if (star != defaultCenterStar)
            stars.Add(star);
    }
}
