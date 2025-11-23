using UnityEngine;

public class ChildState : PlayerBaseState
{
    public ChildState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 2: Sinom (Anak)");
        player.SpriteRend.color = Color.white;
    }

    public override void Update()
    {
        if (player.InputActions.Gameplay.MainAction.WasPerformedThisFrame())
        {
            Jump();
        }
    }

    public override void FixedUpdate()
    {
        // Auto-Run Logic
        player.Rb.linearVelocity = new Vector2(-player.RunSpeed, player.Rb.linearVelocity.y);

        // --- UPDATE VISUAL ---
        // Cek apakah di udara (Lompat) atau di tanah (Lari)
        if (Mathf.Abs(player.Rb.linearVelocity.y) > 0.1f) // Sedang di udara
        {
             // Gunakan Sprite Lompat
             player.UpdateVisuals(player.ChildJumpSprite);
        }
        else
        {
             // Di tanah, gunakan Animasi Lari
             player.UpdateVisuals(null, "Child-Run");
        }
    }

    private void Jump()
    {
        if (Mathf.Abs(player.Rb.linearVelocity.y) < 0.01f)
        {
            player.Rb.AddForce(Vector2.up * player.JumpForce, ForceMode2D.Impulse);
        }
    }
}