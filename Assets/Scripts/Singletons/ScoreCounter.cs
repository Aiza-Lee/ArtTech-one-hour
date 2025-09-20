using UnityEngine;

public class ScoreCounter : MonoBehaviour {
	public static ScoreCounter Inst;

	[SerializeField] private TMPro.TMP_Text _scoreText;

	private int _score = 0;

	void Awake() {
		if (Inst != null && Inst != this) {
			Destroy(gameObject);
			return;
		}
		Inst = this;
	}

	void Start() {
		_scoreText.text = _score.ToString();
	}

	public void AddScore(int delta) {
		_score += delta;
		_scoreText.text = _score.ToString();
	}
}