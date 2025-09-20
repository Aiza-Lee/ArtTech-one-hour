using UnityEngine;

public class EnemyGenerator : MonoBehaviour {
	[SerializeField] private Transform _playerTransform;
	[SerializeField] private NormalEnemy _enemyPrefab;
	[SerializeField] private float _spawnInterval = 2f;

	private void Start() {
		InvokeRepeating(nameof(SpawnEnemy), _spawnInterval, _spawnInterval);
	}

	private void SpawnEnemy() {
		var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
		enemy.SetTarget(_playerTransform);
		enemy.gameObject.transform.localScale = Vector3.one * Random.Range(0.8f, 1.6f);
	}
}