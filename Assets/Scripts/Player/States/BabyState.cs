using UnityEngine;

public class BabyState : PlayerBaseState
{
    public BabyState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 1: Mijil (Bayi)");
        player.Rb.linearVelocity = Vector2.zero;
        player.SpriteRend.color = Color.white; // Reset warna tint jika ada
    }

    public override void Update()
    {
        if (player.InputActions.Gameplay.MainAction.WasPerformedThisFrame())
        {
            Crawl();
        }

        // --- UPDATE VISUAL ---
        // Jika kecepatan cukup tinggi, play animasi. Jika diam, tampilkan sprite Idle.
        if (player.Rb.linearVelocity.magnitude > 0.1f)
        {
            player.UpdateVisuals(null, "Baby_Crawl");
        }
        else
        {
            player.UpdateVisuals(player.BabyIdleSprite);
        }
    }

    private void Crawl()
    {
        player.Rb.AddForce(Vector2.left * player.CrawlForce, ForceMode2D.Impulse);
    }
}