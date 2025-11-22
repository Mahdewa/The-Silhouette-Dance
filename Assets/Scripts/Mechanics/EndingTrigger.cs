using UnityEngine;
using UnityEngine.SceneManagement; // Jika ingin restart/quit

public class GameEndingTrigger : MonoBehaviour
{
    public GameObject blackScreenUI; // Canvas hitam penutup layar

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("GAME TAMAT - THE SILHOUETTE DANCE");
        // 1. Matikan Player
        GameObject.FindGameObjectWithTag("Player").SetActive(false);

        // 2. Munculkan Layar Hitam
        if (blackScreenUI != null) blackScreenUI.SetActive(true);

        // 3. Mainkan SFX Bayi (Opsional)
        // AudioManager.Play("BabyCry");
    }
}