using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using StunLib.Lib;
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

	private ProgressBar progressbar1;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private Timer timer_0;

	private Button button1;

	private WebBrowser webBrowser1;

	private Panel panel1;

	private Label label6;

	private Label label5;

	private PictureBox pictureBox1;

	private PictureBox pictureBox2;

	private Label label7;

	private MenuStrip mnMainMenu;

	private ToolStripMenuItem menuToolStripMenuItem;

	private ToolStripMenuItem configToolStripMenuItem;

	private ToolStripMenuItem exitToolStripMenuItem;

	private ImageList imageList_0;

	private Label label8;

	private PictureBox pictureBoxSteam;

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

	private ProgressBar progressBar_0;
    private IContainer components;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.progressbar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer_0 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.imageList_0 = new System.Windows.Forms.ImageList(this.components);
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxSteam = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mnMainMenu = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label8 = new System.Windows.Forms.Label();
            this.pnMain = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSteam)).BeginInit();
            this.mnMainMenu.SuspendLayout();
            this.pnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressbar1
            // 
            this.progressbar1.BackColor = System.Drawing.Color.Black;
            this.progressbar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.progressbar1.Location = new System.Drawing.Point(12, 382);
            this.progressbar1.Name = "progressbar1";
            this.progressbar1.Size = new System.Drawing.Size(611, 16);
            this.progressbar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressbar1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 444);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "GameProductVersion";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 360);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(307, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "CurrentFileName";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(326, 360);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(297, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "BytesPercentage";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(365, 401);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(258, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Bandwidth";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer_0
            // 
            this.timer_0.Tick += new System.EventHandler(this.timer_0_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.ImageList = this.imageList_0;
            this.button1.Location = new System.Drawing.Point(249, 419);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 38);
            this.button1.TabIndex = 7;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // imageList_0
            // 
            this.imageList_0.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList_0.ImageSize = new System.Drawing.Size(132, 33);
            this.imageList_0.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowNavigation = false;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(323, 37);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(300, 278);
            this.webBrowser1.TabIndex = 10;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser1_NewWindow);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(422, 442);
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
            // pictureBox2
            // 
            this.pictureBox2.Image = global::UpLauncher.Properties.Resources.green;
            this.pictureBox2.Location = new System.Drawing.Point(12, 334);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::UpLauncher.Properties.Resources.green;
            this.pictureBox1.Location = new System.Drawing.Point(607, 334);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::UpLauncher.Properties.Resources.MainImg;
            this.panel1.Location = new System.Drawing.Point(12, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 279);
            this.panel1.TabIndex = 11;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            // 
            // pictureBoxSteam
            // 
            this.pictureBoxSteam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxSteam.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSteam.Image")));
            this.pictureBoxSteam.Location = new System.Drawing.Point(425, 411);
            this.pictureBoxSteam.Name = "pictureBoxSteam";
            this.pictureBoxSteam.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxSteam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSteam.TabIndex = 0;
            this.pictureBoxSteam.TabStop = false;
            this.pictureBoxSteam.Visible = false;
            this.pictureBoxSteam.Click += new System.EventHandler(this.pictureBoxSteam_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(34, 334);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(306, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "NetworkNatType";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.mnMainMenu.Size = new System.Drawing.Size(633, 29);
            this.mnMainMenu.TabIndex = 18;
            this.mnMainMenu.Text = "menuStrip1";
            this.mnMainMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mnMainMenu_MouseDown);
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.BackColor = System.Drawing.Color.Black;
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(12, 402);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Estimated end time";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label8.Visible = false;
            // 
            // pnMain
            // 
            this.pnMain.BackColor = System.Drawing.Color.Black;
            this.pnMain.Controls.Add(this.mnMainMenu);
            this.pnMain.Controls.Add(this.progressbar1);
            this.pnMain.Controls.Add(this.label4);
            this.pnMain.Controls.Add(this.button1);
            this.pnMain.Controls.Add(this.webBrowser1);
            this.pnMain.Controls.Add(this.pictureBoxSteam);
            this.pnMain.Controls.Add(this.panel1);
            this.pnMain.Controls.Add(this.label3);
            this.pnMain.Controls.Add(this.label1);
            this.pnMain.Controls.Add(this.label2);
            this.pnMain.Controls.Add(this.label6);
            this.pnMain.Controls.Add(this.label5);
            this.pnMain.Controls.Add(this.pictureBox1);
            this.pnMain.Controls.Add(this.pictureBox2);
            this.pnMain.Controls.Add(this.label7);
            this.pnMain.Controls.Add(this.label8);
            this.pnMain.Location = new System.Drawing.Point(1, 1);
            this.pnMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(633, 465);
            this.pnMain.TabIndex = 20;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(635, 467);
            this.Controls.Add(this.pnMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.mnMainMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSteam)).EndInit();
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
		label2.Text = "";
		label3.Text = "";
		label4.Text = "";
		label5.Text = "";
		label6.Text = Class32.string_23;
		label7.Text = Class32.smethod_13("CheckingNetwork");
		menuToolStripMenuItem.Text = Class32.smethod_13("Menu");
		configToolStripMenuItem.Text = Class32.smethod_13("ConfigMenu") + "...";
		exitToolStripMenuItem.Text = Class32.smethod_13("ExitMenu");
		webBrowser1.Visible = false;
		button1.ImageIndex = 1;
		progressBar_0.Style = ProgressBarStyle.Continuous;
		progressBar_0.Value = 0;
		try
		{
			string uriString = "http://www.testdriveunlimited2.com/launchernews/" + Class32.string_25;
			webBrowser1.Url = new Uri(uriString, UriKind.Absolute);
		}
		catch (Exception arg)
		{
			Class25.smethod_3($"Exception: {arg}");
		}
		bool_0 = false;
		timer_0.Interval = 200;
		timer_0.Start();
		dateTime_1 = (dateTime_0 = DateTime.Now);
		dateTime_2 = DateTime.MinValue;
		point_0 = default(Point);
		bool_1 = false;
		Class32.keyChecker_0.Begin();
	}

	private void timer_0_Tick(object sender, EventArgs e)
	{
		DateTime now = DateTime.Now;
		label1.Text = Class32.string_20 + " build " + Class32.string_21;
		label2.Text = "";
		label3.Text = "";
		label4.Text = "";
		label6.Text = Class32.string_23;
		label8.Text = "";
		pictureBoxSteam.Visible = Class32.bool_1;
		if ((now - dateTime_2).TotalMinutes >= 5.0)
		{
			Class32.gameServersStatus_0.Begin();
			dateTime_2 = now;
		}
		string text = "";
		if (Class32.gameServersStatus_0.Status == GameServersStatus.eStatus.Ok)
		{
			if (Class32.gameServersStatus_0.ServersAreAvailable)
			{
				text = Class32.smethod_13("GameServersStatusOk");
				label5.ForeColor = Color.Green;
				pictureBox1.Visible = true;
				pictureBox1.Image = Resources.green;
			}
			else if (!Class32.gameServersStatus_0.ServersAreAvailable)
			{
				text = Class32.smethod_13("GameServersStatusNOk");
				label5.ForeColor = Color.Red;
				pictureBox1.Visible = true;
				pictureBox1.Image = Resources.red;
			}
		}
		label5.Text = text;
		if (Class32.natResolver_0.Status != eStatus_0)
		{
			label7.Visible = true;
			pictureBox2.Visible = true;
			string text2 = "";
			switch (Class32.natResolver_0.NatType)
			{
			case StunNetType.SymmetricUdpFirewall:
			case StunNetType.RestrictedCone:
			case StunNetType.PortRestrictedCone:
				label7.ForeColor = Color.Orange;
				pictureBox2.Image = Resources.orange;
				label7.MouseLeave += label7_MouseLeave;
				label7.Click += label7_Click;
				label7.MouseEnter += label7_MouseEnter;
				text2 = "Moderate:";
				break;
			case StunNetType.UdpBlocked:
			case StunNetType.Symmetric:
				label7.ForeColor = Color.Red;
				pictureBox2.Image = Resources.red;
				label7.MouseLeave += label7_MouseLeave;
				label7.Click += label7_Click;
				label7.MouseEnter += label7_MouseEnter;
				text2 = "Strict:";
				break;
			case StunNetType.OpenInternet:
			case StunNetType.FullCone:
			case StunNetType.UPnPUsed:
				label7.ForeColor = Color.Green;
				pictureBox2.Image = Resources.green;
				text2 = "Open:";
				break;
			}
			text2 += Class32.natResolver_0.NatType;
			if (Class32.natResolver_0.NatType != 0)
			{
				Class32.smethod_6("NetworkNatType", text2);
			}
			label7.Text = Class32.smethod_13("NatType") + text2;
			eStatus_0 = Class32.natResolver_0.Status;
			Class32.statistics_0.Begin();
		}
		if (Class33.smethod_0().State == ClientState.StateFinished)
		{
			timer_0.Stop();
			Close();
			return;
		}
		if (Class33.smethod_0().State == ClientState.StateWaitUserLaunchGame)
		{
			if (Class32.natResolver_0.Status == eStatus_0 && Class32.statistics_0.Status != 0)
			{
				if (!bool_0)
				{
					if (Class32.keyChecker_0.Status == KeyChecker.eStatus.Ok && !Class32.keyChecker_0.KeyIsValid)
					{
						timer_0.Stop();
						Class32.smethod_12(ErrorCode.ErrorKeyBanned, ErrorCodeEx.ErrorKeyBanned);
						Close();
						return;
					}
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
					button1.Visible = true;
					bool_0 = true;
				}
			}
			else
			{
				if (Class33.smethod_0().Error != 0)
				{
					Class32.natResolver_0.Stop();
				}
				label2.Text = Class32.smethod_13("Wait");
			}
		}
		else
		{
			if (Class33.smethod_0().State == ClientState.StateError)
			{
				timer_0.Stop();
				switch (Class32.smethod_12(Class33.smethod_0().Error, Class33.smethod_0().ErrorEx))
				{
				case DialogResult.OK:
					Close();
					break;
				case DialogResult.Cancel:
					Class33.smethod_0().SetWaitUserLaunchGame();
					timer_0.Start();
					break;
				case DialogResult.Abort:
					break;
				case DialogResult.Retry:
					Class33.smethod_0().StartUpdate();
					timer_0.Start();
					break;
				}
				return;
			}
			if (Class33.smethod_0().State == ClientState.StateDbUdpate)
			{
				label2.Text = Class32.smethod_13("CheckingFiles");
				return;
			}
			if (Class33.smethod_0().State == ClientState.StateClearCache)
			{
				label2.Text = Class32.smethod_13("Wait");
				return;
			}
			if (Class33.smethod_0().State != ClientState.StateFilesTransfer && Class33.smethod_0().State != ClientState.StateLaunchGame)
			{
				label2.Text = Class32.smethod_13("CheckingFiles");
			}
		}
		Class33.smethod_0().GetProgressInfos(out var a_rGlobalProgressInfo);
		progressBar_0.Value = (int)a_rGlobalProgressInfo.TotalBytesPercentage;
		label3.Text = a_rGlobalProgressInfo.TotalBytesPercentage + "%";
		if (a_rGlobalProgressInfo.TotalBytesToReceive - a_rGlobalProgressInfo.TotalBytesReceived > 0L)
		{
			label2.Text = ((a_rGlobalProgressInfo.CurrentFileName == null) ? Class32.smethod_13("CheckingFiles") : a_rGlobalProgressInfo.CurrentFileName);
			label3.Text = $"{Class32.smethod_14(a_rGlobalProgressInfo.TotalBytesReceived)} / {Class32.smethod_14(a_rGlobalProgressInfo.TotalBytesToReceive)} - {a_rGlobalProgressInfo.TotalBytesPercentage}%";
			progressBar_0.Value = (int)a_rGlobalProgressInfo.TotalBytesPercentage;
			label4.Text = $"{Class32.smethod_14(a_rGlobalProgressInfo.BytesPerSeconds)}/s";
			label8.Text = "estimated end at: " + a_rGlobalProgressInfo.EstimatedEndTime.ToString();
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (bool_0 && (Class33.smethod_0().IsOnlineAllowed || PopupForm.Show(Class32.smethod_13("OfflineMode"), MessageBoxButtons.YesNo) != DialogResult.No))
		{
			button1.Visible = false;
			Class33.smethod_0().LaunchGame();
			Application.DoEvents();
		}
	}

	private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
	{
		webBrowser1.Visible = true;
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

	private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
	{
		e.Cancel = true;
		Process.Start(webBrowser1.StatusText);
	}

	private void panel1_Click(object sender, EventArgs e)
	{
		Process.Start("http://testdriveunlimited2.com");
	}

	private void panel1_MouseEnter(object sender, EventArgs e)
	{
		Cursor = Cursors.Hand;
	}

	private void panel1_MouseLeave(object sender, EventArgs e)
	{
		Cursor = Cursors.Default;
	}

	private void label7_Click(object sender, EventArgs e)
	{
		Process.Start("http://forums.testdriveunlimited2.com/showthread.php?p=212112");
	}

	private void label7_MouseEnter(object sender, EventArgs e)
	{
		Cursor = Cursors.Hand;
	}

	private void label7_MouseLeave(object sender, EventArgs e)
	{
		Cursor = Cursors.Default;
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

	private void pictureBoxSteam_Click(object sender, EventArgs e)
	{
		Process.Start("http://store.steampowered.com/app/9930/");
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
