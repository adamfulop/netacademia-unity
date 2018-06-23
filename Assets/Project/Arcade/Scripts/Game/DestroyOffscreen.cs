using Lean.Pool;
using UnityEngine;

public class DestroyOffscreen : MonoBehaviour {
	public float Offset;

	private bool _offScreen;
	private float _offScreenX;

	private PixelPerfectCamera _pixelPerfectCamera;
	private Rigidbody2D _rigidbody2D;
	
	// GameControllerhez event
	public delegate void OnDestroy();
	public event OnDestroy DestroyCallback;

	private void Awake() {
		_pixelPerfectCamera = FindObjectOfType<PixelPerfectCamera>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void Start() {
		_offScreenX = Screen.width / 2f / _pixelPerfectCamera.PixelPerUnit + Offset;
	}

	private void Update() {
		var positionX = transform.position.x;
		var directionX = _rigidbody2D.velocity.x;

		if (Mathf.Abs(positionX) > _offScreenX) {
			if (directionX < 0 && positionX < -_offScreenX) {
				_offScreen = true;
			} else if (directionX > 0 && positionX > _offScreenX) {
				_offScreen = true;
			}
		} else {
			_offScreen = false;
		}

		if (_offScreen) {
			OnOutOfBounds();
		}
	}

	public void OnOutOfBounds() {
		_offScreen = false;
		// Pooling nélkül: Destroy(gameObject);
		// Saját pool: GameObjectUtil.Destroy(gameObject);
		LeanPool.Despawn(gameObject);
		
		// GameControllerhez event
		if (DestroyCallback != null) {
			DestroyCallback();
		}
	}
}
