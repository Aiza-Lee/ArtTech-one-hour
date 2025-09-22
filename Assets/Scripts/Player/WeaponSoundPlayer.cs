using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponSoundPlayer : MonoBehaviour {
	public AudioClip ShootClip;

	private AudioSource _audioSource;
	void Awake() {
		_audioSource = GetComponent<AudioSource>();
	}
	public void PlayShootSound() {
		_audioSource.PlayOneShot(ShootClip);
	}
}