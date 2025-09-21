using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class NormalEnemy : MonoBehaviour {

	[Header("敌人属性")]
	public float ChasingForce = 2f;
	public int Damage = 1;

	[SerializeField] private ParticleTrigger _hurtParticle;

	[SerializeField] private float _maxSpeed = 5f;

	public int ScoreValue = 1;
	public int MaxHealth = 1;

	private int _currentHealth;

	private Rigidbody2D _rgb2D;
	private SpriteRenderer _spriteRenderer;
	private Transform _target;


	void Awake() {
		_rgb2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_currentHealth = MaxHealth;
	}

	void FixedUpdate() {
		if (_target != null) {
			var direction = (_target.position - transform.position).normalized;
			_rgb2D.AddForce(direction * ChasingForce);

			if (_rgb2D.velocity.magnitude > _maxSpeed) {
				_rgb2D.velocity = _rgb2D.velocity.normalized * _maxSpeed;
			}
		}
	}

	void OnDestroy() {
		ScoreCounter.Inst.AddScore(1);
	}

	public void SetTarget(Transform target) {
		_target = target;
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("DamageFromPlayer")) {
			Vector3 hitPoint = collision.ClosestPoint(transform.position);
			Vector3 hitDirection = hitPoint - transform.position;
			_hurtParticle.Play(-hitDirection);

			_currentHealth -= 1;
			_spriteRenderer.color *= new Color(0.7f, 0.7f, 0.7f);
			if (_currentHealth <= 0) {
				StartCoroutine(DoDestroy());
			}
		}
	}
	
	IEnumerator DoDestroy() {
		_target = null;
		GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds(_hurtParticle.Duration * 2f);
		Destroy(gameObject);
	}
}