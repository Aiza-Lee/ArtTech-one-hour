using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour {
	public static CameraShaker Inst;

	[SerializeField] private float _magnitudeFactor = 1.0f;

	private Tweener _curPosTweener, _curRotTweener;

	void Awake() {
		if (Inst != null && Inst != this) {
			Destroy(gameObject);
			return;
		}
		Inst = this;
	}

	public void Shake(float duration, float magnitude) {
		if (_curPosTweener != null && _curPosTweener.IsActive()) {
			_curPosTweener.Kill();
			transform.localPosition = Vector3.zero;
		}
		_curPosTweener = transform.DOShakePosition(duration, magnitude * _magnitudeFactor);
		if (_curRotTweener != null && _curRotTweener.IsActive()) {
			_curRotTweener.Kill();
			transform.localRotation = Quaternion.identity;
		}
		_curRotTweener = transform.DOShakeRotation(duration, magnitude * _magnitudeFactor / 3);
	}
}