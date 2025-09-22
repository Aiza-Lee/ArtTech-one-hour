using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
	[Header("挂载")]
	[SerializeField] private Weapon _weapon;
	[SerializeField] private ParticleTrigger _hurtParticle;

	[Header("角色属性")]
	public float Speed = 5f;
	public int Damage = 1;
	public int MaxHealth = 5;
	public int CurrentHealth = 5;

	private Rigidbody2D _rgb2D;
	void Awake() {
		_rgb2D = GetComponent<Rigidbody2D>();
	}

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

		_rgb2D.velocity = Speed * movement;
	}

	private void UpdateUseWeapon() {
		if (CurrentHealth > 0 && Input.GetKeyDown(KeyCode.J)) {
			_weapon.UseWeapon();
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (CurrentHealth <= 0) return;
		
		if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			Debug.Log("Player hit by enemy");
			CurrentHealth -= 1;

			// show hurt effect
			Vector3 hitPoint = collision.GetContact(0).point;
			_hurtParticle.Play(hitPoint, transform.position);
			CameraShaker.Inst.Shake(0.5f, 0.8f);
			// update health bar
			StatusBar.Inst.UpdateHealthText(this);

			if (CurrentHealth <= 0) {
				StartCoroutine(DoReload());
			}
		}
	}
	IEnumerator DoReload() {
		yield return new WaitForSeconds(_hurtParticle.Duration);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
