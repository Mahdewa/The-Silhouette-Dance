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
        player.transform.localScale = new Vector3(-8, 8, 8);

        if (player.SpriteRend != null) {
            player.SpriteRend.color = Color.gray;
        }
    }

    public override void FixedUpdate()
    {
        // Cek input TAHAN (Hold)
        if (player.InputActions.Gameplay.MainAction.IsPressed())
        {
            // Jika sudah lelah, pemain tidak bisa jalan meskipun tombol ditekan
            if (isExhausted)
            {
                player.Rb.linearVelocity = Vector2.zero;
                return; 
            }

            // Logic Jalan ke KIRI
            player.Rb.linearVelocity = new Vector2(-player.RunSpeed * 0.5f, player.Rb.linearVelocity.y); // Speed lebih lambat (0.5x)
            // player.Anim.Play("Elderly_Walk");

            // Hitung durasi menahan tombol
            walkTimer += Time.fixedDeltaTime;

            // Jika melebihi batas 3 detik -> Capek!
            if (walkTimer >= maxWalkDuration)
            {
                EnterExhaustedState();
            }
        }
        else
        {
            // Jika tombol DILEPAS -> Reset stamina
            player.Rb.linearVelocity = Vector2.zero;
            // player.Anim.Play("Elderly_Idle");
            
            RecoverStamina();
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