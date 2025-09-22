using UnityEngine;

public class EnemyGenerator : MonoBehaviour {
	[SerializeField] private Transform _playerTransform;
	[SerializeField] private Enemy _enemyPrefab;
	public float SpawnInterval = 2f;
	public float SpeedUpRate = 0.0000001f;

	private float _lastSpawnTime = 0f;

	void FixedUpdate() {
		SpawnInterval *= 1f - SpeedUpRate;
		if (Time.time - _lastSpawnTime >= SpawnInterval) {
			_lastSpawnTime = Time.time;
			SpawnEnemy();
		}
	}

	private void SpawnEnemy() {
		var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
		enemy.Target = _playerTransform;
		enemy.transform.localScale = Vector3.one * Random.Range(0.65f, 2.4f);
	}
}