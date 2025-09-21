using UnityEngine;

public class WeaponSoundPlayer : MonoBehaviour {
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private AudioClip _shootClip;

	public void PlayShootSound() {
		_audioSource.PlayOneShot(_shootClip);
	}
}