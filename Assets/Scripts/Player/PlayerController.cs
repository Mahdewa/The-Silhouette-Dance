using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // --- COMPONENTS ---
    [Header("Components")]
    public Rigidbody2D Rb;
    public Animator Anim;
    
    public SpriteRenderer SpriteRend;

    // --- INPUT SYSTEM ---
    public GameControls InputActions; // Class yang kita generate tadi

    // --- STATE MACHINE ---
    public PlayerBaseState CurrentState;
    
    // Instance dari setiap State
    public BabyState StateBaby;
    public ChildState StateChild;
    public TeenState StateTeen;
    public AdultState StateAdult;
    public ElderlyState StateElderly;
    public PregnantState StatePregnant;

    // --- SETTINGS (Bisa disesuaikan di Inspector) ---
    [Header("Movement Settings")]
    public float CrawlForce = 5f; // Tenaga bayi merangkak 
    public float RunSpeed = 4f;   // Kecepatan lari anak 
    public float JumpForce = 7f;  // Kekuatan lompat 

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        InputActions = new GameControls();
        SpriteRend = GetComponent<SpriteRenderer>();

        // Inisialisasi States
        StateBaby = new BabyState(this);
        StateChild = new ChildState(this);
        StateTeen = new TeenState(this);
        StateAdult = new AdultState(this);
        StateElderly = new ElderlyState(this);
        StatePregnant = new PregnantState(this);
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    private void Start()
    {
        SwitchState(StateBaby);
    }

    private void Update()
    {
        CurrentState.Update();
    }

    private void FixedUpdate()
    {
        CurrentState.FixedUpdate();
    }

    // Fungsi untuk ganti State
    public void SwitchState(PlayerBaseState newState)
    {
        CurrentState?.Exit(); // Keluar dari state lama
        CurrentState = newState;
        CurrentState.Enter(); // Masuk ke state baru
    }
}