using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class HurtParticlePalyer : MonoBehaviour {
	private ParticleSystem _particle;

	public float Duration => _particle.main.duration;

	void Awake() {
		_particle = GetComponent<ParticleSystem>();
	}

	public void Play(Vector3 hitPoint) {
		transform.up = (transform.position - hitPoint).normalized;
		_particle.Play();
	}
}