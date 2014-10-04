using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class EditorControls : GameComponent
{
    public EditorControls(Game game) : base(game) { }

    Form windowsGameForm;
	IGraphicsDeviceService graphicsService;
	GraphicsDevice graphics;

    public override void Initialize()
    {
        graphicsService = Game.Services.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
        graphics = graphicsService.GraphicsDevice;

        windowsGameForm = Control.FromHandle(Game.Window.Handle) as Form;
        InitializeComponent();
    }

    Panel RenderPanel;
    MenuStrip MainMenu;

    void InitializeComponent()
    {
        MainMenu = new MenuStrip();
        RenderPanel = new Panel();
        MainMenu.SuspendLayout();
        windowsGameForm.SuspendLayout();
        MainMenu.Location = new System.Drawing.Point(0, 0);
        MainMenu.Name = "MainMenu";
        MainMenu.Size = new Size(741, 24);
        MainMenu.TabIndex = 0;
        RenderPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        RenderPanel.Location = new System.Drawing.Point(0, 49);
        RenderPanel.Name = "RenderPanel";
        RenderPanel.Size = new Size(800, 600);
        RenderPanel.TabIndex = 2;
        windowsGameForm.Controls.Add(MainMenu);
        windowsGameForm.Controls.Add(RenderPanel);
        MainMenu.ResumeLayout(false);
        MainMenu.PerformLayout();
        windowsGameForm.ResumeLayout(false);
        windowsGameForm.PerformLayout();
        graphicsService.DeviceResetting += new EventHandler(OnDeviceReset);
        graphicsService.DeviceCreated += new EventHandler(OnDeviceCreated);
        graphics.Reset();
    }

	void OnDeviceCreated(object sender, EventArgs e)
	{
	    graphics = graphicsService.GraphicsDevice;
	    graphics.Reset();
	}

	void OnDeviceReset(object sender, EventArgs e)
	{
	    graphics.PresentationParameters.DeviceWindowHandle = RenderPanel.Handle;
	    graphics.PresentationParameters.BackBufferWidth = RenderPanel.Width;
	    graphics.PresentationParameters.BackBufferHeight = RenderPanel.Height;
	}

}
