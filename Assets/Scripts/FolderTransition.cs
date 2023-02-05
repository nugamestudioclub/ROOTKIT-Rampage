using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class FolderTransition : MonoBehaviour {
	public event EventHandler<FolderTransitionEventArgs> Enter;

	[SerializeField]
	private FolderWindow destination;
	public FolderWindow Destination => destination;
	
	private void OnTriggerEnter2D(Collider2D collision) {
		Enter?.Invoke(this, new FolderTransitionEventArgs(this, collision));
	}
}