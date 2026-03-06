using UnityEngine;

public class Firescript : MonoBehaviour
{
    [Header("Particle Settings")]
    [Tooltip("Sleep het Particle System uit de hiërarchie (het kind-object) hiernaartoe.")]
    [SerializeField] private ParticleSystem explosionParticles;

    [Tooltip("Optioneel: Activeer de effecten ook als het object alleen gedisactiveerd wordt.")]
    [SerializeField] private bool triggerOnDisable = false;

    void Start()
    {
    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        HandleFireEffect();
    }

    private void OnDisable()
    {
        if (triggerOnDisable)
        {
            HandleFireEffect();
        }
    }

    private void HandleFireEffect()
    {
        if (explosionParticles != null)
        {
            explosionParticles.transform.parent = null;

            explosionParticles.Play();

            float totalDuration = explosionParticles.main.duration + explosionParticles.main.startLifetime.constantMax;
            Destroy(explosionParticles.gameObject, totalDuration);
        }
    }
}