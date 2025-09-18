using UnityEngine;

public class Weapon : MonoBehaviour {
	[Header("属性")]
	public float Cooldown = 0.5f;


	private float _lastUseTime = -9999f;
	private bool _onPlayer;
	private Player _bondedPlayer;

	public void SetPlayer(Player player) {
		_bondedPlayer = player;
		_onPlayer = true;
		transform.SetParent(player.transform);
		transform.localPosition = Vector3.zero;
	}

	public float CooldownPercent => Time.time - _lastUseTime >= Cooldown ? 1f : (Time.time - _lastUseTime) / Cooldown;

	public bool UseWeapon() {
		if (CooldownPercent < 1f) {
			return false;
		}
		Debug.Log("UseWeapon");
		_onPlayer = !_onPlayer;
		if (_onPlayer) {
			DamageLineGenerator.Inst.Spawn(_bondedPlayer.transform.position, transform.position);
			_bondedPlayer.transform.position = transform.position;
			transform.SetParent(_bondedPlayer.transform);
			transform.localPosition = Vector3.zero;
		} else {
			transform.SetParent(null);
		}
		_lastUseTime = Time.time;
		return true;
	}
}