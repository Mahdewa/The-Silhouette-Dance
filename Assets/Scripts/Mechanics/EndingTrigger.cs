using UnityEngine;
using UnityEngine.SceneManagement; // Wajib ada untuk ganti scene
using System.Collections; // Wajib ada untuk IEnumerator (Delay)

public class GameEndingTrigger : MonoBehaviour
{
    [Header("Transition Settings")]
    public string epilogSceneName = "Epilogue"; // Tulis nama scene tujuan kamu disini PERSIS
    public float delayBeforeLoad = 2f; // Berapa detik layar hitam sebelum pindah scene?
    
    [Header("UI References")]
    public GameObject blackScreenUI; // Canvas hitam penutup layar

    private bool hasTriggered = false; // Supaya trigger gak kepanggil 2x

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(EndGameSequence(other.gameObject));
        }
    }

    IEnumerator EndGameSequence(GameObject playerObj)
    {
        Debug.Log("Trigger Ending Tersentuh. Memulai Transisi ke Epilog...");

        // 1. Matikan Input Player (Supaya gak bisa gerak lagi saat layar gelap)
        // Kita matikan scriptnya, bukan objectnya (supaya coroutine gak ikut mati kalau script ini nempel di player)
        PlayerController pc = playerObj.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.Rb.linearVelocity = Vector2.zero; // Stop paksa
            pc.enabled = false; // Matikan kontrol
            // Opsional: Matikan animasi atau set ke Idle
            // pc.Anim.Play("Pregnant_Idle"); 
        }

        // 2. Munculkan Layar Hitam
        if (blackScreenUI != null) 
        {
            blackScreenUI.SetActive(true);
        }

        // 3. Tunggu Waktu Delay (Misal 2 detik hening)
        yield return new WaitForSeconds(delayBeforeLoad);

        // 4. Pindah Scene
        // Pastikan scene "Epilog" sudah dimasukkan ke Build Settings!
        SceneManager.LoadScene(epilogSceneName);
    }
}