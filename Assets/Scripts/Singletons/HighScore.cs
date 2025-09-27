using UnityEngine;

public class HighScore : MonoBehaviour {
	public static HighScore Inst;
	void Awake() {
		if (Inst != null && Inst != this) { Destroy(gameObject); return; }
		Inst = this;
		DontDestroyOnLoad(gameObject);
	}

	public int Score { get; set; } = 0;
}