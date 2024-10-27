using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using UpLauncher.Properties;

namespace UpLauncher;

public class ConfigForm : Form
{
	public delegate void ConfigSavedHandler(AudioMode a_AudioMode);

	private ConfigSavedHandler configSavedHandler_0;

	private AudioMode audioMode_0;

	private IContainer icontainer_0;

	private Label lblConfigAudio;

	private RadioButton rdConfigAudioXAudio;

	private RadioButton rdConfigAudioDirectSound;

	private Button btnConfigAudio;

	private MenuStrip menuStrip1;

	private ToolStripMenuItem xToolStripMenuItem;

	private Panel pnConfigForm;

	public AudioMode SelectedAudioMode
	{
		get
		{
			return audioMode_0;
		}
		set
		{
			audioMode_0 = value;
		}
	}

	public event ConfigSavedHandler ConfigSaved
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			configSavedHandler_0 = (ConfigSavedHandler)Delegate.Combine(configSavedHandler_0, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			configSavedHandler_0 = (ConfigSavedHandler)Delegate.Remove(configSavedHandler_0, value);
		}
	}

	public ConfigForm()
	{
		InitializeComponent();
		ToolStripManager.Renderer = new ToolStripProfessionalRenderer(new CustomProfressionalColors());
		base.Icon = Resources.game;
		MinimumSize = base.Size;
		MaximumSize = base.Size;
		pnConfigForm.Width = base.Width - 4;
		pnConfigForm.Height = base.Height - 4;
		pnConfigForm.Left = 2;
		pnConfigForm.Top = 2;
		Text = Class32.smethod_13("ConfigMenu");
		lblConfigAudio.Text = Class32.smethod_13("AudioLabel");
		btnConfigAudio.Text = Class32.smethod_13("SaveConfig");
		btnConfigAudio.AutoSize = true;
		btnConfigAudio.Location = new Point(base.Width - (btnConfigAudio.Size.Width + 30), btnConfigAudio.Location.Y);
		rdConfigAudioDirectSound.Tag = AudioMode.DirectSound;
		rdConfigAudioXAudio.Tag = AudioMode.XAudio2;
		base.Load += ConfigForm_Load;
	}

	private void ConfigForm_Load(object sender, EventArgs e)
	{
		method_0();
	}

	private void btnConfigAudio_Click(object sender, EventArgs e)
	{
		if (configSavedHandler_0 != null)
		{
			configSavedHandler_0(audioMode_0);
		}
		Close();
	}

	private void rdConfigAudioDirectSound_Click(object sender, EventArgs e)
	{
		audioMode_0 = (AudioMode)(sender as RadioButton).Tag;
		method_0();
	}

	private void method_0()
	{
		switch (audioMode_0)
		{
		case AudioMode.DirectSound:
			rdConfigAudioDirectSound.Checked = true;
			rdConfigAudioXAudio.Checked = false;
			break;
		case AudioMode.XAudio2:
			rdConfigAudioDirectSound.Checked = false;
			rdConfigAudioXAudio.Checked = true;
			break;
		}
	}

	private void xToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void xToolStripMenuItem_MouseLeave(object sender, EventArgs e)
	{
		(sender as ToolStripMenuItem).ForeColor = Color.DarkOrange;
	}

	private void xToolStripMenuItem_MouseEnter(object sender, EventArgs e)
	{
		(sender as ToolStripMenuItem).ForeColor = Color.Black;
	}

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
		this.lblConfigAudio = new System.Windows.Forms.Label();
		this.rdConfigAudioXAudio = new System.Windows.Forms.RadioButton();
		this.rdConfigAudioDirectSound = new System.Windows.Forms.RadioButton();
		this.btnConfigAudio = new System.Windows.Forms.Button();
		this.menuStrip1 = new System.Windows.Forms.MenuStrip();
		this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.pnConfigForm = new System.Windows.Forms.Panel();
		this.menuStrip1.SuspendLayout();
		this.pnConfigForm.SuspendLayout();
		base.SuspendLayout();
		this.lblConfigAudio.AutoSize = true;
		this.lblConfigAudio.BackColor = System.Drawing.Color.Black;
		this.lblConfigAudio.ForeColor = System.Drawing.Color.DarkOrange;
		this.lblConfigAudio.Location = new System.Drawing.Point(16, 21);
		this.lblConfigAudio.Name = "lblConfigAudio";
		this.lblConfigAudio.Size = new System.Drawing.Size(37, 13);
		this.lblConfigAudio.TabIndex = 0;
		this.lblConfigAudio.Text = "Audio:";
		this.rdConfigAudioXAudio.AutoSize = true;
		this.rdConfigAudioXAudio.BackColor = System.Drawing.Color.Black;
		this.rdConfigAudioXAudio.ForeColor = System.Drawing.Color.DarkOrange;
		this.rdConfigAudioXAudio.Location = new System.Drawing.Point(69, 42);
		this.rdConfigAudioXAudio.Name = "rdConfigAudioXAudio";
		this.rdConfigAudioXAudio.Size = new System.Drawing.Size(65, 17);
		this.rdConfigAudioXAudio.TabIndex = 1;
		this.rdConfigAudioXAudio.TabStop = true;
		this.rdConfigAudioXAudio.Text = "XAudio2";
		this.rdConfigAudioXAudio.UseVisualStyleBackColor = false;
		this.rdConfigAudioXAudio.Click += new System.EventHandler(rdConfigAudioDirectSound_Click);
		this.rdConfigAudioDirectSound.AutoSize = true;
		this.rdConfigAudioDirectSound.BackColor = System.Drawing.Color.Black;
		this.rdConfigAudioDirectSound.Checked = true;
		this.rdConfigAudioDirectSound.ForeColor = System.Drawing.Color.DarkOrange;
		this.rdConfigAudioDirectSound.Location = new System.Drawing.Point(69, 19);
		this.rdConfigAudioDirectSound.Name = "rdConfigAudioDirectSound";
		this.rdConfigAudioDirectSound.Size = new System.Drawing.Size(87, 17);
		this.rdConfigAudioDirectSound.TabIndex = 2;
		this.rdConfigAudioDirectSound.TabStop = true;
		this.rdConfigAudioDirectSound.Text = "Direct Sound";
		this.rdConfigAudioDirectSound.UseVisualStyleBackColor = false;
		this.rdConfigAudioDirectSound.Click += new System.EventHandler(rdConfigAudioDirectSound_Click);
		this.btnConfigAudio.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnConfigAudio.BackColor = System.Drawing.Color.FromArgb(255, 176, 86);
		this.btnConfigAudio.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btnConfigAudio.FlatAppearance.BorderSize = 2;
		this.btnConfigAudio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btnConfigAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnConfigAudio.ForeColor = System.Drawing.Color.Black;
		this.btnConfigAudio.Location = new System.Drawing.Point(151, 77);
		this.btnConfigAudio.Name = "btnConfigAudio";
		this.btnConfigAudio.Size = new System.Drawing.Size(47, 23);
		this.btnConfigAudio.TabIndex = 3;
		this.btnConfigAudio.Text = "Save";
		this.btnConfigAudio.UseVisualStyleBackColor = false;
		this.btnConfigAudio.Click += new System.EventHandler(btnConfigAudio_Click);
		this.menuStrip1.BackColor = System.Drawing.Color.Black;
		this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.xToolStripMenuItem });
		this.menuStrip1.Location = new System.Drawing.Point(0, 0);
		this.menuStrip1.Name = "menuStrip1";
		this.menuStrip1.Size = new System.Drawing.Size(214, 27);
		this.menuStrip1.TabIndex = 0;
		this.menuStrip1.Text = "menuStrip1";
		this.xToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		this.xToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.xToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.xToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.xToolStripMenuItem.ForeColor = System.Drawing.Color.DarkOrange;
		this.xToolStripMenuItem.Margin = new System.Windows.Forms.Padding(175, 0, 0, 0);
		this.xToolStripMenuItem.Name = "xToolStripMenuItem";
		this.xToolStripMenuItem.Size = new System.Drawing.Size(30, 23);
		this.xToolStripMenuItem.Text = "X";
		this.xToolStripMenuItem.MouseLeave += new System.EventHandler(xToolStripMenuItem_MouseLeave);
		this.xToolStripMenuItem.MouseEnter += new System.EventHandler(xToolStripMenuItem_MouseEnter);
		this.xToolStripMenuItem.Click += new System.EventHandler(xToolStripMenuItem_Click);
		this.pnConfigForm.BackColor = System.Drawing.Color.Black;
		this.pnConfigForm.Controls.Add(this.lblConfigAudio);
		this.pnConfigForm.Controls.Add(this.rdConfigAudioXAudio);
		this.pnConfigForm.Controls.Add(this.rdConfigAudioDirectSound);
		this.pnConfigForm.Controls.Add(this.btnConfigAudio);
		this.pnConfigForm.Controls.Add(this.menuStrip1);
		this.pnConfigForm.Location = new System.Drawing.Point(4, 3);
		this.pnConfigForm.Name = "pnConfigForm";
		this.pnConfigForm.Size = new System.Drawing.Size(214, 112);
		this.pnConfigForm.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.DarkOrange;
		base.ClientSize = new System.Drawing.Size(220, 115);
		base.Controls.Add(this.pnConfigForm);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.MainMenuStrip = this.menuStrip1;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "ConfigForm";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Audio Config";
		this.menuStrip1.ResumeLayout(false);
		this.menuStrip1.PerformLayout();
		this.pnConfigForm.ResumeLayout(false);
		this.pnConfigForm.PerformLayout();
		base.ResumeLayout(false);
	}
}
