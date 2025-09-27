using UnityEngine;

public class MiniMap : MonoBehaviour {
	[SerializeField] private Transform _target;

	void Update() {
		if (_target != null) {
			var pos = _target.position;
			transform.position = new Vector3(pos.x, pos.y, transform.position.z);
		}
	}
}
