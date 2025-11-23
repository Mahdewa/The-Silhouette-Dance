using UnityEngine;

public class SimpleSpawner : MonoBehaviour 
{
    [Header("Settings")]
    public GameObject obstaclePrefab; // Masukkan Prefab Mobil disini
    public float spawnInterval = 7f;  // Jarak waktu antar mobil
    public int maxSpawns = 2;         // Berapa kali mobil muncul? (Set ke 2)
    
    private int currentCount = 0;     // Penghitung internal

    void Start() 
    { 
        // Mulai spawn berulang setiap 'spawnInterval' detik
        InvokeRepeating("Spawn", 1f, spawnInterval); 
    }

    void Spawn() 
    {
        // 1. Cek apakah sudah mencapai batas?
        if (currentCount >= maxSpawns)
        {
            CancelInvoke("Spawn"); // Perintah untuk STOP InvokeRepeating
            Debug.Log("Spawn selesai. Tidak ada mobil lagi.");
            return;
        }

        // 2. Spawn Mobil
        if (obstaclePrefab != null)
        {
            Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            currentCount++; // Tambah hitungan (0 -> 1 -> 2)
            Debug.Log("Mobil muncul ke-" + currentCount);
        }
        else
        {
            Debug.LogError("ERROR: Slot Obstacle Prefab masih kosong!");
        }
    }
}