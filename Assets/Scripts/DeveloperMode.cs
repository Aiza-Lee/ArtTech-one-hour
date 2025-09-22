using UnityEngine;

public class DeveloperMode : MonoBehaviour {
	private readonly KeyCode[] _toggleKey = new KeyCode[] {
		KeyCode.W, KeyCode.W, KeyCode.S, KeyCode.S,
		KeyCode.A, KeyCode.D, KeyCode.A, KeyCode.D,
	};

	[SerializeField] private Player _player;
	[SerializeField] private DamageLine _damageLinePrefab;

	private int _currentIndex = 0;
	private float _lastKeyTime = 0f;
	private const float MaxInterval = 0.6f;
	private bool _isDevMode = false;

	void Update() {
		if (_isDevMode) {
			if (Input.GetKeyDown(KeyCode.H)) {
				_player.CurrentHealth = _player.MaxHealth;
			}
			return;
		}
		if (Input.GetKeyDown(_toggleKey[_currentIndex])) {
			if (Time.time - _lastKeyTime <= MaxInterval) {
				_currentIndex++;
				if (_currentIndex >= _toggleKey.Length) {
					// Toggle developer mode
					_isDevMode = true;
					Debug.Log("Developer mode activated");
					_player.MaxHealth = _player.CurrentHealth = 100;
					_damageLinePrefab.LineWidth *= 10;
					_currentIndex = 0;
				}
			} else {
				_currentIndex = 1;
			}
			_lastKeyTime = Time.time;
		} else if (Input.anyKeyDown) {
			_currentIndex = 0;
		}
	}
}