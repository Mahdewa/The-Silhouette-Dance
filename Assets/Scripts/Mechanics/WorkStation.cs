using UnityEngine;
using UnityEngine.Events; // Untuk Event visual (misal layar nyala)

public class WorkStation : MonoBehaviour
{
    [Header("Settings")]
    public int tapsRequired = 15; // Berapa kali harus tekan spasi
    private int currentTaps = 0;

    [Header("Visual Feedback")]
    public GameObject screenGlow; // Masukkan sprite layar PC menyala disini
    public UnityEvent onWorkFinished; // Event saat tugas selesai

    private void Start()
    {
        // Pastikan efek glow mati duluan
        if (screenGlow != null) screenGlow.SetActive(false);
    }

    // Fungsi ini akan dipanggil oleh Player saat tombol Spasi ditekan
    public bool AddWorkProgress()
    {
        currentTaps++;
        
        // Efek visual: Layar berkedip atau suara ketikan bisa ditaruh sini
        if (screenGlow != null) 
            screenGlow.SetActive(!screenGlow.activeSelf); // Toggle nyala/mati ala ngetik

        // Cek apakah pekerjaan selesai
        if (currentTaps >= tapsRequired)
        {
            CompleteTask();
            return true; // Kembalikan true (Selesai)
        }
        
        return false; // Belum selesai
    }

    private void CompleteTask()
    {
        // Matikan collider agar player tidak terjebak lagi di meja ini
        GetComponent<Collider2D>().enabled = false;
        
        if (screenGlow != null) screenGlow.SetActive(true); // Layar nyala terus tanda selesai
        onWorkFinished?.Invoke();
        
        Debug.Log("Tugas di meja ini selesai!");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Cari script AdultState di player
            PlayerController player = other.GetComponent<PlayerController>();
            
            // Pastikan player sedang dalam mode AdultState
            if (player.CurrentState == player.StateAdult) 
            {
                // Beritahu AdultState bahwa kita ada di meja ini
                player.StateAdult.EnterWorkMode(this);
            }
        }
    }
}