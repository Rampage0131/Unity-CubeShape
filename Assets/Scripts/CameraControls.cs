using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

    private const float cameraDistance = 13.0f;
    private const float animationDuration = 1.0f;

    private Vector3 newPosition = Vector3.zero;

	void Start() {

	}

    void OnGUI() {
        if (GameObject.Find("PlayerCube") == null) {
            GUILayout.Label("Game Over");
        }
    }

	void Update() {
        if (iTween.Count(gameObject) == 0) {
            Vector3 movement = Vector3.zero;
            Vector3 rotation = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.D)) {
                movement = transform.right + transform.forward;
                rotation.y = -0.25f;
            } else if (Input.GetKeyDown(KeyCode.A)) {
                movement = -transform.right + transform.forward;
                rotation.y = 0.25f;
            } else if (Input.GetKeyDown(KeyCode.W)) {                
                movement = transform.up + transform.forward;
                rotation.x = 0.25f;
            } else if (Input.GetKeyDown(KeyCode.S)) {
                movement = -transform.up + transform.forward;
                rotation.x = -0.25f;
            } else if (Input.GetKeyDown(KeyCode.Q)) {
                rotation.z = -0.25f;
            } else if (Input.GetKeyDown(KeyCode.E)) {
                rotation.z = 0.25f;
            }

            if (!movement.Equals(Vector3.zero)) {
                newPosition = transform.position + movement * cameraDistance;
                newPosition.Set(Mathf.Round(newPosition.x), Mathf.Round(newPosition.y), Mathf.Round(newPosition.z));

                Hashtable moveTo = new Hashtable();
                moveTo.Add("position", newPosition);
                moveTo.Add("time", animationDuration);
                iTween.MoveTo(gameObject, moveTo);
            }

            if (!rotation.Equals(Vector3.zero)) {
                Hashtable rotateBy = new Hashtable();
                rotateBy.Add("amount", rotation);
                rotateBy.Add("time", animationDuration);
                rotateBy.Add("onComplete", "AfterRotation");
                iTween.RotateBy(gameObject, rotateBy);
            }
        }
	}

    IEnumerable AfterRotation() {
        // Round the angles
        transform.eulerAngles = new Vector3(
            Mathf.Round(transform.rotation.eulerAngles.x),
            Mathf.Round(transform.rotation.eulerAngles.y),
            Mathf.Round(transform.rotation.eulerAngles.z));
        return null;
    }
}
