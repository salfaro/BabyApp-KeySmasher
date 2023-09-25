using System;
using System.Drawing;
using System.Windows.Forms;

namespace BabyApp
{
    public partial class BabyAppForm : Form
    {
        private bool babyMode = false;
        private Label exitLabel;

        public BabyAppForm()
        {
            InitializeComponent();

            // Automatically enter baby mode upon launch
            ToggleBabyMode();
        }

        private void BabyAppForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Check for Ctrl + Alt + H to toggle baby mode
            if (e.Control && e.Alt && e.KeyCode == Keys.H)
            {
                ToggleBabyMode();
            }
            else if (babyMode)
            {
                // Baby is in control - change the screen color to random pastel color
                BackColor = GetRandomPastelColor();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check for Ctrl + Alt + H to toggle baby mode
            if (keyData == (Keys.Control | Keys.Alt | Keys.H))
            {
                ToggleBabyMode();
                return true; // Mark the key as handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void ToggleBabyMode()
        {
            babyMode = !babyMode;

            if (babyMode)
            {
                // Enter baby mode - hijack the screen and display random colors
                WindowState = FormWindowState.Maximized;
                FormBorderStyle = FormBorderStyle.None;
                TopMost = true;
                Cursor.Hide();
                //BackColor = GetRandomPastelColor();
                ChangeRandomBackgroundColor();

                // Add a label with the exit message
                exitLabel = new Label();
                exitLabel.Text = "To exit, press Ctrl + Alt + H";
                exitLabel.Font = new Font("Arial", 16);
                exitLabel.ForeColor = Color.White;
                exitLabel.BackColor = Color.Transparent;
                exitLabel.AutoSize = true;
                Controls.Add(exitLabel);
                exitLabel.Location = new Point((Width - exitLabel.Width) / 2, 10);
            }
            else
            {
                // Exit baby mode - show a dialog and return to normal
                if (exitLabel != null)
                {
                    Controls.Remove(exitLabel);
                    exitLabel.Dispose();
                }
                // Exit baby mode - show a dialog and return to normal
                WindowState = FormWindowState.Normal;
                FormBorderStyle = FormBorderStyle.Sizable;
                TopMost = false;
                Cursor.Show();

                DialogResult result = MessageBox.Show("Would you like to close the app?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Close();
                }
            }
        }

        private void ChangeRandomBackgroundColor()
        {
            BackColor = GetRandomPastelColor();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (babyMode)
            {
                // Change the background color with each keystroke
                ChangeRandomBackgroundColor();
            }
        }

        private Color GetRandomPastelColor()
        {
            Random rand = new Random();
            int r = rand.Next(128, 256); // Red component
            int g = rand.Next(128, 256); // Green component
            int b = rand.Next(128, 256); // Blue component

            return Color.FromArgb(r, g, b);
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BabyAppForm());
        }

    }

}
