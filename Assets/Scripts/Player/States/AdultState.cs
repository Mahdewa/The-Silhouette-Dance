using UnityEngine;

public class AdultState : PlayerBaseState
{
    public bool isWorking = false; 
    private float walkDirection = 1f; 
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
        
        if (player.SpriteRend != null) player.SpriteRend.color = Color.white;
        
        UpdateScaleOnly(walkingScale);
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
        UpdateScaleOnly(walkingScale);
        
        // EFFICIENT SFX LOOP
        if (!AudioManager.Instance.sfxSource.isPlaying)
        {
            AudioManager.Instance.PlaySFX("Walk");
        }

        // --- VISUAL ---
        if (walkDirection > 0) player.UpdateVisuals(null, "Adult-Walk");
        else player.UpdateVisuals(null, "Adult-Walk-Back");
    }

    private void UpdateScaleOnly(float size)
    {
        Vector3 currentScale = player.transform.localScale;
        currentScale.x = size; 
        currentScale.y = size;
        currentScale.z = 1f;
        player.transform.localScale = currentScale;
    }

    public void EnterWorkMode(WorkStation station)
    {
        isWorking = true;
        currentStation = station;
        Debug.Log("Mulai Bekerja");
        
        // Stop suara jalan, ganti typing
        AudioManager.Instance.sfxSource.Stop();
        AudioManager.Instance.PlaySFX("Typing");
        
        player.UpdateVisuals(player.AdultWorkSprite);
        UpdateScaleOnly(workingScale);
    }

    public void FinishWorking()
    {
        isWorking = false;
        currentStation = null;
        Debug.Log("Selesai Bekerja.");
        
        AudioManager.Instance.sfxSource.Stop(); // Stop typing
        AudioManager.Instance.PlaySFX("Likes"); // Feedback selesai
    }
    
    public void FlipDirection()
    {
        walkDirection = -1f; 
        // Suara jalan akan otomatis terhandle di FixedUpdate
        Debug.Log("Putar Balik");
    }
}