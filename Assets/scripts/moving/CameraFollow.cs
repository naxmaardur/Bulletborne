using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public movementController target;
	BoxCollider targetCollider;
	public Vector2 focusAreaSize;
	public float verticalOffset;
	FocusArea focusArea;

	public float lookAheadX;
	public float lookSmoothTimeX;
	public float verticalSmoothTime;


	float currentLookAheadX;
	float targetLookAheadX;
	float lookAheadDirX;
	float smoothLookVelocityX;
	float smoothLookVelocityY;

	void Start () {
		targetCollider = target.GetComponent<BoxCollider> ();
		focusArea = new FocusArea (targetCollider.bounds, focusAreaSize);
	}

	void LateUpdate(){
		focusArea.Update (targetCollider.bounds);

		Vector3 focusPosition = focusArea.center + Vector3.forward * verticalOffset;

		if (focusArea.velocity.x != 0) {
			lookAheadDirX = Mathf.Sign (focusArea.velocity.x);
		}

		targetLookAheadX = lookAheadDirX * lookAheadX;
		currentLookAheadX = Mathf.SmoothDamp (currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

		focusPosition.y = Mathf.SmoothDamp (transform.position.y, focusPosition.y, ref smoothLookVelocityY, verticalSmoothTime);
		focusPosition += Vector3.right * currentLookAheadX;
        
		transform.position = focusPosition + Vector3.up * -10;
	}

	void OnDrawGizmos(){
		Gizmos.color = new Color (1, 0, 0, .5f);
		Gizmos.DrawCube (focusArea.center, focusAreaSize);
	}

	struct FocusArea{
		public Vector3 center;
		public Vector3 velocity;
		float left,right;
		float top,bottom;
		public FocusArea(Bounds targetBounds, Vector3 size){
			left = targetBounds.center.x - size.x/2;
			right = targetBounds.center.x + size.x/2;
			bottom = targetBounds.min.y;
			top = targetBounds.min.y + size.y;

			velocity = Vector3.zero;
			center = new Vector3((left+right)/2, (top+bottom)/2);
		}
		public void Update (Bounds targetBounds) {
			float shiftX = 0;
			if (targetBounds.min.x < left) {
				shiftX = targetBounds.min.x - left;
			} else if (targetBounds.max.x > right) {
				shiftX = targetBounds.max.x - right;
			}
			left += shiftX;
			right += shiftX;

			float shiftY = 0;
			if (targetBounds.min.y < bottom) {
				shiftY = targetBounds.min.y - bottom;
			} else if (targetBounds.max.y > top) {
				shiftY = targetBounds.max.y - top;
			}
			bottom += shiftY;
			top += shiftY;
			center = new Vector3 ((left + right) / 2, (top + bottom) / 2);
			velocity = new Vector3 (shiftX, shiftY);
		}
			
	}
}
