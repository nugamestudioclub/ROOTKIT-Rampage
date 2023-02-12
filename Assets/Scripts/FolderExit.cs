using System;
using TMPro;
using UnityEngine;

public class FolderExit : MonoBehaviour {
	[SerializeField]
	TMP_Text txtCaption;

	[SerializeField]
	private string caption;

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private Sprite spriteLocked;
	[SerializeField]
	private Sprite spriteUnlocked;

	private bool locked = true;
	public bool Locked {
		get => locked;
		set {
			locked = value;
			Refresh();
		}
	}

	void Start() {
		Refresh();
		GameState.Instance.Exit = this;
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if( Locked )
			return;
		var obj = collision.gameObject;
		if( obj.CompareTag("Player") )
			TransitionManager.ToWin();
	}

	void OnDrawGizmos() {
		Refresh();
	}

	private void Refresh() {
		if( txtCaption != null )
			txtCaption.text = caption;
		if( spriteRenderer != null )
			spriteRenderer.sprite = locked ? spriteLocked : spriteUnlocked;
	}
}