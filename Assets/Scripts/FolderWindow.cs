using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FolderWindow : MonoBehaviour {
	[SerializeField]
	TMP_Text txtWindowCaption;

	[SerializeField]
	string folderName = "Folder";

	public bool TryFindTransition(string folderName, out FolderTransition transition) {
		if( transitions == null ) {
			transition = null;
			return false;
		}
		for( int i = 0; i < transitions.Length; ++i ) {
			if( string.Equals(folderName, transitions[i].Destination.FolderName, StringComparison.OrdinalIgnoreCase) ) {
				transition = transitions[i];
				return true;
			}
		}
		transition = null;
		return false;
	}

	[SerializeField]
	private FolderTransition[] transitions;

	public string FolderName => folderName;

	void Start() {
		if( transitions != null )
			foreach( var transition in transitions )
				transition.Enter += Transition_Enter;

		Refresh();
	}

	private static bool transitionInProgress;

	private void Transition_Enter(object sender, FolderTransitionEventArgs e) {
		if( transitionInProgress )
			return;

		var obj = e.Collision.gameObject;
		if( obj.CompareTag("Player") ) {
			transitionInProgress = true;
			var destinationTransform = e.Transition.Destination.FindDestinationTransform(this);
			obj.transform.position = destinationTransform.position;
			StartCoroutine(EndTransition());
		}
	}

	private Transform FindDestinationTransform(FolderWindow sourceWindow) {
		if( TryFindTransition(sourceWindow.FolderName, out var destination) )
			return destination.transform;
		else
			return gameObject.transform;
	}
	private static IEnumerator EndTransition() {
		yield return new WaitForSeconds(0.25f);
		transitionInProgress = false;
	}

	private void OnDrawGizmos() {
		Refresh();
	}

	private void Refresh() {
		if( txtWindowCaption != null )
			txtWindowCaption.text = folderName;
	}
}
