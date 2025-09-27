using UnityEngine;

public class StatusBar : MonoBehaviour {
	public static StatusBar Inst;
	void Awake() {
		if (Inst != null && Inst != this) { Destroy(gameObject); return; }
		Inst = this;
	}

	public RectTransform HealthBarFill;
	public RectTransform WeaponChargeFill;


	public void UpdateHealthText(float percent) {
		HealthBarFill.localScale = new Vector3(percent, 1, 1);
	}

	public void UpdateWeaponCharge(float percent) {
		WeaponChargeFill.localScale = new Vector3(percent, 1, 1);
	}
}