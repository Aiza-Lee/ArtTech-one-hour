using UnityEngine;

public class Player : MonoBehaviour {

	[Header("挂载")]
	[SerializeField] private Weapon _weapon;

	[Header("角色属性")]
	public float Speed = 5f;
	public int Damage = 1;
	public int MaxHealth = 5;
	public int CurrentHealth = 5;

	void Start() {
		_weapon.SetPlayer(this);
	}

	void Update() {
		UpdateMove();
		UpdateUseWeapon();
	}

	private void UpdateMove() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		var movement = new Vector2(moveHorizontal, moveVertical);

		if (movement.magnitude > 1) {
			movement.Normalize();
		}

		transform.position += Speed * Time.deltaTime * (Vector3) movement;
	}
	
	private void UpdateUseWeapon() {
		if (Input.GetKeyDown(KeyCode.J)) {
			_weapon.UseWeapon();
		}
	}
}
