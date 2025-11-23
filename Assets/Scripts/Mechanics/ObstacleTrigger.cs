using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            // Pastikan player sedang di fase Anak (ChildState)
            if (player.CurrentState is ChildState childState)
            {
                // Panggil fungsi jatuh di script ChildState
                childState.TriggerTrip();
                
                // Opsional: Matikan obstacle biar tidak kena trigger 2x
                GetComponent<Collider2D>().enabled = false; 
            }
        }
    }
}