using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FolderWindow : MonoBehaviour {
	[SerializeField]
	TMP_Text txtCaption;

	[SerializeField]
	string folderName = "Folder";
	[SerializeField]
	private FolderTransition[] transitions;

	public string FolderName => folderName;

	private static bool transitionInProgress;

	void Start() {
		if( transitions != null )
			foreach( var transition in transitions )
				transition.Enter += Transition_Enter;

		Refresh();
	}

	private void OnDrawGizmos() {
		Refresh();
	}

	private static IEnumerator EndTransition() {
		yield return new WaitForSeconds(0.25f);
		transitionInProgress = false;
	}

	private Transform FindDestinationTransform(FolderWindow sourceWindow) {
		if( TryFindTransition(sourceWindow.FolderName, out var destination) )
			return destination.transform;
		else
			return gameObject.transform;
	}

	private void Refresh() {
		if( txtCaption != null )
			txtCaption.text = folderName;
	}

	public bool TryFindTransition(string folderName, out FolderTransition transition) {
		if( transitions == null ) {
			transition = null;
			return false;
		}
		for( int i = 0; i < transitions.Length; ++i ) {
			if( transitions[i].Destination != null && string.Equals(folderName, transitions[i].Destination.FolderName, StringComparison.OrdinalIgnoreCase) ) {
				transition = transitions[i];
				return true;
			}
		}
		transition = null;
		return false;
	}

	private void Transition_Enter(object sender, FolderTransitionEventArgs e) {
		if( transitionInProgress )
			return;

		var obj = e.Collision.gameObject;
		if( obj.CompareTag("Player") ) {
            GameState.Instance.ResetPlayerHealth();
            transitionInProgress = true;
			var destinationTransform = e.Transition.Destination.FindDestinationTransform(this);
			obj.transform.position = destinationTransform.position;
			StartCoroutine(EndTransition());
		}
	}
}