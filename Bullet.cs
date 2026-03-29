using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 25f;
    public float lifetime = 3f;

    private Light bulletLight;
    private TrailRenderer trail;
    private float baseIntensity;

    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;

        bulletLight = GetComponentInChildren<Light>();
        trail = GetComponent<TrailRenderer>();

        if (bulletLight != null)
            baseIntensity = bulletLight.intensity;

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (bulletLight != null)
            bulletLight.intensity = baseIntensity + Mathf.Sin(Time.time * 30f) * 0.5f;
    }

    void OnDestroy()
    {
        if (trail != null)
        {
            trail.transform.SetParent(null);
            trail.autodestruct = true;
        }
    }
}