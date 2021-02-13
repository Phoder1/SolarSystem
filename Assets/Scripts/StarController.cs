using UnityEngine;

public class StarController : MonoBehaviour
{
    [Range(0, 333)]
    [SerializeField] private float mass;
    [SerializeField] private Vector2 startVelocity;
    [SerializeField] private bool updateGravity;
    [SerializeField] private GameObject arrowPivot;
    [SerializeField] private GameObject arrow;
    private bool showArrow;
    Rigidbody2D rb;
    StarsManager starsManager;



    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = mass;
    }
    private void Start() {
        rb.velocity = startVelocity;
        starsManager = StarsManager._instance;
        starsManager.stars.Add(this);
        if (updateGravity)
            starsManager.SetGravityEvent += SetGravity;

    }
    private void FixedUpdate() {
        if (updateGravity)
            UpdateGravity();
    }
    public void SetGravity(bool value) => updateGravity = value;
    public void SetArrow(bool value) => showArrow = value;

    private void UpdateGravity() {
        //F = (Gravity const * mass1 * mass2) / distance * distance
        foreach (StarController star in starsManager.stars) {
            if (star != this) {
                float force = (StarsManager.gravityConst * this.mass * star.mass) / Mathf.Pow(Vector2.Distance(rb.position, star.rb.position), 2);
                Vector2 forceDirection = (star.rb.position - rb.position).normalized;
                Vector2 forceVector = forceDirection * force;
                rb.AddForce(forceVector);
            }

        }
    }
}
