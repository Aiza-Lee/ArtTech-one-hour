using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

	[Header("挂载")]
	[SerializeField] private Weapon _weapon;
	[SerializeField] private ParticleTrigger _hurtParticle;
	[SerializeField] private float _beatBackRadius = 2f;

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

		// transform.position += Speed * Time.deltaTime * (Vector3) movement;
		_rgb2D.velocity = Speed * movement;
	}

	private void UpdateUseWeapon() {
		if (Input.GetKeyDown(KeyCode.J)) {
			_weapon.UseWeapon();
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			Debug.Log("Player hit by enemy");
			CurrentHealth -= 1;
			if (CurrentHealth <= 0) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			} else {
				// show hurt effect
				Vector3 hitPoint = collision.GetContact(0).point;
				Vector3 hitDirection = hitPoint - transform.position;
				_hurtParticle.Play(-hitDirection);
				CameraShaker.Inst.Shake(0.5f, 0.8f);

				// enemies knock back
				Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _beatBackRadius, LayerMask.GetMask("Enemy"));
				var cnt = enemies.Count();
				foreach (Collider2D enemy in enemies) {
					if (enemy.TryGetComponent<Rigidbody2D>(out var rb)) {
						Vector2 dir = (enemy.transform.position - transform.position).normalized;
						rb.AddForce(70f * cnt * cnt * dir, ForceMode2D.Impulse);
					}
				}
				
				// update health bar
				StatusBar.Inst.UpdateHealthText(this);
			}
		}
	}
}
