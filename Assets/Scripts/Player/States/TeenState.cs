using System.Collections;
using UnityEngine;

public class TeenState : PlayerBaseState
{
    private bool isShocked = false; // Status kena penalty

    public TeenState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 3: Remaja");
        player.transform.localScale = new Vector3(8, 8, 8); // Hadap Kanan
        isShocked = false;
    }

    public override void FixedUpdate()
    {
        // 1. Jika sedang Kaget (Shock), hentikan semua input & gerakan
        if (isShocked)
        {
            player.Rb.linearVelocity = Vector2.zero;
            return; 
        }

        // 2. Logika Normal (Jalan vs Berhenti)
        if (player.InputActions.Gameplay.MainAction.IsPressed())
        {
            // Input DITAHAN -> Berhenti (Aman)
            player.Rb.linearVelocity = Vector2.zero;
            // player.Anim.Play("Teen_Stop_Like"); 
        }
        else
        {
            // Input DILEPAS -> Jalan (Bahaya jika ada mobil)
            player.Rb.linearVelocity = new Vector2(player.RunSpeed * 0.8f, player.Rb.linearVelocity.y);
            // player.Anim.Play("Teen_Walk");
        }
    }

    // Fungsi ini akan dipanggil oleh Mobil/Motor
    public void TriggerShock()
    {
        // Kalau sudah kaget, jangan kaget lagi (biar gak spam)
        if (isShocked) return;

        player.StartCoroutine(ShockRoutine());
    }

    private IEnumerator ShockRoutine()
    {
        isShocked = true;
        
        // Efek Visual & Audio
        Debug.Log("TIN TIN! Hampir ketabrak! (Shock)");
        player.Rb.linearVelocity = Vector2.zero; // Stop paksa
        player.Rb.AddForce(Vector2.left * 2f, ForceMode2D.Impulse); // Efek terpental dikit ke belakang
        
        // player.Anim.Play("Teen_Shock");
        // AudioManager.Play("CarHonk"); 

        // DELAY 1 DETIK (Sesuai GDD)
        yield return new WaitForSeconds(1f); 

        // Pulih kembali
        isShocked = false;
        Debug.Log("Lanjut jalan...");
        // player.Anim.Play("Teen_Walk");
    }
    
    // Helper untuk mengecek apakah player sedang berhenti (Aman)
    public bool IsStopped()
    {
        return player.InputActions.Gameplay.MainAction.IsPressed();
    }
}