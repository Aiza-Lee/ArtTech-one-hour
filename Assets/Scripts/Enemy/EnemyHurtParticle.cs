using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EnemyHurtParticle : MonoBehaviour {
	private ParticleSystem _particleSystem;

	public float Duration => _particleSystem.main.duration;

	void Awake() {
		_particleSystem = GetComponent<ParticleSystem>();
	}

	public void Play(Vector3 direction) {
		transform.up = direction;
		_particleSystem.Play();
	}
}