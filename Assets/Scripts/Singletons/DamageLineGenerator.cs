using UnityEngine;

public sealed class DamageLineGenerator : MonoBehaviour {
	public static DamageLineGenerator Inst { get; private set; } = null;
	private void Awake() {
		if (Inst != null && Inst != this) { Destroy(Inst.gameObject); }
		Inst = this;
	}

	public DamageLine DamageLinePrefab;
	
	public void Spawn(Vector3 start, Vector3 end) {
		var damageLine = Instantiate(DamageLinePrefab);
		damageLine.SpawnAt(start, end);
	}
}
