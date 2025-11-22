using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerController player;

    // Konstruktor untuk menghubungkan state dengan player
    public PlayerBaseState(PlayerController player)
    {
        this.player = player;
    }

    // Dipanggil sekali saat masuk ke state ini
    public virtual void Enter() { }

    // Dipanggil setiap frame (untuk Input & Logika)
    public virtual void Update() { }

    // Dipanggil setiap physics frame (untuk pergerakan Rigidbody)
    public virtual void FixedUpdate() { }

    // Dipanggil sekali saat keluar dari state ini
    public virtual void Exit() { }
}