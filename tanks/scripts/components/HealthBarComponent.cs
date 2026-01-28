using Godot;


[Tool]
public partial class HealthBarComponent : Node2D
{
    [Export]
    public HealthComponent healthComponent;

    private int width = 100;
    [Export]
    public int Width
    {
        get => width;
        set
        {
            width = value;
            QueueRedraw();
        }
    }

    private int height = 10;
    [Export]
    public int Height
    {
        get => height;
        set
        {
            height = value;
            QueueRedraw();
        }
    }

    [Export]
    public bool ShowHealthText { get; set; } = true;


    public override void _Ready()
    {
        if (healthComponent == null)
        {
            GD.PrintErr("HealthBarComponent: healthComponent is not assigned.");
            return;
        }

        healthComponent.HealthChanged += OnHealthChanged;
        QueueRedraw();
    }

    public override void _Draw()
    {
        if (healthComponent == null)
            return;

        float healthPercent = (float)healthComponent.currentHealth / healthComponent.MaxHealth;
        Color bgColor = new Color(0.2f, 0.2f, 0.2f);
        Color fgColor = new Color(1f, 0.0f, 0.0f);

        DrawRect(new Rect2(0, 0, Width, Height), bgColor);
        DrawRect(new Rect2(0, 0, Width * healthPercent, Height), fgColor);

        // if (ShowHealthText)
        // {
        //     string healthText = $"{healthComponent.currentHealth} / {healthComponent.MaxHealth}";
        //     Vector2 textSize = (new Label()).GetThemeFont("").GetStringSize(healthText);
        //     Vector2 textPosition = new Vector2((Width - textSize.X) / 2, (Height - textSize.Y) / 2);
        //     DrawString((new Label()).GetThemeFont(""), textPosition + new Vector2(0, textSize.Y), healthText);
        // }
    }

    private void OnHealthChanged(int currentHealth, int maxHealth)
    {
        QueueRedraw();
    }
}
