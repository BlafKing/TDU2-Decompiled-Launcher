using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using UpLauncher.Properties;

namespace UpLauncher;

public class MainForm : Form
{
	private enum Enum3
	{
		const_0,
		const_1,
		const_2
	}

	private IContainer icontainer_0;

	private Label label1;

	private Button button1;

	private Panel panel1;

	private Label label6;

	private Label label5;

	private MenuStrip mnMainMenu;

	private ToolStripMenuItem menuToolStripMenuItem;

	private ToolStripMenuItem configToolStripMenuItem;

	private ToolStripMenuItem exitToolStripMenuItem;

	private ImageList imageList_0;

	private ToolStripMenuItem xToolStripMenuItem;

	private ToolStripMenuItem minimizetoolStripMenuItem;

	private ToolStripSeparator toolStripSeparator1;

	private Panel pnMain;

	private NatResolver.eStatus eStatus_0;

	private DateTime dateTime_0;

	private DateTime dateTime_1;

	private DateTime dateTime_2;

	private bool bool_0;

	private Point point_0;

	private bool bool_1;

    private Timer processCheckTimer;

    private ProgressBar progressBar_0;
    private IContainer components;
    private ProgressBar progressbar1;
    public static MainForm MainInstance;

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_0 != null)
		{
			icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.imageList_0 = new System.Windows.Forms.ImageList(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mnMainMenu = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnMain = new System.Windows.Forms.Panel();
            this.progressbar1 = new System.Windows.Forms.ProgressBar();
            this.mnMainMenu.SuspendLayout();
            this.pnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 397);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "GameProductVersion";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.ImageList = this.imageList_0;
            this.button1.Location = new System.Drawing.Point(137, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 38);
            this.button1.TabIndex = 7;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // imageList_0
            // 
            this.imageList_0.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList_0.ImageSize = new System.Drawing.Size(132, 33);
            this.imageList_0.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(210, 397);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(201, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "UpLauncherVersion";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(326, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(275, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "ServersStatus";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackgroundImage = global::UpLauncher.Properties.Resources.MainImg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(414, 315);
            this.panel1.TabIndex = 11;
            // 
            // mnMainMenu
            // 
            this.mnMainMenu.BackColor = System.Drawing.Color.Black;
            this.mnMainMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mnMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.xToolStripMenuItem,
            this.minimizetoolStripMenuItem});
            this.mnMainMenu.Location = new System.Drawing.Point(0, 0);
            this.mnMainMenu.Name = "mnMainMenu";
            this.mnMainMenu.Size = new System.Drawing.Size(414, 29);
            this.mnMainMenu.TabIndex = 18;
            this.mnMainMenu.Text = "menuStrip1";
            this.mnMainMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mnMainMenu_MouseDown);
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.menuToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.ForeColor = System.Drawing.Color.DarkOrange;
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 25);
            this.menuToolStripMenuItem.Text = "Menu";
            this.menuToolStripMenuItem.MouseEnter += new System.EventHandler(this.minimizetoolStripMenuItem_MouseEnter);
            this.menuToolStripMenuItem.MouseLeave += new System.EventHandler(this.minimizetoolStripMenuItem_MouseLeave);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.configToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.configToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.configToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.configToolStripMenuItem.ForeColor = System.Drawing.Color.DarkOrange;
            this.configToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.configToolStripMenuItem.Text = "Config";
            this.configToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            this.configToolStripMenuItem.MouseEnter += new System.EventHandler(this.minimizetoolStripMenuItem_MouseEnter);
            this.configToolStripMenuItem.MouseLeave += new System.EventHandler(this.minimizetoolStripMenuItem_MouseLeave);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(107, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.exitToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.DarkOrange;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.exitToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            this.exitToolStripMenuItem.MouseEnter += new System.EventHandler(this.minimizetoolStripMenuItem_MouseEnter);
            this.exitToolStripMenuItem.MouseLeave += new System.EventHandler(this.minimizetoolStripMenuItem_MouseLeave);
            // 
            // xToolStripMenuItem
            // 
            this.xToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.xToolStripMenuItem.ForeColor = System.Drawing.Color.DarkOrange;
            this.xToolStripMenuItem.Name = "xToolStripMenuItem";
            this.xToolStripMenuItem.Size = new System.Drawing.Size(30, 25);
            this.xToolStripMenuItem.Text = "X";
            this.xToolStripMenuItem.Click += new System.EventHandler(this.xToolStripMenuItem_Click);
            this.xToolStripMenuItem.MouseEnter += new System.EventHandler(this.minimizetoolStripMenuItem_MouseEnter);
            this.xToolStripMenuItem.MouseLeave += new System.EventHandler(this.minimizetoolStripMenuItem_MouseLeave);
            // 
            // minimizetoolStripMenuItem
            // 
            this.minimizetoolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.minimizetoolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.minimizetoolStripMenuItem.ForeColor = System.Drawing.Color.DarkOrange;
            this.minimizetoolStripMenuItem.Name = "minimizetoolStripMenuItem";
            this.minimizetoolStripMenuItem.Size = new System.Drawing.Size(28, 25);
            this.minimizetoolStripMenuItem.Text = "-";
            this.minimizetoolStripMenuItem.Click += new System.EventHandler(this.minimizetoolStripMenuItem_Click);
            this.minimizetoolStripMenuItem.MouseEnter += new System.EventHandler(this.minimizetoolStripMenuItem_MouseEnter);
            this.minimizetoolStripMenuItem.MouseLeave += new System.EventHandler(this.minimizetoolStripMenuItem_MouseLeave);
            // 
            // pnMain
            // 
            this.pnMain.BackColor = System.Drawing.Color.Black;
            this.pnMain.Controls.Add(this.mnMainMenu);
            this.pnMain.Controls.Add(this.progressbar1);
            this.pnMain.Controls.Add(this.button1);
            this.pnMain.Controls.Add(this.panel1);
            this.pnMain.Controls.Add(this.label1);
            this.pnMain.Controls.Add(this.label6);
            this.pnMain.Location = new System.Drawing.Point(1, 1);
            this.pnMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(414, 419);
            this.pnMain.TabIndex = 20;
            // 
            // progressbar1
            // 
            this.progressbar1.Location = new System.Drawing.Point(0, 0);
            this.progressbar1.Name = "progressbar1";
            this.progressbar1.Size = new System.Drawing.Size(100, 23);
            this.progressbar1.TabIndex = 19;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(416, 421);
            this.Controls.Add(this.pnMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.mnMainMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.mnMainMenu.ResumeLayout(false);
            this.mnMainMenu.PerformLayout();
            this.pnMain.ResumeLayout(false);
            this.pnMain.PerformLayout();
            this.ResumeLayout(false);

	}

	public MainForm()
	{
		MainInstance = this;
		InitializeComponent();
        MainInstance.Opacity = 0;
        progressBar_0 = new CustomProgressBar(progressbar1);
		progressbar1.Visible = false;
		pnMain.Controls.Add(progressBar_0);
		ToolStripManager.Renderer = new ToolStripProfessionalRenderer(new CustomProfressionalColors());
		menuToolStripMenuItem.DropDownOpening += menuToolStripMenuItem_DropDownOpening;
		menuToolStripMenuItem.DropDownClosed += menuToolStripMenuItem_DropDownClosed;
		base.Icon = Resources.game;
		imageList_0.Images.Add(Resources.ButtonPlayIn);
		imageList_0.Images.Add(Resources.ButtonPlayOut);
		imageList_0.Images.Add(Resources.ButtonPlayOk);
		Text = "TDU2 Launcher & Updater";
		label1.Text = Class32.string_20 + " build " + Class32.string_21;
		label5.Text = "";
		label6.Text = "Patched Offline Launcher"; //Class32.string_23
        menuToolStripMenuItem.Text = Class32.smethod_13("Menu");
		configToolStripMenuItem.Text = Class32.smethod_13("ConfigMenu") + "...";
		exitToolStripMenuItem.Text = Class32.smethod_13("ExitMenu");
		button1.ImageIndex = 1;
		progressBar_0.Style = ProgressBarStyle.Continuous;
		progressBar_0.Value = 0;
        bool_0 = true;
        button1.Visible = true;
        button1.Click += button1_Click;
        button1.MouseLeave += button1_MouseLeave;
        button1.MouseHover += button1_MouseHover;
        button1.MouseEnter += button1_MouseEnter;
        Point mousePosition = Control.MousePosition;
        mousePosition = button1.PointToClient(mousePosition);
        if (button1.ClientRectangle.Contains(mousePosition))
        {
            button1.ImageIndex = 2;
        }
        else
        {
            button1.ImageIndex = 0;
        }
        dateTime_1 = (dateTime_0 = DateTime.Now);
		dateTime_2 = DateTime.MinValue;
		point_0 = default(Point);
		bool_1 = false;
        MainInstance.Shown += MainForm_Shown;
    }

    private void MainForm_Shown(object sender, EventArgs e)
    {
        Timer timer = new Timer();
        timer.Interval = 500;

        timer.Tick += (s, args) =>
        {
            timer.Stop();
            timer.Dispose();
            button1_Click(null, EventArgs.Empty);
        };
        timer.Start();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Class33.smethod_0().LaunchGame();

        processCheckTimer = new Timer();
        processCheckTimer.Interval = 500;
        processCheckTimer.Tick += ProcessCheckTimer_Tick;
        processCheckTimer.Start();
    }

    private void ProcessCheckTimer_Tick(object sender, EventArgs e)
    {
        var processes = Process.GetProcessesByName("TestDrive2");
        foreach (var process in processes)
        {
            if (process.MainWindowHandle != IntPtr.Zero)
            {
                processCheckTimer.Stop();
                processCheckTimer.Tick -= ProcessCheckTimer_Tick;
                Application.Exit();
                return;
            }
        }
    }

    private void button1_MouseEnter(object sender, EventArgs e)
	{
		if (bool_0)
		{
			button1.ImageIndex = 2;
		}
		else
		{
			button1.ImageIndex = 1;
		}
	}

	private void button1_MouseLeave(object sender, EventArgs e)
	{
		if (bool_0)
		{
			button1.ImageIndex = 0;
		}
		else
		{
			button1.ImageIndex = 1;
		}
	}

	private void button1_MouseHover(object sender, EventArgs e)
	{
		if (bool_0)
		{
			button1.ImageIndex = 2;
		}
		else
		{
			button1.ImageIndex = 1;
		}
	}

	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason == CloseReason.UserClosing && (Class33.smethod_0().State == ClientState.StateRelaunch || Class33.smethod_0().State == ClientState.StateClearCache || Class33.smethod_0().State == ClientState.StateLaunchGame))
		{
			e.Cancel = true;
		}
	}

	private void exitToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Application.Exit();
	}

	private void configToolStripMenuItem_Click(object sender, EventArgs e)
	{
		configToolStripMenuItem.Enabled = false;
		ConfigForm configForm = new ConfigForm();
		configForm.SelectedAudioMode = Class32.audioMode_0;
		configForm.ConfigSaved += method_0;
		configForm.ShowDialog(this);
		configToolStripMenuItem.Enabled = true;
		configToolStripMenuItem.ForeColor = Color.DarkOrange;
	}

	private void method_0(AudioMode audioMode_0)
	{
		Class32.smethod_16(audioMode_0);
	}

	private void xToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Application.Exit();
	}

	private void minimizetoolStripMenuItem_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void minimizetoolStripMenuItem_MouseEnter(object sender, EventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
		if (toolStripMenuItem != menuToolStripMenuItem || !toolStripMenuItem.DropDown.Visible)
		{
			toolStripMenuItem.ForeColor = Color.Black;
		}
	}

	private void minimizetoolStripMenuItem_MouseLeave(object sender, EventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
		if (toolStripMenuItem != menuToolStripMenuItem || !toolStripMenuItem.DropDown.Visible)
		{
			toolStripMenuItem.ForeColor = Color.DarkOrange;
		}
	}

	private void menuToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
	{
		(sender as ToolStripMenuItem).ForeColor = Color.Black;
	}

	private void menuToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
	{
		(sender as ToolStripMenuItem).ForeColor = Color.DarkOrange;
	}

	private void mnMainMenu_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			bool_1 = true;
			base.Capture = true;
			point_0 = e.Location;
		}
	}

	private void MainForm_MouseUp(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left && bool_1)
		{
			bool_1 = false;
			base.Capture = false;
		}
	}

	private void MainForm_MouseMove(object sender, MouseEventArgs e)
	{
		if (bool_1 && base.Capture)
		{
			int num = e.Location.X - point_0.X;
			int num2 = e.Location.Y - point_0.Y;
			base.Location = new Point(base.Left + num, base.Top + num2);
		}
	}
}
