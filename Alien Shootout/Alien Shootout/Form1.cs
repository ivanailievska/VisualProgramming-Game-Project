using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace Alien_Shootout
{
    public partial class Form1 : Form
    {

        bool goLeft,goRight,goUp,goDown,gameOver;
        string facing = "up";
        int playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int alienSpeed = 3;
        Random randNum = new Random();
        int score;

        List<PictureBox> alienList = new List<PictureBox> ();

        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (playerHealth > 1)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
                player.Image = Properties.Resources.dead;
                GameTimer.Stop();
            }

            txtAmmo.Text = "Ammo: " + ammo;
            txtScore.Text = "Kills: " + score;

            if (playerHealth < 20)
            {
                healthBar.ForeColor = System.Drawing.Color.Red;
            }

            if (goLeft == true && player.Left > 0)
            {
                player.Left -= speed;
            }
            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += speed;
            }
            if (goUp == true && player.Top > 45)
            {
                player.Top -= speed;
            }
            if (goDown == true && player.Top + player.Height < this.ClientSize.Height)
            {
                player.Top += speed;
            }

            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "ammo")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }
                }

                if(x is PictureBox && (string)x.Tag == "alien")
                {

                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerHealth -= 1;
                    }

                    if(x.Left > player.Left)
                    {
                        x.Left -= alienSpeed;
                        ((PictureBox)x).Image = Properties.Resources.aleft;
                    }
                    if (x.Left < player.Left)
                    {
                        x.Left += alienSpeed;
                        ((PictureBox)x).Image = Properties.Resources.aright;
                    }
                    if (x.Top > player.Top)
                    {
                        x.Top -= alienSpeed;
                        ((PictureBox)x).Image = Properties.Resources.aup;
                    }
                    if (x.Top < player.Top)
                    {
                        x.Top += alienSpeed;
                        ((PictureBox)x).Image = Properties.Resources.adown;
                    }
                }

                foreach(Control j in this.Controls)
                {
                    if(j is PictureBox && (string)j.Tag == "bullet" && x is PictureBox && (string)x.Tag == "alien")
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;
                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            alienList.Remove(((PictureBox)x));
                            MakeAlien();
                        }
                    }
                }

            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(gameOver == true)
            {
                return;
            }

            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.left;
            }

            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing="right";
                player.Image = Properties.Resources.right;
            }

            if(e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                player.Image = Properties.Resources.up;
            }

            if(e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                player.Image= Properties.Resources.down;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }

            if (e.KeyCode == Keys.Space && ammo > 0 && gameOver == false)
            {
                ammo--;
                ShootBullet(facing);
                if (ammo < 1)
                {
                    DropAmmo();
                }
            }

            if(e.KeyCode == Keys.Enter && gameOver == true)
            {
                RestartGame();
            }
        }

        private void ShootBullet(string direction) 
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletLeft = player.Left + (player.Width / 2);
            shootBullet.bulletTop = player.Top + (player.Height / 2);
            shootBullet.MakeBullet(this);
        }

        private void MakeAlien()
        {
            PictureBox alien = new PictureBox();
            alien.Tag = "alien";
            alien.Image = Properties.Resources.adown;
            alien.Left = randNum.Next(0, 1200);
            alien.Top = randNum.Next(0, 1200);
            alien.SizeMode = PictureBoxSizeMode.AutoSize;
            alienList.Add(alien);
            this.Controls.Add(alien);
            player.BringToFront();
        }

        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = randNum.Next(10, this.ClientSize.Width - ammo.Width);
            ammo.Top = randNum.Next(60, this.ClientSize.Height - ammo.Height);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);

            ammo.BringToFront();
            player.BringToFront();
        }

        private void RestartGame()
        {
            player.Image = Properties.Resources.up;
            foreach(PictureBox i in alienList)
            {
                this.Controls.Remove(i);
            }

            alienList.Clear();

            for(int i = 0; i < 3; i++)
            {
                MakeAlien();
            }

            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
            gameOver = false;

            playerHealth = 100;
            score = 0;
            ammo = 10;
            GameTimer.Start();
        }
    }
}
