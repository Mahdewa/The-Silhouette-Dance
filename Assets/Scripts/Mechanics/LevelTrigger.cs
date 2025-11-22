using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [Header("1. Change Player State?")]
    public bool changeState = false;
    // Kita pakai Enum sederhana agar mudah dipilih di Inspector
    public enum PlayerStateOption { Baby, Child, Teen, Adult, Elderly, Pregnant }
    public PlayerStateOption targetState;

    [Header("2. Change Stage Group?")]
    public bool changeStageGroup = false;
    public GameObject currentGroupToDisable; // Group lama yang akan dimatikan
    public GameObject nextGroupToEnable;     // Group baru yang akan dinyalakan

    [Header("3. Reposition Player?")]
    public bool repositionPlayer = false;
    public Transform newSpawnPoint; // Titik koordinat tujuan teleport

    [Header("4. Camera Switch?")]
    public bool switchCamera = false;
    public GameObject cameraToEnable;  // Kamera baru yang mau dinyalakan
    public GameObject cameraToDisable; // Kamera lama yang mau dimatikan

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah yang menabrak adalah Player
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            
            if (player != null)
            {
                ExecuteTrigger(player);
            }
        }
    }

    private void ExecuteTrigger(PlayerController player)
    {
        // 1. Logika Ganti State
        if (changeState)
        {
            switch (targetState)
            {
                case PlayerStateOption.Baby:
                    player.SwitchState(player.StateBaby);
                    break;
                case PlayerStateOption.Child:
                    player.SwitchState(player.StateChild);
                    break;
                case PlayerStateOption.Teen:
                    player.SwitchState(player.StateTeen);
                    break;
                case PlayerStateOption.Adult:
                    player.SwitchState(player.StateAdult);
                    break;
                case PlayerStateOption.Elderly:
                    player.SwitchState(player.StateElderly);
                    break;
                case PlayerStateOption.Pregnant:
                    player.SwitchState(player.StatePregnant);
                    break;
            }
            Debug.Log("State Changed to: " + targetState);
        }

        // 2. Logika Ganti Stage Group (Parent)
        if (changeStageGroup)
        {
            if (currentGroupToDisable != null) currentGroupToDisable.SetActive(false);
            if (nextGroupToEnable != null) nextGroupToEnable.SetActive(true);
        }

        // 3. Logika Teleportasi
        if (repositionPlayer && newSpawnPoint != null)
        {
            player.transform.position = newSpawnPoint.position;
            // Reset velocity agar tidak 'terlempar' saat teleport
            player.Rb.linearVelocity = Vector2.zero; 
        }

        // 4. Logika Ganti Kamera
        if (switchCamera)
        {
            if (cameraToDisable != null) cameraToDisable.SetActive(false);
            if (cameraToEnable != null) cameraToEnable.SetActive(true);
        }
        
        // Matikan trigger ini agar tidak terpanggil dua kali
        gameObject.SetActive(false);
    }
}