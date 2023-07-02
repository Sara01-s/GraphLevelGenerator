using UnityEngine.SceneManagement;
using UnityEngine;

internal sealed class RestartLevelOnTouch : MonoBehaviour {

    private void OnTriggerEnter(Collider triggered) {
        if (triggered.gameObject.CompareTag("FinishLine")) {
            Debug.Log("Reinicio");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
