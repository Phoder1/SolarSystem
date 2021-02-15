using System;
using UnityEngine;

public class MoonCollection : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private ParticleSystem particlesTrail;

    public event Action MoonCollectedEvent;

    [System.Obsolete]

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleSystem ps = collision.gameObject.GetComponent<ParticleSystem>();
        ps.Play();
        MoonCollectedEvent?.Invoke();
        collision.gameObject.GetComponent<Collider2D>().enabled = false;
        collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(collision.gameObject, ps.duration);
        if (particles != null)
            particles.emissionRate += 1;
            particlesTrail.emissionRate += 1;
    }


}
