using UnityEngine;

public sealed class DamageLineGenerator : MonoBehaviour {

	public static DamageLineGenerator Inst { get; private set; } = null;

	[SerializeField] private DamageLine _damageLinePrefab;

	private void Awake() {
		if (Inst != null && Inst != this) { Destroy(Inst.gameObject); }
		Inst = this;
	}

	public void Spawn(Vector3 start, Vector3 end) {
		var damageLine = Instantiate(_damageLinePrefab);
		damageLine.SpawnAt(start, end);
	}
}
