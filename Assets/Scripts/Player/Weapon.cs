using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour {
	[Header("属性")]
	public float Cooldown = 0.5f;
	public float ChargingPercent => Mathf.Clamp01((Time.time - _lastUseTime) / Cooldown);


	private float _lastUseTime = -9999f;
	private bool _onPlayer;
	private Player _bondedPlayer;
	private Color _originalColor;

	private Rigidbody2D _rb2D;
	private SpriteRenderer _spriteRenderer;
	private AudioSource _soundPlayer;
	private void Awake() {
		_rb2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_originalColor = _spriteRenderer.color;
		_soundPlayer = GetComponent<AudioSource>();
	}

	void Start() {
		_rb2D.AddTorque(_rb2D.mass / 6, ForceMode2D.Impulse);
	}

	public void SetPlayer(Player player) {
		_bondedPlayer = player;
		_onPlayer = true;
	}

	void Update() {
		StatusBar.Inst.UpdateWeaponCharge(ChargingPercent);
		_spriteRenderer.color = Color.Lerp(
			_originalColor * new Color(0.05f, 0.05f, 0.05f), _originalColor, ChargingPercent
		);
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
			_lastUseTime = Time.time;

			CameraShaker.Inst.Shake(0.4f, 0.4f);
			_soundPlayer.Play();
			DamageLineGenerator.Inst.Spawn(_bondedPlayer.transform.position, transform.position);
			StartCoroutine(DoTransition());
		} else {
			if (ChargingPercent < 1f) {
				return false;
			}
			_onPlayer = false;
		}
		return true;
	}

	private IEnumerator DoTransition() {
		// 等待几帧，保证武器效果生效后再转移玩家位置
		yield return null;
		yield return null;
		yield return null;
		yield return null;
		_bondedPlayer.GetComponent<Rigidbody2D>().position = transform.position;
	}
}