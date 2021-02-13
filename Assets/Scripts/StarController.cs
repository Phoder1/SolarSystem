using UnityEngine;

public class StarController : MonoBehaviour
{
    [Range(0, 333)]
    [SerializeField] private float startMass;
    [SerializeField] private Vector2 startVelocity;
    [SerializeField] private bool updateGravity;
    [SerializeField] private GameObject arrowPivot;
    [SerializeField] private GameObject arrow;
    BoxCollider2D arrowCollider;
    private float massMultiplier;
    public float GetSetMassMultiplier {
        get => massMultiplier;
        set {
            massMultiplier = value;
            rb.mass = startMass * value;
        }
    }
    SpriteRenderer arrowSpriteRenderer;
    private bool showArrow;
    [HideInInspector]
    public Rigidbody2D rb;
    StarsManager starsManager;



    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = startMass;
        if(arrow != null)
        arrowCollider = arrow.GetComponent<BoxCollider2D>();
    }
    private void Start() {
        rb.velocity = startVelocity;
        starsManager = StarsManager._instance;
        starsManager.AddStar(this);
        starsManager.SetArrowEvent += SetArrow;
        if (updateGravity)
            starsManager.SetGravityEvent += SetGravity;
        if (arrow != null)
            arrowSpriteRenderer = arrow.GetComponent<SpriteRenderer>();
        GetSetMassMultiplier = 1;

    }
    private void FixedUpdate() {
        if (updateGravity)
            UpdateGravity();
        if (arrowPivot != null && arrow != null && showArrow) {
            arrowPivot.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, rb.velocity));
            arrowSpriteRenderer.transform.localScale = new Vector3( 1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
            arrowSpriteRenderer.size = new Vector2(Mathf.Max(rb.velocity.magnitude, 0.2f), 1);
            arrow.transform.localPosition = new Vector2(arrowSpriteRenderer.size.x * arrowSpriteRenderer.transform.localScale.x / 2, 0);
            arrowCollider.size = arrowSpriteRenderer.size;
        }
    }
    public void SetGravity(bool value) => updateGravity = value;
    public void SetArrow(bool value) {
        showArrow = value;
        if (arrow != null && arrowPivot != null) {
            arrowPivot.SetActive(value);
            arrow.SetActive(value);
        }
    }

    private void UpdateGravity() {
        //F = (Gravity const * mass1 * mass2) / distance * distance
        foreach (StarController star in starsManager.GetStars) {
            if (star != this) {
                float force = (StarsManager.gravityConst * rb.mass * star.rb.mass) / Mathf.Pow(Vector2.Distance(rb.position, star.rb.position), 2);
                Vector2 forceDirection = (star.rb.position - rb.position).normalized;
                Vector2 forceVector = forceDirection * force;
                rb.AddForce(forceVector);
            }

        }
    }
}
