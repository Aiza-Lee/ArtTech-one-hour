using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Laser : MonoBehaviour {

	[Header("基础设置")]
	public Color LineColor = Color.red;
	public float LineWidth = 0.1f;

	[Header("动画设置")]
	public float FadeInTime = 0.1f;
	public float FadeOutTime = 0.2f;
	public float LifeTime = 0.5f;


	private SpriteRenderer _renderer;
	private void Awake() {
		_renderer = GetComponent<SpriteRenderer>();
	}

	public void SpawnAt(Vector3 start, Vector3 end) {
		Debug.Log($"Spawn DamageLine from {start} to {end}");
		var direction = end - start;
		var length = direction.magnitude + LineWidth * 2;

		transform.position = (start + end) / 2;
		transform.up = direction.normalized;
		transform.localScale = new Vector3(LineWidth, length, 1f);

		StartCoroutine(DoLaserEffect());
	}
	private IEnumerator DoLaserEffect() {
		// fade in
		float timer = 0f;
		while (timer < FadeInTime) {
			timer += Time.deltaTime;
			ChangeAlpha(Mathf.Clamp01(timer / FadeInTime));
			yield return null;
		}
		// stay
		timer = 0f;
		while (timer < LifeTime) {
			timer += Time.deltaTime;
			yield return null;
		}
		// fade out
		timer = 0f;
		while (timer < FadeOutTime) {
			timer += Time.deltaTime;
			ChangeAlpha(1f - Mathf.Clamp01(timer / FadeOutTime));
			yield return null;
		}

		Destroy(gameObject);
	}

	private void ChangeAlpha(float alpha) {
		_renderer.color = new Color(
			LineColor.r,
			LineColor.g,
			LineColor.b,
			alpha
		);
	}
}