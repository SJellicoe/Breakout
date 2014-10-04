using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Windows.Forms;
using System.Windows;




/// <summary>
/// Summary description for Class1
/// </summary>
/// 

namespace SET_Breakout
{
    public class EditorControls : GameComponent
    {
        public EditorControls(Game game)
            : base(game)
        {

        }

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
            //  RenderPanel = new Panel();
            MainMenu.SuspendLayout();
            windowsGameForm.SuspendLayout();
            MainMenu.Location = new System.Drawing.Point(0, 0);
            MainMenu.Name = "MainMenu";
            MainMenu.Size = new System.Drawing.Size(741, 24);
            MainMenu.TabIndex = 0;
            /* RenderPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
             RenderPanel.Location = new System.Drawing.Point(0, 49);
             RenderPanel.Name = "RenderPanel";
             RenderPanel.Size = new System.Drawing.Size(800, 600);
             RenderPanel.TabIndex = 2;*/
            windowsGameForm.Controls.Add(MainMenu);
            // windowsGameForm.Controls.Add(RenderPanel);
            MainMenu.ResumeLayout(false);
            MainMenu.PerformLayout();
            windowsGameForm.ResumeLayout(false);
            windowsGameForm.PerformLayout();
            graphicsService.DeviceResetting += new EventHandler<System.EventArgs>(OnDeviceReset);
            graphicsService.DeviceCreated += new EventHandler<System.EventArgs>(OnDeviceCreated);

            /**** File ***/
            var item = new System.Windows.Forms.ToolStripMenuItem("File");
           
            var subitem = new System.Windows.Forms.ToolStripMenuItem("New Game");
            subitem.Click += new EventHandler(OnNewClick);
            item.DropDownItems.Add(subitem);

            subitem = new System.Windows.Forms.ToolStripMenuItem("Pause/Cotinue");
            subitem.Click += new EventHandler(OnPauseClick);
            item.DropDownItems.Add(subitem);

            subitem = new System.Windows.Forms.ToolStripMenuItem("Exit");
            subitem.Click += new EventHandler(OnExitClick);
            item.DropDownItems.Add(subitem);

            MainMenu.Items.Add(item);

            /**** Tools ***/
            item = new System.Windows.Forms.ToolStripMenuItem("Tools");

            subitem = new System.Windows.Forms.ToolStripMenuItem("Difficultly");
            subitem.Click += new EventHandler(OnDiffClick);
           
            item.DropDownItems.Add(subitem);
       

            MainMenu.Items.Add(item);

            /**** Help ***/
            item = new System.Windows.Forms.ToolStripMenuItem("Help");

            subitem = new System.Windows.Forms.ToolStripMenuItem("About");
            subitem.Click += new EventHandler(OnAboutClick);
            item.DropDownItems.Add(subitem);

            subitem = new System.Windows.Forms.ToolStripMenuItem("Controls");
            subitem.Click += new EventHandler(OnControlsClick);
            item.DropDownItems.Add(subitem);

            MainMenu.Items.Add(item);



        }

        public void OnPauseClick(object sender, EventArgs e)
        {
            Game1.paused = !(Game1.paused);
        }

        public void OnDiffClick(object sender, EventArgs e)
        {
            
        }

        public void OnAboutClick(object sender, EventArgs e)
        {
            MessageBox.Show("Created By Greg McCoy and Sean Jellicoe \n 2014 SET Conestoga College");
        }

        public void OnControlsClick(object sender, EventArgs e)
        {
            MessageBox.Show("A, D - Move \n SPACE - Start \n F - Fire (When you have Lasers powerup) \n ESC - Exit");
        }

        public void OnNewClick(object sender, EventArgs e)
        {
            MessageBox.Show("Click The X in the corner then double click the exe");
        }

        public void OnExitClick(object sender, EventArgs e)
        {
            Game.Exit();
        }

        void OnDeviceCreated(object sender, object e)
        {
            graphics = graphicsService.GraphicsDevice;

        }

        void OnDeviceReset(object sender, object e)
        {
            graphics.PresentationParameters.DeviceWindowHandle = RenderPanel.Handle;
            graphics.PresentationParameters.BackBufferWidth = RenderPanel.Width;
            graphics.PresentationParameters.BackBufferHeight = RenderPanel.Height;
        }

    }
}
