using UnityEngine;

public class BabyState : PlayerBaseState
{
    public BabyState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 1: Mijil (Bayi)");
        // Reset velocity saat masuk
        player.Rb.linearVelocity = Vector2.zero;

        if (player.SpriteRend != null) {
            player.SpriteRend.color = new Color(1f, 0.6f, 0.6f);
        }
    }

    public override void Update()
    {
        // Logika: Pemain harus menekan tombol berkali-kali (Tap-Tap)
        if (player.InputActions.Gameplay.MainAction.WasPerformedThisFrame())
        {
            Crawl();
        }
    }

    private void Crawl()
    {
        // Di Unity 2D, Kanan = X Positif, Kiri = X Negatif.
        // GDD bilang "Arah ke Kiri", jadi kita pakai Vector2.left (-1, 0)
        
        player.Rb.AddForce(Vector2.left * player.CrawlForce, ForceMode2D.Impulse);
        
        // Trigger animasi crawl disini jika ada
        Debug.Log("Bayi Merangkak!");
    }
}