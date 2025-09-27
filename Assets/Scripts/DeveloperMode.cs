using UnityEngine;

public class DeveloperMode : MonoBehaviour {
	private readonly KeyCode[] _toggleKey = new KeyCode[] {
		KeyCode.W, KeyCode.W, KeyCode.S, KeyCode.S,
		KeyCode.A, KeyCode.D, KeyCode.A, KeyCode.D,
	};

	[SerializeField] private Player _player;
	[SerializeField] private Laser _damageLinePrefab;

	private int _currentIndex = 0;
	private float _lastKeyTime = 0f;
	private const float MAX_INTERVAL = 0.6f;
	private bool _isDevMode = false;

	void Update() {
		if (_isDevMode) {
			if (Input.GetKeyDown(KeyCode.H)) {
				StatusBar.Inst.UpdateHealthText(1f);
				_player.CurrentHealth = _player.MaxHealth;
			}
			return;
		}
		if (Input.GetKeyDown(_toggleKey[_currentIndex])) {
			if (Time.time - _lastKeyTime <= MAX_INTERVAL) {
				_currentIndex++;
				if (_currentIndex >= _toggleKey.Length) {
					// Toggle developer mode
					_isDevMode = true;
					Debug.Log("Developer mode activated");
					StatusBar.Inst.UpdateHealthText(1f);
					_player.MaxHealth = _player.CurrentHealth = 500;
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

	void OnDestroy() {
		if (!_isDevMode) return;
		_damageLinePrefab.LineWidth /= 10;
	}
}