using UnityEngine;

public class ElderlyState : PlayerBaseState
{
    private float walkTimer = 0f;
    private float maxWalkDuration = 3.0f; 
    private bool isExhausted = false; 

    public ElderlyState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 5: Pungkur (Lansia)");
        walkTimer = 0f;
        isExhausted = false;
        player.transform.localScale = new Vector3(-1, 1, 1);

        if (player.SpriteRend != null) player.SpriteRend.color = Color.white;
    }

    public override void FixedUpdate()
    {
        // --- KONDISI 1: INPUT DITAHAN (Jalan) ---
        if (player.InputActions.Gameplay.MainAction.IsPressed())
        {
            if (isExhausted) 
            {
                player.Rb.linearVelocity = Vector2.zero;
                player.UpdateVisuals(player.ElderlyIdleSprite);
                
                // Kalau lagi capek, jangan putar suara jalan (tapi biarkan suara batuk)
                if (AudioManager.Instance.sfxSource.isPlaying)
                {
                    // Cek: Kalau yang bunyi itu "Walk", matikan. Kalau "Cough", biarkan.
                    if (IsPlayingClip("Walk"))
                    {
                        AudioManager.Instance.sfxSource.Stop();
                    }
                }
                return; 
            }

            // Logika Jalan Normal
            player.Rb.linearVelocity = new Vector2(-player.RunSpeed * 0.5f, player.Rb.linearVelocity.y);
            player.UpdateVisuals(null, "Elder-Walk");
            
            // Play SFX Jalan (Hanya jika belum bunyi)
            if (!AudioManager.Instance.sfxSource.isPlaying)
            {
                AudioManager.Instance.PlaySFX("Walk");
            }

            // Hitung Stamina
            walkTimer += Time.fixedDeltaTime;
            if (walkTimer >= maxWalkDuration) 
            {
                EnterExhaustedState();
            }
        }
        // --- KONDISI 2: INPUT DILEPAS (Diam) ---
        else
        {
            player.Rb.linearVelocity = Vector2.zero;
            player.UpdateVisuals(player.ElderlyIdleSprite);
            RecoverStamina();

            // PERBAIKAN DISINI:
            // Stop suara HANYA jika itu bukan Batuk
            if (AudioManager.Instance.sfxSource.isPlaying) 
            {
                // Jika yang sedang main BUKAN Cough, baru boleh di-stop
                if (!IsPlayingClip("Cough")) 
                {
                    AudioManager.Instance.sfxSource.Stop();
                }
            }
        }
    }

    private void EnterExhaustedState()
    {
        if (!isExhausted) 
        {
            isExhausted = true;
            
            // Stop suara jalan dulu
            AudioManager.Instance.sfxSource.Stop();
            
            // Mainkan Batuk
            AudioManager.Instance.PlaySFX("Cough");
            
            Debug.Log("Nenek Capek! (Playing Cough SFX)");
        }
    }

    private void RecoverStamina()
    {
        // Kita reset stamina, TAPI jangan reset status 'isExhausted' kalau batuk belum selesai
        // (Opsional: atau biarkan pemain bisa jalan lagi setelah lepas tombol)
        walkTimer = 0f;
        isExhausted = false;
    }

    // --- Helper Cek Nama Clip (Biar Aman) ---
    private bool IsPlayingClip(string clipNamePart)
    {
        if (AudioManager.Instance.sfxSource.clip != null)
        {
            // Cek apakah nama file audionya mengandung kata tersebut (misal: "SFX_Cough" mengandung "Cough")
            return AudioManager.Instance.sfxSource.clip.name.Contains(clipNamePart);
        }
        return false;
    }
}