using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleTrigger : MonoBehaviour {
	private ParticleSystem _particleSystem;

	public float Duration => _particleSystem.main.duration / _particleSystem.main.simulationSpeed;

	void Awake() {
		_particleSystem = GetComponent<ParticleSystem>();
	}

	public void Play(Vector3 direction) {
		transform.up = direction.normalized;
		_particleSystem.Play();
	}
}