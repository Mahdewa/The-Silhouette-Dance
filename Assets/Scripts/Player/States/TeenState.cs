using System.Collections;
using UnityEngine;

public class TeenState : PlayerBaseState
{
    private bool isShocked = false;

    public TeenState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 3: Asmarandana (Remaja)");
        // Pastikan menghadap Kanan (Scale X positif)
        player.transform.localScale = new Vector3(8, 8, 8);

        if (player.SpriteRend != null) {
            player.SpriteRend.color = Color.green;
        }
    }

    public override void FixedUpdate()
    {
        if (isShocked)
        {
            player.Rb.linearVelocity = Vector2.zero;
            return; 
        }

        // MEKANIK HOLD:
        // Cek apakah tombol SEDANG ditekan/tahan
        if (player.InputActions.Gameplay.MainAction.IsPressed())
        {
            // Jika ditahan -> Berhenti (Main HP / Like)
            player.Rb.linearVelocity = Vector2.zero;
            // player.Anim.Play("Teen_Stop_Like"); 
        }
        else
        {
            // Jika dilepas -> Auto Walk ke KANAN (Vector2.right)
            player.Rb.linearVelocity = new Vector2(player.RunSpeed * 0.8f, player.Rb.linearVelocity.y);
            // player.Anim.Play("Teen_Walk");
        }
    }

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