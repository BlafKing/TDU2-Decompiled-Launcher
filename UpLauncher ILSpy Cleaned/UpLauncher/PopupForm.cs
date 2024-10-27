using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace UpLauncher;

public class PopupForm : Form
{
	public delegate void MessageResultHandler(object sender, DialogResult dialogResult_0);

	public MessageResultHandler ChoiceDown;

	private MessageBoxButtons messageBoxButtons_0;

	private IContainer icontainer_0;

	private Button btOk;

	private Button btCancel;

	private Button btRetry;

	private SplitContainer pnForm;

	private RichTextBox tbMessage;

	public PopupForm()
	{
		InitializeComponent();
	}

	private void method_0(string string_0, MessageBoxButtons messageBoxButtons_1)
	{
		messageBoxButtons_0 = messageBoxButtons_1;
		tbMessage.MaximumSize = new Size(SystemInformation.PrimaryMonitorSize.Width / 2, SystemInformation.PrimaryMonitorSize.Height / 2);
		tbMessage.Text = string_0;
		method_3();
		btOk.Text = Class32.smethod_13("OK");
		btCancel.Text = Class32.smethod_13("Cancel");
		btRetry.Text = Class32.smethod_13("Retry");
		method_1();
		method_2();
	}

	private void method_1()
	{
		pnForm.Width = tbMessage.Width + 10;
		pnForm.Height = pnForm.Panel2.Height + tbMessage.Height + 10;
		pnForm.SplitterDistance = tbMessage.Height + 5;
		RichTextBox richTextBox = tbMessage;
		tbMessage.Top = 5;
		richTextBox.Left = 5;
		base.Width = pnForm.Width + 4;
		base.Height = pnForm.Height + 4;
		SplitContainer splitContainer = pnForm;
		pnForm.Top = 2;
		splitContainer.Left = 2;
	}

	private void method_2()
	{
		Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
		btOk.Visible = false;
		btCancel.Visible = false;
		btRetry.Visible = false;
		switch (messageBoxButtons_0)
		{
		case MessageBoxButtons.OK:
			btOk.Visible = true;
			btOk.Left = pnForm.Panel2.Width / 2 - btOk.Width / 2;
			break;
		case MessageBoxButtons.OKCancel:
		{
			btOk.Visible = true;
			btCancel.Visible = true;
			int num4 = btOk.Width + btCancel.Width + 50;
			btOk.Left = pnForm.Panel2.Width / 2 - num4 / 2;
			if (btOk.Left <= 0)
			{
				btOk.Left = 10;
			}
			btCancel.Left = btOk.Right + 50;
			break;
		}
		default:
		{
			btOk.Visible = true;
			btRetry.Visible = true;
			btCancel.Visible = true;
			int num2 = btOk.Width + btCancel.Width + btRetry.Width + 100;
			btOk.Left = base.Width / 2 - num2 / 3;
			if (btOk.Left <= 0)
			{
				btOk.Left = 10;
			}
			btRetry.Left = btOk.Right + 50;
			btCancel.Left = btRetry.Right + 50;
			break;
		}
		case MessageBoxButtons.YesNo:
		{
			btOk.DialogResult = DialogResult.Yes;
			btCancel.DialogResult = DialogResult.No;
			btOk.Text = Class32.smethod_13("Yes");
			btCancel.Text = Class32.smethod_13("No");
			btOk.Visible = true;
			btCancel.Visible = true;
			int num3 = btOk.Width + btCancel.Width + 50;
			btOk.Left = pnForm.Panel2.Width / 2 - num3 / 2;
			if (btOk.Left <= 0)
			{
				btOk.Left = 10;
			}
			btCancel.Left = btOk.Right + 50;
			break;
		}
		case MessageBoxButtons.RetryCancel:
		{
			btRetry.Visible = true;
			btCancel.Visible = true;
			int num = btRetry.Width + btCancel.Width + 50;
			btRetry.Left = pnForm.Panel2.Width / 2 - num / 2;
			if (btRetry.Left <= 0)
			{
				btOk.Left = 10;
			}
			btCancel.Left = btRetry.Right + 50;
			break;
		}
		}
	}

	private void method_3()
	{
		tbMessage.Width = tbMessage.MinimumSize.Width;
		tbMessage.Height = tbMessage.MinimumSize.Height;
		tbMessage.Update();
		int lineFromCharIndex = tbMessage.GetLineFromCharIndex(tbMessage.Text.Length - 1);
		int num = tbMessage.Text.Split('\n').Length;
		if (lineFromCharIndex <= 0)
		{
			return;
		}
		Graphics graphics = tbMessage.CreateGraphics();
		SizeF sizeF = graphics.MeasureString(tbMessage.Text, tbMessage.Font);
		int num2 = 0;
		while (!((float)(lineFromCharIndex - num) * sizeF.Height <= (float)tbMessage.Height) && (tbMessage.Width < tbMessage.MaximumSize.Width || tbMessage.Height < tbMessage.MaximumSize.Height))
		{
			if (num2 == 0)
			{
				if (tbMessage.Width + 50 < tbMessage.MaximumSize.Width)
				{
					tbMessage.Width += 50;
					tbMessage.Update();
				}
				else
				{
					tbMessage.Width = tbMessage.MaximumSize.Width;
				}
				num2++;
			}
			else
			{
				if (tbMessage.Height + 50 < tbMessage.MaximumSize.Height)
				{
					tbMessage.Height += 50;
					tbMessage.Update();
				}
				else
				{
					tbMessage.Height = tbMessage.MaximumSize.Height;
				}
				num2 = 0;
			}
			lineFromCharIndex = tbMessage.GetLineFromCharIndex(tbMessage.Text.Length - 1);
		}
		tbMessage.Height += (int)sizeF.Height;
	}

