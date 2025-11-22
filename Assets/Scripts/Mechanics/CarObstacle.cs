using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    [Header("Perspective Settings")]
    public float growSpeed = 1f;       // Kecepatan membesar
    public float startScale = 0.5f;    // Ukuran saat muncul (jauh)
    public float maxScale = 2f;      // Ukuran maksimal (depan mata)
    public float dangerThreshold = 1.2f; // Ukuran minimal untuk dianggap "NABRAK"

    private void Start()
    {
        // Set ukuran awal
        transform.localScale = new Vector3(startScale, startScale, 1f);
    }

    private void Update()
    {
        // 1. Logika Membesar (Scale Up)
        if (transform.localScale.x < maxScale)
        {
            float newScale = transform.localScale.x + (growSpeed * Time.deltaTime);
            transform.localScale = new Vector3(newScale, newScale, 1f);
        }
        else
        {
            // Jika sudah mencapai ukuran maksimal (sudah lewat), hapus mobil
            Destroy(gameObject);
        }
    }

    // Kita pakai OnTriggerStay karena mobilnya diam, player yang mungkin jalan melewatinya
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Cek apakah mobil SUDAH CUKUP BESAR untuk menabrak?
            // (Kalau masih kecil/jauh, anggap saja background, jadi aman)
            if (transform.localScale.x >= dangerThreshold)
            {
                PlayerController player = other.GetComponent<PlayerController>();

                if (player.CurrentState is TeenState teenState)
                {
                    // Logika GDD: Kalau berhenti = Aman. Kalau jalan = Tabrak.
                    if (!teenState.IsStopped())
                    {
                        teenState.TriggerShock(); // Player Kaget!
                        
                        // Opsional: Hapus mobil setelah nabrak biar gak kena spam shock
                         Destroy(gameObject); 
                    }
                    else
                    {
                        Debug.Log("Mobil lewat depan muka.. Wushhh.. (Aman karena berhenti)");
                    }
                }
            }
        }
    }
}