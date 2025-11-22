using System.Collections;
using UnityEngine;

public class PregnantState : PlayerBaseState
{
    public bool isMourning = false; // Status sedang ziarah
    private bool canMove = true;    // Apakah boleh jalan?

    public PregnantState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 6: Epilog (Bumil)");
        // Arah gerak ke KANAN (Pulang ke rumah)
        player.transform.localScale = new Vector3(8, 8, 8);
        canMove = true;
        isMourning = false;

        if (player.SpriteRend != null) {
            player.SpriteRend.color = new Color(0.7f, 0.3f, 1f);
        }
    }

    public override void FixedUpdate()
    {
        // Jika sedang ziarah atau dilarang gerak, berhenti total
        if (!canMove || isMourning)
        {
            player.Rb.linearVelocity = Vector2.zero;
            return;
        }

        // Input HOLD (Tahan) - Tanpa Stamina
        if (player.InputActions.Gameplay.MainAction.IsPressed())
        {
            // Jalan pelan ke KANAN
            player.Rb.linearVelocity = new Vector2(player.RunSpeed * 0.4f, player.Rb.linearVelocity.y);
            // player.Anim.Play("Pregnant_Walk");
        }
        else
        {
            player.Rb.linearVelocity = Vector2.zero;
            // player.Anim.Play("Pregnant_Idle");
        }
    }

    // --- LOGIKA ZIARAH (Dipanggil Trigger Makam) ---
    public void StartMourning(float duration)
    {
        // Mulai event ziarah: Hentikan karakter
        player.StartCoroutine(MourningRoutine(duration));
    }

    private IEnumerator MourningRoutine(float duration)
    {
        Debug.Log("Sampai di Makam. Berdoa...");
        isMourning = true;  // Flag true biar gak bisa jalan
        canMove = false;
        player.Rb.linearVelocity = Vector2.zero;
        
        // player.Anim.Play("Pregnant_Mourn");

        yield return new WaitForSeconds(duration); // Tunggu 3 detik

        Debug.Log("Selesai berdoa. Silakan jalan pulang.");
        isMourning = false;
        canMove = true; // Izinkan jalan lagi
        // player.Anim.Play("Pregnant_Idle");
    }
}