using System.Collections;
using UnityEngine;

public class PregnantState : PlayerBaseState
{
    public bool isMourning = false;
    private bool canMove = true;

    public PregnantState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 6: Epilog (Bumil)");
        player.transform.localScale = new Vector3(1, 1, 1);
        canMove = true;
        isMourning = false;
        player.SpriteRend.color = Color.white;
    }

    public override void FixedUpdate()
    {
        if (isMourning) return; // Visual diurus oleh Coroutine

        // LOGIKA GERAK
        if (canMove && player.InputActions.Gameplay.MainAction.IsPressed())
        {
            player.Rb.linearVelocity = new Vector2(player.RunSpeed * 0.4f, player.Rb.linearVelocity.y);
            
            // Animasi Jalan
            player.UpdateVisuals(null, "Bumil-walk");
        }
        else
        {
            player.Rb.linearVelocity = Vector2.zero;
            
            // Sprite Idle
            player.UpdateVisuals(player.PregnantIdleSprite);
        }
    }

    public void StartMourning(float duration)
    {
        player.StartCoroutine(MourningRoutine(duration));
    }

    private IEnumerator MourningRoutine(float duration)
    {
        isMourning = true;
        canMove = false;
        player.Rb.linearVelocity = Vector2.zero;
        
        // --- GANTI SPRITE MOURN (BERDOA) ---
        player.UpdateVisuals(player.PregnantMournSprite);

        yield return new WaitForSeconds(duration);

        isMourning = false;
        canMove = true;
        // Kembali ke logika FixedUpdate (Idle/Walk)
    }
}