using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NormalEnemy : MonoBehaviour {

	[Header("敌人属性")]
	public float ChasingForce = 2f;
	public int Damage = 1;

	[SerializeField] private float maxSpeed = 5f;

	private Rigidbody2D _rgb2D;

	private Transform _target;


	void Awake() {
		_rgb2D = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		if (_target != null) {
			var direction = (_target.position - transform.position).normalized;
			_rgb2D.AddForce(direction * ChasingForce);
			
			if (_rgb2D.velocity.magnitude > maxSpeed) {
				_rgb2D.velocity = _rgb2D.velocity.normalized * maxSpeed;
			}
		} else {
			_rgb2D.velocity = Vector2.zero;
		}
	}

	public void SetTarget(Transform target) {
		_target = target;
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("DamageFromPlayer")) {
			Destroy(gameObject);
		}
	}
}