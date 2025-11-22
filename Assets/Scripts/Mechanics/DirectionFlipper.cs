using UnityEngine;
public class DirectionFlipper : MonoBehaviour
{
    [Header("Exit Trigger Settings")]
    public GameObject exitTriggerToEnable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.CurrentState is AdultState adultState)
            {
                adultState.FlipDirection(); // Panggil fungsi putar balik
                
                if (exitTriggerToEnable != null)
                {
                    exitTriggerToEnable.SetActive(true);
                    Debug.Log("Pintu Keluar Kantor sekarang Terbuka!");
                }

                gameObject.SetActive(false); // Matikan trigger setelah dipakai
            }
        }
    }
}