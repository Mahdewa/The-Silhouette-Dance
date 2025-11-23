using UnityEngine;
using UnityEngine.UI; // Wajib ada untuk UI

public class TutorialDisplay : MonoBehaviour
{
    [Header("References")]
    public PlayerController player; // Drag Player ke sini
    public Image tutorialImage;     // Drag UI Image (Source Image) ke sini

    [Header("Tutorial Sprites")]
    public Sprite iconTapCrawl;
    public Sprite iconTapJump;
    public Sprite iconHoldStop;
    public Sprite iconTapWork;
    public Sprite iconHoldWalk;

    private void Update()
    {
        // Pengecekan keamanan: Jika player belum di-drag, jangan jalankan error
        if (player == null || tutorialImage == null) return;

        PlayerBaseState currentState = player.CurrentState;

        // --- LOGIKA ---
        
        // 1. Cek Bayi
        if (currentState is BabyState)
        {
            ShowGuide(iconTapCrawl);
        }
        // 2. Cek Anak
        else if (currentState is ChildState)
        {
            ShowGuide(iconTapJump);
        }
        // 3. Cek Remaja
        else if (currentState is TeenState)
        {
            ShowGuide(iconHoldStop);
        }
        // 4. Cek Dewasa
        else if (currentState is AdultState)
        {
            // Kita perlu casting (mengubah tipe data) untuk akses variabel khusus AdultState
            AdultState adult = (AdultState)currentState;
            
            if (adult.isWorking)
            {
                ShowGuide(iconTapWork);
            }
            else
            {
                HideGuide();
            }
        }
        // 5. Cek Lansia
        else if (currentState is ElderlyState)
        {
            ShowGuide(iconHoldWalk);
        }
        // 6. Cek Bumil
        else if (currentState is PregnantState)
        {
            PregnantState pregnant = (PregnantState)currentState;

            if (pregnant.isMourning)
            {
                HideGuide();
            }
            else
            {
                ShowGuide(iconHoldWalk);
            }
        }
    }

    private void ShowGuide(Sprite icon)
    {
        if (icon != null)
        {
            tutorialImage.sprite = icon;
            tutorialImage.enabled = true;
        }
        else
        {
            HideGuide();
        }
    }

    private void HideGuide()
    {
        if(tutorialImage != null)
            tutorialImage.enabled = false;
    }
}