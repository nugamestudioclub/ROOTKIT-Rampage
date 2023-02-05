using System;
using TMPro;
using UnityEngine;

public class FolderTransition : MonoBehaviour {
	[SerializeField]
	TMP_Text txtCaption;

	public event EventHandler<FolderTransitionEventArgs> Enter;

	[SerializeField]
	private FolderWindow destination;
	public FolderWindow Destination => destination;

	[SerializeField]
	private bool isOneWay;

	void Start() {
		Refresh();
	}

	private void OnDrawGizmos() {
		Refresh();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if( !isOneWay )
			Enter?.Invoke(this, new FolderTransitionEventArgs(this, collision));
	}

	private void Refresh() {
		if( txtCaption != null && destination != null )
			txtCaption.text = destination.FolderName;
	}
}

public class FolderTransitionEventArgs : EventArgs {

	private readonly FolderTransition transition;
	public FolderTransition Transition => transition;

	private readonly Collider2D collision;
	public Collider2D Collision => collision;

	public FolderTransitionEventArgs(FolderTransition transition, Collider2D collision) {
		if( transition == null )
			throw new ArgumentNullException(nameof(transition));
		if( collision == null )
			throw new ArgumentNullException(nameof(collision));
		this.transition = transition;
		this.collision = collision;
	}
}