using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // --- COMPONENTS ---
    [Header("Components")]
    public Rigidbody2D Rb;
    public Animator Anim;
    public SpriteRenderer SpriteRend;

    // --- INPUT SYSTEM ---
    public GameControls InputActions;

    // --- STATE MACHINE ---
    public PlayerBaseState CurrentState;
    
    public BabyState StateBaby;
    public ChildState StateChild;
    public TeenState StateTeen;
    public AdultState StateAdult;
    public ElderlyState StateElderly;
    public PregnantState StatePregnant;

    // --- SETTINGS ---
    [Header("Movement Settings")]
    public float CrawlForce = 5f;
    public float RunSpeed = 4f;
    public float JumpForce = 7f;

    // --- VISUAL SETTINGS (SPRITES) ---
    [Header("Visual Assets - Sprites")]
    // 1. Idle Sprites per State
    public Sprite BabyIdleSprite;
    public Sprite ChildIdleSprite;
    public Sprite TeenIdleSprite;
    public Sprite AdultIdleSprite;
    public Sprite ElderlyIdleSprite;
    public Sprite PregnantIdleSprite;

    // 2. Event Specific Sprites
    public Sprite ChildJumpSprite;
    public Sprite ChildFallSprite; // Opsional jika nanti ada logika jatuh
    public Sprite TeenShockSprite;
    public Sprite AdultWorkSprite;
    public Sprite PregnantMournSprite;

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

    private void OnEnable() => InputActions.Enable();
    private void OnDisable() => InputActions.Disable();

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

    public void SwitchState(PlayerBaseState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    // --- HELPER UNTUK VISUAL ---
    // Fungsi ini pintar: kalau kita kasih Sprite, dia matikan Animator.
    // Kalau kita kasih nama Animasi, dia nyalakan Animator.
    public void UpdateVisuals(Sprite spriteToUse = null, string animName = "")
    {
        if (spriteToUse != null)
        {
            // Mode Sprite Statis
            Anim.enabled = false; // Matikan animator biar sprite gak ketimpa
            SpriteRend.sprite = spriteToUse;
        }
        else if (!string.IsNullOrEmpty(animName))
        {
            // Mode Animasi
            Anim.enabled = true;
            // Cek biar gak play animasi yang sama berulang-ulang (reset frame)
            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName(animName))
            {
                Anim.Play(animName);
            }
        }
    }
}