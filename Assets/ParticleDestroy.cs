using UnityEngine;
using System.Collections;

public class ParticleDestroy : MonoBehaviour {

    ParticleSystem ps;
    ParticleSystem.Particle[] particles;
    [SerializeField]
    float deathSpeed;
    [SerializeField]
    float distanceThreshold;
    [HideInInspector]
    int amount;
    // Use this for initialization
    void Start () {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.maxParticles];
	}
	
	// Update is called once per frame
	void Update () {
        amount = ps.GetParticles(particles);

        for (int i = 0; i < amount; i++)
            if (Vector3.Distance(particles[i].position, transform.position) > distanceThreshold)
            {
                particles[i].remainingLifetime -= deathSpeed;
   //          particles[i].
            }

        ps.SetParticles(particles, amount);
	}
}
