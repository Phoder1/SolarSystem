using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] int numberOfMoons;
    [SerializeField] float sunMinDistance;
    [SerializeField] float sunMaxDistance;
    [SerializeField] GameObject moonPrefab;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI winTimeText;
    [SerializeField] TextMeshProUGUI starsCounter;
    [SerializeField] MoonCollection moonCollection;
    [SerializeField] GameObject winScreenPanel;

    int starCount;

    private int GetSetStarCount {
        get => starCount;
        set {
            starsCounter.text = "Stars collected: " + (starCount = value);
            if (starCount == numberOfMoons)
                WinScreen();
        }
    }

    private void WinScreen() {
        winScreenPanel.SetActive(true);
        winTimeText.text = "Time: " + Time.time.ToString("0.0");
        SetTimescale(0.05f);
    }
    private void SetTimescale(float scale) {
        Time.timeScale = scale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void Start() {
        SetTimescale(1);
        for (int i = 0; i < numberOfMoons; i++) {
            Vector3 moonPos;
            do
                moonPos = new Vector3(Random.Range(-sunMaxDistance,sunMaxDistance), Random.Range(-sunMaxDistance, sunMaxDistance));
            while (!PositionValid(moonPos));
            Instantiate(moonPrefab, moonPos, Quaternion.identity);
        }
        moonCollection.MoonCollectedEvent += () => { GetSetStarCount++; };
    }

    private void Update() {
        timeText.text = "Time: " + Time.time.ToString("0.0");
    }
    private bool PositionValid(Vector3 moonPos)
        => Vector2.Distance(moonPos, transform.position) <= sunMaxDistance
        && Vector2.Distance(moonPos, transform.position) >= sunMinDistance;

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, sunMinDistance);
        Gizmos.DrawWireSphere(transform.position, sunMaxDistance);
    }
}
