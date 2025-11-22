using UnityEngine;

public class AdultState : PlayerBaseState
{
    // Flag untuk mengecek apakah sedang bekerja di meja
    public bool isWorking = false; 
    // Arah jalan (1 = Kanan, -1 = Kiri). Awalnya ke Kanan.
    private float walkDirection = 1f;

    private WorkStation currentStation = null;

    public AdultState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 4: Durma (Dewasa)");
        walkDirection = 1f; // Reset arah ke kanan
        isWorking = false;
        player.transform.localScale = new Vector3(8, 8, 8);

        if (player.SpriteRend != null) {
            player.SpriteRend.color = Color.blue;
        }
    }

    public override void Update()
    {
        // Logika saat SEDANG KERJA (Duduk di meja)
        if (isWorking && currentStation != null)
        {
            if (player.InputActions.Gameplay.MainAction.WasPerformedThisFrame())
            {
                // Kirim sinyal 'ngetik' ke meja
                bool finished = currentStation.AddWorkProgress();
                
                // Play Animasi Ngetik (One Shot)
                // player.Anim.SetTrigger("Type");

                if (finished)
                {
                    FinishWorking();
                }
            }
        }
    }

    public override void FixedUpdate()
    {
        // Jika sedang kerja, jangan jalan
        if (isWorking)
        {
            player.Rb.linearVelocity = Vector2.zero;
            return;
        }

        // Logika Auto Walk Normal
        player.Rb.linearVelocity = new Vector2(player.RunSpeed * walkDirection, player.Rb.linearVelocity.y);
        
        // Update arah hadap visual
        if(walkDirection > 0) player.transform.localScale = new Vector3(8, 8, 8);
        else player.transform.localScale = new Vector3(-8, 8, 8);
    }

    // Fungsi helper untuk dipanggil oleh Trigger Meja Kantor nanti
    public void EnterWorkMode(WorkStation station)
    {
        isWorking = true;
        currentStation = station;
        
        // Snap posisi player biar pas di depan kursi (opsional, biar rapi)
        // player.transform.position = new Vector3(station.transform.position.x, player.transform.position.y, 0);
        
        Debug.Log("Mulai Bekerja (Spam Spasi!)");
        // player.Anim.SetBool("IsSitting", true);
    }

    public void FinishWorking()
    {
        isWorking = false;
        currentStation = null;
        
        Debug.Log("Selesai Bekerja. Lanjut Jalan.");
        // player.Anim.SetBool("IsSitting", false);
    }
    
    // Fungsi helper untuk putar balik saat ujung kantor
    public void FlipDirection()
    {
        walkDirection = -1f; // Ubah arah jadi ke Kiri
    }
}