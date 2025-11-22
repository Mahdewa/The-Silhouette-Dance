using UnityEngine;

public class ChildState : PlayerBaseState
{
    public ChildState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 2: Sinom (Anak)");

        if (player.SpriteRend != null) 
        player.SpriteRend.color = Color.yellow;
    }

    public override void Update()
    {
        // Logika Lompat (Hanya jika menekan tombol & menyentuh tanah)
        if (player.InputActions.Gameplay.MainAction.WasPerformedThisFrame())
        {
            Jump();
        }
    }

    public override void FixedUpdate()
    {
        // Auto-Run Logic: Selalu bergerak ke Kiri konstan
        // Kita maintain velocity X, tapi biarkan velocity Y (gravitasi) apa adanya
        player.Rb.linearVelocity = new Vector2(-player.RunSpeed, player.Rb.linearVelocity.y);
    }

    private void Jump()
    {
        // Cek sederhana apakah di tanah (bisa dikembangkan dengan Raycast nanti)
        if (Mathf.Abs(player.Rb.linearVelocity.y) < 0.01f)
        {
            player.Rb.AddForce(Vector2.up * player.JumpForce, ForceMode2D.Impulse);
            Debug.Log("Anak Lompat!"); // [cite: 128]
        }
    }
}