using UnityEngine;

public class StatusBar : MonoBehaviour {
	public static StatusBar Inst;
	void Awake() {
		if (Inst != null && Inst != this) { Destroy(gameObject); }
		Inst = this;
	}

	public RectTransform HealthBarFill;
	public RectTransform WeaponChargeFill;


	public void UpdateHealthText(Player player) {
		float fillAmount = (float) player.CurrentHealth / player.MaxHealth;
		HealthBarFill.localScale = new Vector3(fillAmount, 1, 1);
	}

	public void UpdateWeaponCharge(float percent) {
		WeaponChargeFill.localScale = new Vector3(percent, 1, 1);
	}
}