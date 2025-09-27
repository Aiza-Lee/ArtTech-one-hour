using UnityEngine;

public class ScoreCounter : MonoBehaviour {
	public static ScoreCounter Inst;
	void Awake() {
		if (Inst != null && Inst != this) { Destroy(gameObject); return; }
		Inst = this;
		DontDestroyOnLoad(gameObject);
	}

	[SerializeField] private TMPro.TMP_Text _scoreText;
	[SerializeField] private TMPro.TMP_Text _highScoreText;

	private int _score = 0;

	void Start() {
		_scoreText.text = _score.ToString();
		_highScoreText.text = "/ " + HighScore.Inst.Score.ToString();
	}

	public void AddScore(int delta) {
		_score += delta;
		_scoreText.text = _score.ToString();
		if (_score > HighScore.Inst.Score) {
			HighScore.Inst.Score = _score;
			_highScoreText.text = "/ " + HighScore.Inst.Score.ToString();
		}
	}
}