using UnityEngine;

public class OneTimeSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject carPrefab;
    public float spawnDelay = 0f; // Berapa detik menunggu sebelum spawn?

    private bool hasSpawned = false;

    // OPSI A: Spawn otomatis berdasarkan Waktu (sejak stage mulai)
    private void Start()
    {
        if (spawnDelay >= 0)
        {
            Invoke("SpawnCar", spawnDelay);
        }
    }

    // OPSI B: Spawn pakai Trigger (Lebih aman biar player pasti lihat)
    // Kalau kamu mau mobilnya muncul PAS player mendekat, pakai ini:
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            SpawnCar();
        }
    }
    */

    void SpawnCar()
    {
        if (hasSpawned) return; // Cegah spawn double
        
        Instantiate(carPrefab, transform.position, Quaternion.identity);
        hasSpawned = true;
        
        Debug.Log("Mobil muncul satu kali di: " + gameObject.name);
    }
}