using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int numberOfMoons;
    [SerializeField] float sunMinDistance;
    [SerializeField] float sunMaxDistance;
    [SerializeField] GameObject moonPrefab;

    private void Start() {
        for (int i = 0; i < numberOfMoons; i++) {
            Vector3 moonPos;
            do
                moonPos = new Vector3(Random.Range(-sunMaxDistance,sunMaxDistance), Random.Range(-sunMaxDistance, sunMaxDistance));
            while (!PositionValid(moonPos));
            Instantiate(moonPrefab, moonPos, Quaternion.identity);
        }
    }
    private bool PositionValid(Vector3 moonPos)
        => Vector2.Distance(moonPos, transform.position) <= sunMaxDistance
        && Vector2.Distance(moonPos, transform.position) >= sunMinDistance;

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, sunMinDistance);
        Gizmos.DrawWireSphere(transform.position, sunMaxDistance);
    }
}
