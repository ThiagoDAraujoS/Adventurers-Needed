using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Grounded : MonoBehaviour {

    ParticleSystem system;
    ParticleSystem.Particle[] particles;
    public int layer = 0;
    public float maxDistance;
	void Start ()
    {
        system = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[system.maxParticles];
	}


    void LateUpdate()
    {
        int particlesAlive = system.GetParticles(particles);
        for (int i = 0; i < particlesAlive; i++)
            if (particles[i].remainingLifetime > particles[i].startLifetime - 0.1f)
                if (!Physics.Raycast(particles[i].position, Vector3.down, maxDistance, layer))
                    particles[i].remainingLifetime = 0.0f;
        system.SetParticles(particles, particlesAlive);
    }
}
