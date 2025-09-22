using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleTrigger : MonoBehaviour {

	public float Duration => _particleSystem.main.duration / _particleSystem.main.simulationSpeed;

	private ParticleSystem _particleSystem;
	void Awake() {
		_particleSystem = GetComponent<ParticleSystem>();
	}

	public void Play(Vector3 damageSource, Vector3 hitPoint) {
		var direction = hitPoint - damageSource;
		transform.position = hitPoint;
		transform.up = direction.normalized;
		_particleSystem.Play();
	}
}