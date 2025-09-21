using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class DamageLine : MonoBehaviour {

	[Header("基础设置")]
	public Color LineColor = Color.red;
	public float LineWidth = 0.1f;
	public Sprite LaserSprite;
	
	[Header("动画设置")]
	public float FadeInTime = 0.1f;
	public float FadeOutTime = 0.2f;
	public float LifeTime = 0.5f;
	
	private SpriteRenderer _renderer;
	private float _currentLifetime = 0f;

	private void Awake() {
		_renderer = GetComponent<SpriteRenderer>();

		_renderer.color = LineColor;

		if (LaserSprite == null) {
			throw new System.Exception("请在编辑器中为DamageLine组件指定LaserSprite贴图");
		}

		_renderer.sprite = LaserSprite;
		_renderer.enabled = false;
	}

	public void SpawnAt(Vector3 start, Vector3 end) {
		Debug.Log($"Spawn DamageLine from {start} to {end}");
		transform.position = (start + end) / 2;
		var direction = end - start;
		var length = direction.magnitude + LineWidth * 2;
		
		transform.up = direction.normalized; 
		
		transform.localScale = new Vector3(LineWidth, length, 1f);
		

		StartCoroutine(LaserEffectSequence());
	}

	private IEnumerator LaserEffectSequence() {
		_currentLifetime = 0f;
		_renderer.enabled = true;

		float fadeInTimer = 0f;
		while (fadeInTimer < FadeInTime) {
			fadeInTimer += Time.deltaTime;
			float alpha = Mathf.Clamp01(fadeInTimer / FadeInTime);

			Color brightColor = new(
				LineColor.r, 
				LineColor.g, 
				LineColor.b, 
				alpha
			);
			_renderer.color = brightColor;

			yield return null;
		}

		_currentLifetime = 0f;
		while (_currentLifetime < LifeTime) {
			_currentLifetime += Time.deltaTime;
			yield return null;
		}

		float fadeOutTimer = 0f;
		while (fadeOutTimer < FadeOutTime) {
			fadeOutTimer += Time.deltaTime;
			float alpha = 1f - Mathf.Clamp01(fadeOutTimer / FadeOutTime);

			_renderer.color = new Color(
				LineColor.r, 
				LineColor.g, 
				LineColor.b, 
				alpha
			);

			yield return null;
		}
		
		Destroy(gameObject);
	}
}