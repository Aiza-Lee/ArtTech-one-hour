using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Weapon : MonoBehaviour {
	[Header("属性")]
	public float Cooldown = 0.5f;
	public float ChargingPercent => Mathf.Clamp01((Time.time - _lastUseTime) / Cooldown);

	[SerializeField] private WeaponSoundPlayer _soundPlayer;

	private float _lastUseTime = -9999f;
	private bool _onPlayer;
	private Player _bondedPlayer;

	private Collider2D _collider2D;
	private Rigidbody2D _rb2D;
	private SpriteRenderer _spriteRenderer;

	private Color _originalColor;

	private void Awake() {
		_collider2D = GetComponent<Collider2D>();
		_collider2D.enabled = false;
		_rb2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_originalColor = _spriteRenderer.color;
	}

	void Start() {
		_rb2D.AddTorque(_rb2D.mass / 2, ForceMode2D.Impulse);
	}

	public void SetPlayer(Player player) {
		_bondedPlayer = player;
		_onPlayer = true;
	}

	void Update() {
		StatusBar.Inst.UpdateWeaponCharge(ChargingPercent);
		if (Mathf.Abs(Time.time - _lastUseTime - Cooldown) < 0.01f) {
			CameraShaker.Inst.Shake(0.2f, 0.2f);
		}
		_spriteRenderer.color = Color.Lerp(_originalColor * new Color(0.05f, 0.05f, 0.05f), _originalColor, ChargingPercent);
	}

	void FixedUpdate() {
		if (_onPlayer) {
			var vec = _bondedPlayer.transform.position - transform.position;
			_rb2D.AddForce(_rb2D.mass * 25f * vec);
		}
	}

	public bool UseWeapon() {
		if (!_onPlayer) {
			_onPlayer = true;
			_collider2D.enabled = false;
			_lastUseTime = Time.time;

			CameraShaker.Inst.Shake(0.4f, 0.4f);
			_soundPlayer.PlayShootSound();
			DamageLineGenerator.Inst.Spawn(_bondedPlayer.transform.position, transform.position);
			_bondedPlayer.GetComponent<Rigidbody2D>().position = transform.position;
		} else {
			if (ChargingPercent < 1f) {
				return false;
			}
			_onPlayer = false;
			_collider2D.enabled = true;
		}
		return true;
	}
}