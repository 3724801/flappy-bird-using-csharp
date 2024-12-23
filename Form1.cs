using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int gravity = 20;
        int speed = 15;
        Random r = new Random();
        int score = 0;
        int level = 1; // Initialize level
        int levelScoreThreshold = 5; // Score threshold to reach next level
        int levelScoreIncrement = 6; // Increment in score threshold for each subsequent level
        bool passedPipes = false; // Flag to track if bird has passed between pipes

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Top += gravity;
            pictureBox3.Left -= speed;
            pictureBox4.Left -= speed;
            label1.Text = $"Your Score: {score} - Level: {level}";
            if (pictureBox3.Left < 0) { pictureBox3.Left = r.Next(500, 600); passedPipes = false; }
            if (pictureBox4.Left < 0) { pictureBox4.Left = r.Next(500, 600); passedPipes = false; }
            if (pictureBox1.Bounds.IntersectsWith(pictureBox2.Bounds) || pictureBox1.Bounds.IntersectsWith(pictureBox3.Bounds) || pictureBox1.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                timer1.Stop();
                MessageBox.Show($"Game over! Your score is: {score} - Level: {level}");
                score = 0; // Reset score to 0
                level = 1; // Reset level to 1
                speed = 15; // Reset speed
                pictureBox1.Top = 50; // Reset bird position
                passedPipes = false; // Reset passedPipes flag
            }

            // Check if bird has passed between pipes
            if (!passedPipes && pictureBox1.Right > pictureBox3.Right && pictureBox1.Left < pictureBox4.Left)
            {
                passedPipes = true;
                score++; // Increment score
            }

            // Check if score reaches the threshold for the next level
            while (score >= (level * (levelScoreThreshold + levelScoreIncrement - 1)) / 2)
            {
                level++;
                speed += 5; // Increase speed for added challenge in higher levels
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (timer1.Enabled == false)
            {
                if (e.KeyCode == Keys.Enter) { timer1.Start(); pictureBox3.Left = r.Next(500, 600); pictureBox4.Left = r.Next(500, 600); pictureBox1.Top = 50; }
            }

            if (e.KeyCode == Keys.Space) { gravity = -15; }
            if (pictureBox1.Top < 2) { pictureBox1.Top = 15; }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) { gravity = 15; }
        }
    }
}

