using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour {
	public static CameraShaker Inst;
	void Awake() {
		if (Inst != null && Inst != this) { Destroy(gameObject); }
		Inst = this;
	}

	[Header("抖动设置")]
	public float MagnitudeFactor = 1.0f;


	private Tweener _curPosTweener, _curRotTweener;
	public void Shake(float duration, float magnitude) {

		if (_curPosTweener != null && _curPosTweener.IsActive()) {
			_curPosTweener.Kill();
			transform.localPosition = new Vector3(0f, 0f, -10f);
		}
		_curPosTweener = transform.DOShakePosition(duration, magnitude * MagnitudeFactor);

		if (_curRotTweener != null && _curRotTweener.IsActive()) {
			_curRotTweener.Kill();
			transform.localRotation = Quaternion.identity;
		}
		_curRotTweener = transform.DOShakeRotation(duration, magnitude * MagnitudeFactor / 3);
	}
}