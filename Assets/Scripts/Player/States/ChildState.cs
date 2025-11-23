using UnityEngine;
using System.Collections;

public class ChildState : PlayerBaseState
{
    private bool isFalling = false;
    
    // Kita tidak butuh variabel timer manual jika menggunakan isPlaying check
    
    public ChildState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 2: Sinom (Anak)");
        if (player.SpriteRend != null) player.SpriteRend.color = Color.white;
        isFalling = false;
    }

    public override void Exit()
    {
        // Paksa berhenti semua suara (terutama suara Lari yang looping)
        if (AudioManager.Instance.sfxSource.isPlaying)
        {
            AudioManager.Instance.sfxSource.Stop();
        }
        Debug.Log("Keluar dari Stage Anak. Audio Stop.");
    }

    public override void Update()
    {
        if (!isFalling && player.InputActions.Gameplay.MainAction.WasPerformedThisFrame())
        {
            Jump();
        }
    }

    public override void FixedUpdate()
    {
        // 1. KONDISI JATUH
        if (isFalling)
        {
            player.UpdateVisuals(player.ChildFallSprite);
            return;
        }

        // 2. AUTO RUN LOGIC
        player.Rb.linearVelocity = new Vector2(-player.RunSpeed, player.Rb.linearVelocity.y);

        // --- UPDATE VISUAL & AUDIO ---
        // Cek apakah di udara (Lompat) atau di tanah (Lari)
        if (Mathf.Abs(player.Rb.linearVelocity.y) > 0.1f) 
        {
             // UDARA -> Stop Run SFX
             player.UpdateVisuals(player.ChildJumpSprite);
             
             // Kalau lagi lompat, jangan ada suara lari
             if (AudioManager.Instance.sfxSource.clip != null && AudioManager.Instance.sfxSource.clip.name == "Run")
             {
                 AudioManager.Instance.sfxSource.Stop(); 
             }
        }
        else
        {
             // TANAH -> Play Run SFX Loop
             player.UpdateVisuals(null, "Child-Run");
             
             // EFFICIENT SFX: Hanya play jika belum bunyi
             if (!AudioManager.Instance.sfxSource.isPlaying)
             {
                 AudioManager.Instance.PlaySFX("Run");
             }
        }
    }

    private void Jump()
    {
        if (Mathf.Abs(player.Rb.linearVelocity.y) < 0.01f)
        {
            player.Rb.AddForce(Vector2.up * player.JumpForce, ForceMode2D.Impulse);
            
            // Hentikan suara lari seketika saat lompat
            AudioManager.Instance.sfxSource.Stop();
            AudioManager.Instance.PlaySFX("Jump");
        }
    }

    public void TriggerTrip()
    {
        if (isFalling) return; 
        player.StartCoroutine(FallRoutine());
    }

    private IEnumerator FallRoutine()
    {
        isFalling = true;
        Debug.Log("Anak jatuh!");
        
        // Stop suara lari
        AudioManager.Instance.sfxSource.Stop();
        AudioManager.Instance.PlaySFX("Fall");
        
        player.Rb.linearVelocity = Vector2.zero;
        player.Rb.AddForce(Vector2.left * 3f, ForceMode2D.Impulse);
        
        player.UpdateVisuals(player.ChildFallSprite);
        
        yield return new WaitForSeconds(1.5f);
        
        isFalling = false;
    }
}