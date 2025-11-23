using UnityEngine;

public class BabyState : PlayerBaseState
{
    public BabyState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 1: Mijil (Bayi)");
        player.Rb.linearVelocity = Vector2.zero;
        if (player.SpriteRend != null) player.SpriteRend.color = Color.white;
    }

    public override void Update()
    {
        if (player.InputActions.Gameplay.MainAction.WasPerformedThisFrame())
        {
            Crawl();
        }

        // --- UPDATE VISUAL ---
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
        
        // EFFICIENT SFX CALL: Play only if not already playing
        if (!AudioManager.Instance.sfxSource.isPlaying)
        {
            AudioManager.Instance.PlaySFX("Crawling");
        }
    }
}