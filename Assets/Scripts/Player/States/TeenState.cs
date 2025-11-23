using System.Collections;
using UnityEngine;

public class TeenState : PlayerBaseState
{
    private bool isShocked = false;

    public TeenState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 3: Remaja");
        player.transform.localScale = new Vector3(1, 1, 1);
        if (player.SpriteRend != null) player.SpriteRend.color = Color.white;
        isShocked = false;
    }

    public override void FixedUpdate()
    {
        // 1. KONDISI KAGET
        if (isShocked)
        {
            player.Rb.linearVelocity = Vector2.zero;
            return; 
        }

        // 2. LOGIKA NORMAL
        if (player.InputActions.Gameplay.MainAction.IsPressed())
        {
            // Input DITAHAN -> Berhenti
            player.Rb.linearVelocity = Vector2.zero;
            player.UpdateVisuals(player.TeenIdleSprite);
            
            // Stop SFX Jalan jika berhenti
            if (AudioManager.Instance.sfxSource.isPlaying)
            {
                 AudioManager.Instance.sfxSource.Stop();
            }
        }
        else
        {
            // Input DILEPAS -> Jalan
            player.Rb.linearVelocity = new Vector2(player.RunSpeed * 0.8f, player.Rb.linearVelocity.y);
            
            player.UpdateVisuals(null, "Young-Walk");
            
            // EFFICIENT SFX LOOP
            if (!AudioManager.Instance.sfxSource.isPlaying)
            {
                AudioManager.Instance.PlaySFX("Walk");
            }
        }
    }

    public void TriggerShock()
    {
        if (isShocked) return;
        player.StartCoroutine(ShockRoutine());
    }

    private IEnumerator ShockRoutine()
    {
        isShocked = true;
        
        Debug.Log("Shock!");
        AudioManager.Instance.sfxSource.Stop(); // Stop suara jalan
        AudioManager.Instance.PlaySFX("Honk");
        
        player.Rb.linearVelocity = Vector2.zero;
        player.Rb.AddForce(Vector2.left * 2f, ForceMode2D.Impulse); 
        
        player.UpdateVisuals(player.TeenShockSprite);

        yield return new WaitForSeconds(1f); 

        isShocked = false;
    }

    public bool IsStopped()
    {
        return player.InputActions.Gameplay.MainAction.IsPressed();
    }
}