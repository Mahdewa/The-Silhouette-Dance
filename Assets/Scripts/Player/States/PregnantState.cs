using System.Collections;
using UnityEngine;

public class PregnantState : PlayerBaseState
{
    public bool isMourning = false;
    private bool canMove = true;

    public PregnantState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Entering Stage 6: Epilog (Bumil)");
        player.transform.localScale = new Vector3(1, 1, 1);
        canMove = true;
        isMourning = false;
        if (player.SpriteRend != null) player.SpriteRend.color = Color.white;
    }

    public override void FixedUpdate()
    {
        if (isMourning) return; 

        // LOGIKA GERAK
        if (canMove && player.InputActions.Gameplay.MainAction.IsPressed())
        {
            player.Rb.linearVelocity = new Vector2(player.RunSpeed * 0.4f, player.Rb.linearVelocity.y);
            
            player.UpdateVisuals(null, "Bumil-walk");
            
            // EFFICIENT SFX LOOP
            if (!AudioManager.Instance.sfxSource.isPlaying)
            {
                AudioManager.Instance.PlaySFX("Walk");
            }
        }
        else
        {
            player.Rb.linearVelocity = Vector2.zero;
            player.UpdateVisuals(player.PregnantIdleSprite);
            
            // Stop suara kalau berhenti
            if (AudioManager.Instance.sfxSource.isPlaying) AudioManager.Instance.sfxSource.Stop();
        }
    }

    public void StartMourning(float duration)
    {
        player.StartCoroutine(MourningRoutine(duration));
    }

    private IEnumerator MourningRoutine(float duration)
    {
        isMourning = true;
        canMove = false;
        player.Rb.linearVelocity = Vector2.zero;
        
        // Stop audio jalan saat mulai berdoa
        AudioManager.Instance.sfxSource.Stop();
        
        player.UpdateVisuals(player.PregnantMournSprite);

        yield return new WaitForSeconds(duration);

        isMourning = false;
        canMove = true;
    }
}