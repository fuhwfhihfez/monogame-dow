using System;
using System.Windows.Forms;

namespace Dow.Main;

public static class Program
{
    [STAThread]
    static void Main()
    {
        try
        {
            using var game = new Game1();
            game.Run();
        }
        catch (Exception ex)
        {
            using var form = new System.Windows.Forms.Form
            {
                Text = "MonoGame Error",
                Width = 900,
                Height = 600,
                StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            };

            using var textBox = new System.Windows.Forms.TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = System.Windows.Forms.ScrollBars.Both,
                WordWrap = false,
                Dock = System.Windows.Forms.DockStyle.Fill,
                Font = new System.Drawing.Font("Consolas", 10),
                Text = ex.ToString()
            };

            form.Controls.Add(textBox);
            form.ShowDialog();
        }
    }
}