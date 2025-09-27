using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour {
	[Header("挂载")]
	public HurtParticlePalyer HurtParticle;

	[Header("敌人属性")]
	public float ChasingForce = 2f;
	public int MaxHealth = 1;
	public float MaxSpeed = 5f;

	public Transform Target { get; set; }
	private int _currentHealth;

	private Rigidbody2D _rigidbody;
	private SpriteRenderer _spriteRenderer;
	void Awake() {
		_rigidbody = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_currentHealth = MaxHealth;
	}

	void FixedUpdate() {
		if (Target != null) {
			var direction = (Target.position - transform.position).normalized;
			_rigidbody.AddForce(direction * ChasingForce);

			if (_rigidbody.velocity.magnitude > MaxSpeed) {
				_rigidbody.velocity = _rigidbody.velocity.normalized * MaxSpeed;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("DamageFromPlayer")) {
			
			Vector3 hitPoint = collision.ClosestPoint(transform.position);
			HurtParticle.Play(hitPoint);

			_currentHealth -= 1;
			_spriteRenderer.color *= new Color(0.7f, 0.7f, 0.7f);
			if (_currentHealth <= 0) {
				StartCoroutine(DoDestroy(HurtParticle.Duration));
			}
		}
	}
	
	IEnumerator DoDestroy(float delay) {
		ScoreCounter.Inst.AddScore(1);
		Target = null;
		GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
}