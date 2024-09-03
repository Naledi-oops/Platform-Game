using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Platform_Game
{
     
    public partial class Form1 : Form
    {
        bool goleft, goright, jumping, isGameOver;

        int jumpSpeed;
        int score = 0;
        int force;
        int playerSpeed = 7;

        int horizontalSpeed = 3;
        int verticalSpeed = 3;

        int enemyOneSpeed = 3;
        int enemyTwoSpeed = 3;

        WindowsMediaPlayer mp = new WindowsMediaPlayer();
        
        public Form1()
        {
            InitializeComponent();
            mp.URL = "MyMusic.mp3";
            mp.controls.play();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            lblScore.Text = "Score : " + score;
           
            player.Top += jumpSpeed;

            if (goleft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goright == true)
            {
                player.Left += playerSpeed;
            }
            if (jumping == true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -7;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 5;
                            player.Top = x.Top - player.Height;

                           if((string)x.Name == "horizontalPLatform" && goleft == false ||(string)x.Name == "horizontalPlatform" && goright == false)
                            {
                                player.Left -= horizontalSpeed;
                            }

                        }
                        x.BringToFront();
                    }

                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            lblScore.Text = "Score : " + score + Environment.NewLine + "You were kiiled in your journey!";  
                        }
                    }
                        

                   
                }
                    

            }

            horizontalPlatform.Left += horizontalSpeed;
            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = - horizontalSpeed;
            }

            verticalPlatform.Top += verticalSpeed;  
            if (verticalPlatform.Top < 148 || verticalPlatform.Top > 302)
            {
                verticalSpeed = - verticalSpeed;
            }

            enemyOne.Left -= enemyOneSpeed;
            if (enemyOne.Left < pictureBox5.Left || enemyOne.Left + enemyOne.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyOneSpeed = - enemyOneSpeed;
            }

            enemyTwo.Left += enemyTwoSpeed;
            if (enemyTwo.Left < pictureBox2.Left || enemyTwo.Left + enemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = - enemyTwoSpeed;
            }

            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                lblScore.Text = "Score : " + score + Environment.NewLine + "You fell to your death!";
            }

            if (player.Bounds.IntersectsWith(Door.Bounds) && score == 25)
            {
                gameTimer.Stop();
                isGameOver = true;
                lblScore.Text = "Score : " + score + Environment.NewLine + "Game Over!";
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {
            jumping = false;
            goright = false;
            goleft = false;
            isGameOver = false;
            score = 0;

            lblScore.Text = "Score :" + score;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            player.Left = 62;
            player.Top = 370;

            enemyOne.Left = 376;
            enemyTwo.Left = 389;

            horizontalPlatform.Left = 210;
            verticalPlatform.Top = 302;

            gameTimer.Start();

        }
    }
}
