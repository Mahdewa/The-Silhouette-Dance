using UnityEngine;
public class EndLifeTrigger : MonoBehaviour
{
    public GameObject stageGroupToDisable; // Group 3 (Taman)
    public GameObject stageGroupToEnable;  // Group 4 (Epilog)
    public Transform epilogSpawnPoint;     // Titik start Epilog

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            // 1. Matikan Visual Player (Simulasi hilang di balik pohon)
            player.GetComponent<SpriteRenderer>().enabled = false;
            player.Rb.linearVelocity = Vector2.zero; // Stop gerak
            player.enabled = false; // Matikan input

            // 2. Mulai Delay/Transisi ke Epilog (Disini kita pakai Invoke sederhana)
            Invoke("StartEpilog", 5f); // Tunggu 2 detik hening
        }
    }

    void StartEpilog()
    {
        // Pindah ke Stage Terakhir
        stageGroupToDisable.SetActive(false);
        stageGroupToEnable.SetActive(true);

        // Setup Player Kembali
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.transform.position = epilogSpawnPoint.position;
        playerObj.GetComponent<SpriteRenderer>().enabled = true; // Munculkan lagi
        playerObj.GetComponent<PlayerController>().enabled = true; // Nyalakan input

        // Ganti State ke Pregnant (Bumil)
        PlayerController pc = playerObj.GetComponent<PlayerController>();
        pc.SwitchState(pc.StatePregnant); // (Kita buat script ini langkah selanjutnya)
    }
}