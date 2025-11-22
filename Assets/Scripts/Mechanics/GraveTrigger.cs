using UnityEngine;

public class GraveTrigger : MonoBehaviour
{
    public float mournDuration = 3.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            // Panggil fungsi ziarah di PregnantState
            if (player.CurrentState is PregnantState pregnantState)
            {
                pregnantState.StartMourning(mournDuration);
                // Matikan collider agar tidak memicu event berkali-kali
                GetComponent<Collider2D>().enabled = false; 
            }
        }
    }
}