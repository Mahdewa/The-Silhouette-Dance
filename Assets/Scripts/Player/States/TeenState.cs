using System.Collections;
using UnityEngine;

public class TeenState : PlayerBaseState
{
    private bool isShocked = false;

    public TeenState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 3: Remaja");
        player.transform.localScale = new Vector3(1, 1, 1);
        player.SpriteRend.color = Color.white;
        isShocked = false;
    }

    public override void FixedUpdate()
    {
        // 1. KONDISI KAGET
        if (isShocked)
        {
            player.Rb.linearVelocity = Vector2.zero;
            // Sprite sudah di-set di TriggerShock, jadi tidak perlu update di sini
            return; 
        }

        // 2. LOGIKA NORMAL
        if (player.InputActions.Gameplay.MainAction.IsPressed())
        {
            // Input DITAHAN -> Berhenti
            player.Rb.linearVelocity = Vector2.zero;
            
            // Tampilkan Sprite Idle
            player.UpdateVisuals(player.TeenIdleSprite);
        }
        else
        {
            // Input DILEPAS -> Jalan
            player.Rb.linearVelocity = new Vector2(player.RunSpeed * 0.8f, player.Rb.linearVelocity.y);
            
            // Mainkan Animasi Jalan
            player.UpdateVisuals(null, "Young-Walk");
        }
    }

    public void TriggerShock()
    {
        if (isShocked) return;
        player.StartCoroutine(ShockRoutine());
    }

    private IEnumerator ShockRoutine()
    {
        isShocked = true;
        
        Debug.Log("TIN TIN! Hampir ketabrak! (Shock)");
        player.Rb.linearVelocity = Vector2.zero;
        player.Rb.AddForce(Vector2.left * 2f, ForceMode2D.Impulse); 
        
        // --- GANTI SPRITE KAGET ---
        player.UpdateVisuals(player.TeenShockSprite);

        yield return new WaitForSeconds(1f); 

        isShocked = false;
    }

        public bool IsStopped()
    {
        return player.InputActions.Gameplay.MainAction.IsPressed();
    }
}