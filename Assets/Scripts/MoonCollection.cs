using UnityEngine;

public class MoonCollection : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    [System.Obsolete]

    private void OnTriggerEnter2D(Collider2D collision) {
        ParticleSystem ps = collision.gameObject.GetComponent<ParticleSystem>();
        ps.Play();
        collision.gameObject.GetComponent<Collider2D>().enabled = false;
        collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(collision.gameObject, ps.duration);
        if (particles != null)
            particles.emissionRate += 1;


    }

    
}
