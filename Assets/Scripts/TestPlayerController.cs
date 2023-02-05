using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour {
	Rigidbody2D body;

	[SerializeField]
	float speed = 100f;

	void Awake() {
		body = GetComponent<Rigidbody2D>();
	}

	void Update() {
		var direction = GetDirection();
		Move(direction);
	}

	private Vector2 GetDirection() {
		if( Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) )
			return Vector2.up;
		else if( Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) )
			return Vector2.left;
		else if( Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) )
			return Vector2.down;
		else if( Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) )
			return Vector2.right;
		else
			return Vector2.zero;
	}

	private void Move(Vector2 direction) {
		if( !Mathf.Approximately(direction.magnitude, 0f) )
			body.MovePosition(body.position + (Time.deltaTime * speed * direction));
	}
}