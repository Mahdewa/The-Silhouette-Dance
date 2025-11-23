using UnityEngine;

public class ElderlyState : PlayerBaseState
{
    private float walkTimer = 0f;
    private float maxWalkDuration = 3.0f; // Batas stamina (3 detik)
    private bool isExhausted = false; // Status kelelahan

    public ElderlyState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 5: Pungkur (Lansia)");
        walkTimer = 0f;
        isExhausted = false;
        // Pastikan menghadap Kiri (Visual)
        player.transform.localScale = new Vector3(-1, 1, 1);

        if (player.SpriteRend != null) {
            player.SpriteRend.color = Color.gray;
        }
    }

public override void FixedUpdate()
{
    if (player.InputActions.Gameplay.MainAction.IsPressed())
    {
        if (isExhausted) 
        {
            player.Rb.linearVelocity = Vector2.zero;
            player.UpdateVisuals(player.ElderlyIdleSprite); // Capek = Diam/Idle
            return; 
        }

        player.Rb.linearVelocity = new Vector2(-player.RunSpeed * 0.5f, player.Rb.linearVelocity.y);
        
        // Animasi Jalan
        player.UpdateVisuals(null, "Elder-Walk");

        walkTimer += Time.fixedDeltaTime;
        if (walkTimer >= maxWalkDuration) EnterExhaustedState();
    }
    else
    {
        player.Rb.linearVelocity = Vector2.zero;
        RecoverStamina();
        
        // Sprite Idle
        player.UpdateVisuals(player.ElderlyIdleSprite);
    }
}

    private void EnterExhaustedState()
    {
        isExhausted = true;
        Debug.Log("Nenek Capek! Lepas tombol untuk istirahat.");
        // Disini nanti bisa trigger efek Vignette (layar menggelap)
        // player.Anim.Play("Elderly_Exhausted");
    }

    private void RecoverStamina()
    {
        // Reset timer instan saat tombol dilepas (sesuai GDD: lepas sebentar, tahan lagi)
        walkTimer = 0f;
        isExhausted = false;
    }
}