using System.Drawing;
using System.Windows.Forms;

namespace UpLauncher;

public class CustomProgressBar : ProgressBar
{
	private SolidBrush solidBrush_0;

	private SolidBrush solidBrush_1;

	public CustomProgressBar()
	{
		SetStyle(ControlStyles.UserPaint, value: true);
	}

	public CustomProgressBar(ProgressBar a_ProgressBar)
	{
		SetStyle(ControlStyles.UserPaint, value: true);
		BackColor = Color.Black;
		base.Width = a_ProgressBar.Width;
		base.Height = a_ProgressBar.Height;
		base.Location = a_ProgressBar.Location;
		base.Maximum = a_ProgressBar.Maximum;
		solidBrush_1 = new SolidBrush(Color.FromArgb(255, 137, 20));
		solidBrush_0 = new SolidBrush(Color.FromArgb(255, 176, 86));
	}

	protected override void OnPaint(PaintEventArgs paintEventArgs_0)
	{
		Rectangle bounds = new Rectangle(0, 0, base.Width, base.Height);
		if (ProgressBarRenderer.IsSupported)
		{
			ProgressBarRenderer.DrawHorizontalBar(paintEventArgs_0.Graphics, bounds);
		}
		bounds.Width = (int)((double)bounds.Width * ((double)base.Value / (double)base.Maximum)) - 2;
		bounds.Height -= 2;
		paintEventArgs_0.Graphics.FillRectangle(solidBrush_0, 1, 1, bounds.Width - 1, bounds.Height / 2);
		paintEventArgs_0.Graphics.FillRectangle(solidBrush_1, 1, bounds.Height / 2 + 1, bounds.Width - 1, bounds.Height / 2);
	}
}
