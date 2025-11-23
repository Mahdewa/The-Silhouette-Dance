using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    [Header("Settings")]
    public float carSpeed = 10f; // Kecepatan mobil lewat

    private void Update()
    {
        // Mobil bergerak ke Kiri (Melawan arah player remaja)
        transform.Translate(Vector3.left * carSpeed * Time.deltaTime);
        
        // Hapus mobil jika sudah jauh lewat layar (biar hemat memori)
        if (transform.position.x < -75f) // Sesuaikan angka ini
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            // Pastikan player ada di fase Remaja
            if (player.CurrentState is TeenState teenState)
            {
                // CEK: Apakah pemain sedang berhenti (aman)?
                if (teenState.IsStopped())
                {
                    Debug.Log("Player berhenti tepat waktu. Aman.");
                    // Bisa tambah score/bubble "Like" disini
                }
                else
                {
                    // Pemain nekat jalan -> Kena Shock!
                    teenState.TriggerShock();
                }
            }
        }
    }
}