using UnityEngine;

public class StatusBar : MonoBehaviour {
	public static StatusBar Inst;

	[SerializeField] private RectTransform _healthBarFill;
	[SerializeField] private RectTransform _weaponChargeFill;

	void Awake() {
		if (Inst != null && Inst != this) { Destroy(gameObject); }
		Inst = this;
	}

	public void UpdateHealthText(Player player) {
		float fillAmount = (float) player.CurrentHealth / player.MaxHealth;
		_healthBarFill.localScale = new Vector3(fillAmount, 1, 1);
	}

	public void UpdateWeaponCharge(float chargePercent) {
		_weaponChargeFill.localScale = new Vector3(chargePercent, 1, 1);
	}
}