	public static DialogResult Show(string a_Message, MessageBoxButtons a_MessageBoxButtons)
	{
		PopupForm popupForm = new PopupForm();
		popupForm.method_0(a_Message, a_MessageBoxButtons);
		if (MainForm.MainInstance == null)
		{
			popupForm.StartPosition = FormStartPosition.CenterScreen;
		}
		else
		{
			popupForm.StartPosition = FormStartPosition.CenterParent;
		}
		return popupForm.ShowDialog(MainForm.MainInstance);
	}

	private void method_4(object sender, EventArgs e)
	{
		if (tbMessage.Width > 0)
		{
			method_1();
			method_2();
		}
		else
		{
			tbMessage.Width = pnForm.Width;
		}
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
		this.btOk = new System.Windows.Forms.Button();
		this.btCancel = new System.Windows.Forms.Button();
		this.btRetry = new System.Windows.Forms.Button();
		this.pnForm = new System.Windows.Forms.SplitContainer();
		this.tbMessage = new System.Windows.Forms.RichTextBox();
		this.pnForm.Panel1.SuspendLayout();
		this.pnForm.Panel2.SuspendLayout();
		this.pnForm.SuspendLayout();
		base.SuspendLayout();
		this.btOk.AutoSize = true;
		this.btOk.BackColor = System.Drawing.Color.FromArgb(255, 176, 86);
		this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btOk.FlatAppearance.BorderSize = 2;
		this.btOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btOk.ForeColor = System.Drawing.Color.Black;
		this.btOk.Location = new System.Drawing.Point(12, 10);
		this.btOk.Name = "btOk";
		this.btOk.Size = new System.Drawing.Size(75, 27);
		this.btOk.TabIndex = 1;
		this.btOk.Text = "OK";
		this.btOk.UseVisualStyleBackColor = false;
		this.btCancel.AutoSize = true;
		this.btCancel.BackColor = System.Drawing.Color.FromArgb(255, 176, 86);
		this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btCancel.FlatAppearance.BorderSize = 2;
		this.btCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btCancel.ForeColor = System.Drawing.Color.Black;
		this.btCancel.Location = new System.Drawing.Point(107, 10);
		this.btCancel.Name = "btCancel";
		this.btCancel.Size = new System.Drawing.Size(75, 27);
		this.btCancel.TabIndex = 2;
		this.btCancel.Text = "Cancel";
		this.btCancel.UseVisualStyleBackColor = false;
		this.btRetry.AutoSize = true;
		this.btRetry.BackColor = System.Drawing.Color.FromArgb(255, 176, 86);
		this.btRetry.DialogResult = System.Windows.Forms.DialogResult.Retry;
		this.btRetry.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btRetry.FlatAppearance.BorderSize = 2;
		this.btRetry.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btRetry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btRetry.ForeColor = System.Drawing.Color.Black;
		this.btRetry.Location = new System.Drawing.Point(202, 10);
		this.btRetry.Name = "btRetry";
		this.btRetry.Size = new System.Drawing.Size(75, 27);
		this.btRetry.TabIndex = 3;
		this.btRetry.Text = "Retry";
		this.btRetry.UseVisualStyleBackColor = false;
		this.pnForm.BackColor = System.Drawing.Color.Black;
		this.pnForm.Location = new System.Drawing.Point(2, 2);
		this.pnForm.Margin = new System.Windows.Forms.Padding(0);
		this.pnForm.Name = "pnForm";
		this.pnForm.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.pnForm.Panel1.Controls.Add(this.tbMessage);
		this.pnForm.Panel1MinSize = 150;
		this.pnForm.Panel2.Controls.Add(this.btRetry);
		this.pnForm.Panel2.Controls.Add(this.btCancel);
		this.pnForm.Panel2.Controls.Add(this.btOk);
		this.pnForm.Panel2MinSize = 40;
		this.pnForm.Size = new System.Drawing.Size(296, 196);
		this.pnForm.SplitterDistance = 150;
		this.pnForm.SplitterWidth = 1;
		this.pnForm.TabIndex = 4;
		this.pnForm.TabStop = false;
		this.tbMessage.BackColor = System.Drawing.Color.Black;
		this.tbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tbMessage.ForeColor = System.Drawing.Color.White;
		this.tbMessage.Location = new System.Drawing.Point(3, 3);
		this.tbMessage.MinimumSize = new System.Drawing.Size(200, 100);
		this.tbMessage.Name = "tbMessage";
		this.tbMessage.ReadOnly = true;
		this.tbMessage.Size = new System.Drawing.Size(283, 144);
		this.tbMessage.TabIndex = 0;
		this.tbMessage.TabStop = false;
		this.tbMessage.Text = "OK";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.BackColor = System.Drawing.Color.DarkOrange;
		base.ClientSize = new System.Drawing.Size(300, 200);
		base.Controls.Add(this.pnForm);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "PopupForm";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.pnForm.Panel1.ResumeLayout(false);
		this.pnForm.Panel2.ResumeLayout(false);
		this.pnForm.Panel2.PerformLayout();
		this.pnForm.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
