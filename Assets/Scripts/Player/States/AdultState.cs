using UnityEngine;

public class AdultState : PlayerBaseState
{
    public bool isWorking = false; 
    private float walkDirection = 1f;
    private WorkStation currentStation = null;

    public AdultState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 4: Durma (Dewasa)");
        walkDirection = 1f;
        isWorking = false;
        player.transform.localScale = new Vector3(1, 1, 1);
        player.SpriteRend.color = Color.white;
    }

    public override void Update()
    {
        if (isWorking && currentStation != null)
        {
            if (player.InputActions.Gameplay.MainAction.WasPerformedThisFrame())
            {
                bool finished = currentStation.AddWorkProgress();
                // Opsional: Animasi ngetik sebentar atau efek partikel
                if (finished) FinishWorking();
            }
        }
    }

    public override void FixedUpdate()
    {
        // KONDISI KERJA
        if (isWorking)
        {
            player.Rb.linearVelocity = Vector2.zero;
            // Visual Kerja sudah di-set di EnterWorkMode
            return;
        }

        // KONDISI JALAN
        player.Rb.linearVelocity = new Vector2(player.RunSpeed * walkDirection, player.Rb.linearVelocity.y);
        
        if(walkDirection > 0) player.transform.localScale = new Vector3(1, 1, 1);
        else player.transform.localScale = new Vector3(-1, 1, 1);

        // Mainkan Animasi Jalan
        player.UpdateVisuals(null, "Adult-Walk");
    }

    public void EnterWorkMode(WorkStation station)
    {
        isWorking = true;
        currentStation = station;
        
        Debug.Log("Mulai Bekerja");
        
        // --- GANTI SPRITE KERJA ---
        player.UpdateVisuals(player.AdultWorkSprite);
    }

    public void FinishWorking()
    {
        isWorking = false;
        currentStation = null;
        Debug.Log("Selesai Bekerja.");
        // Otomatis akan kembali ke animasi jalan di FixedUpdate
    }
    
    public void FlipDirection()
    {
        walkDirection = -1f;
    }
}