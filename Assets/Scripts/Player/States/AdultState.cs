using UnityEngine;

public class AdultState : PlayerBaseState
{
    public bool isWorking = false; 
    private float walkDirection = 1f; // 1 = Kanan, -1 = Kiri
    private WorkStation currentStation = null;

    [Header("Scale Settings")]
    public float walkingScale = 0.5f; 
    public float workingScale = 0.4f; 

    public AdultState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 4: Durma (Dewasa)");
        walkDirection = 1f;
        isWorking = false;
        
        // Reset Visual
        if (player.SpriteRend != null) player.SpriteRend.color = Color.white;
        
        // Set Ukuran Awal
        UpdateScaleOnly(walkingScale);
        
        // Mulai animasi jalan biasa
        player.UpdateVisuals(null, "Adult-Walk");
    }

    public override void Update()
    {
        if (isWorking && currentStation != null)
        {
            if (player.InputActions.Gameplay.MainAction.WasPerformedThisFrame())
            {
                bool finished = currentStation.AddWorkProgress();
                if (finished) FinishWorking();
            }
        }
    }

    public override void FixedUpdate()
    {
        // --- KONDISI KERJA (Diam) ---
        if (isWorking)
        {
            player.Rb.linearVelocity = Vector2.zero;
            UpdateScaleOnly(workingScale);
            return;
        }

        // --- KONDISI JALAN (Gerak) ---
        player.Rb.linearVelocity = new Vector2(player.RunSpeed * walkDirection, player.Rb.linearVelocity.y);
        
        // Update Ukuran
        UpdateScaleOnly(walkingScale);

        // --- PEMILIHAN ANIMASI BERDASARKAN ARAH ---
        if (walkDirection > 0)
        {
            // Jalan ke KANAN
            player.UpdateVisuals(null, "Adult-Walk");
        }
        else
        {
            // Jalan ke KIRI (Animation sudah di-flip dari editor)
            player.UpdateVisuals(null, "Adult-Walk-Back");
        }
    }

    // Fungsi Helper: HANYA Mengatur Ukuran (Tidak mengurus Arah/Flip lagi)
    private void UpdateScaleOnly(float size)
    {
        Vector3 currentScale = player.transform.localScale;
        currentScale.x = size; // Selalu positif
        currentScale.y = size;
        currentScale.z = 1f;
        player.transform.localScale = currentScale;
    }

    public void EnterWorkMode(WorkStation station)
    {
        isWorking = true;
        currentStation = station;
        Debug.Log("Mulai Bekerja");
        
        player.UpdateVisuals(player.AdultWorkSprite);
        UpdateScaleOnly(workingScale);
    }

    public void FinishWorking()
    {
        isWorking = false;
        currentStation = null;
        Debug.Log("Selesai Bekerja.");
    }
    
    public void FlipDirection()
    {
        // Cukup ubah arah logikanya saja.
        // Animasi "Adult-Walk-Back" akan otomatis terpanggil di FixedUpdate frame berikutnya.
        walkDirection = -1f; 
        
        Debug.Log("Putar Balik -> Panggil Animasi Back");
    }
}