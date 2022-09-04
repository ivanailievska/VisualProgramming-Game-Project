# VisualProgramming-Game-Project
Windows Forms Project by: Ivana Ilievska

Опис на апликацијата:
Апликацијата што ја развивав е игра што се вика Alien Shooutout. Самата игра е едноставна, вие сте стрелецот кој има ограничена муниција и вонземјани се појавуваат на случајни места и доаѓаат по играчот. Има пад на муниција, падот на муниција се случува кога играчот ќе ја потроши својата муниција, таа паѓа на различни места во играта и играчот треба да ја собере. Има место каде што се води сметка за колку муниција има играчот и колку му е резултатот односно колку вонземјани ќе отстрани. Исто така во играта има Health bar односно место каде што се чува состојбата на играчот, ако вонземјанин го допре му се намалува. Ако Health bar-от стигне до 0 тогаш играчот ја губи играта.

Упатство за користењe:
Со самото Стартување на играта, таа започнува. Играчот се движи на стрелките на тастатурата → , ← , ↑ , ↓ , а пука на space . Кога health bar-от ќе стигне на 0 ќе се сопре играта, а таа може да се стартува од почеток со Enter. Сите податоци за прошната игра ќе бидат ресетирани и се стартува нова игра.

![image1](https://user-images.githubusercontent.com/86912362/188316518-0ea3e1b2-a290-4777-bd1a-15e974690f67.png)

![image2](https://user-images.githubusercontent.com/86912362/188316698-b38708df-05ed-4d51-b96b-e888e59f8f2e.png)

![image3](https://user-images.githubusercontent.com/86912362/188316704-0d3169b4-814c-4c8f-92d3-9badf6a4b05a.png)

![image4](https://user-images.githubusercontent.com/86912362/188316716-532158ff-9523-479f-9f0b-5dcf3abc9c75.png)


Функцијата MainTimerEvent, со детално објаснување на кодот со коментари:

private void MainTimerEvent(object sender, EventArgs e)
        {
            if (playerHealth > 1) // if player health is greater than 1
            {
                healthBar.Value = playerHealth; // assign the progress bar to the player health integer
            }
            else
            {
                // if the player health is below 1
                player.Image = Properties.Resources.dead; // show the player dead image
                timer1.Stop(); // stop the timer
                gameOver = true; // change game over to true
            }
 
            label1.Text = "   Ammo:  " + ammo; // show the ammo amount on label 1
            label2.Text = "Kills: " + score; // show the total kills on the score

 
            if (goleft && player.Left > 0)
            {
                player.Left -= speed;
                // if moving left is true AND pacman is 1 pixel more from the left 
                // then move the player to the LEFT
            }
            if (goright && player.Left + player.Width < 930)
            {
                player.Left += speed;
                // if moving RIGHT is true AND player left + player width is less than 930 pixels
                // then move the player to the RIGHT
            }
            if (goup && player.Top > 60)
            {
                player.Top -= speed;
                // if moving TOP is true AND player is 60 pixel more from the top 
                // then move the player to the UP
            }
 
            if (godown && player.Top + player.Height < 700)
            {
                player.Top += speed;
                // if moving DOWN is true AND player top + player height is less than 700 pixels
                // then move the player to the DOWN
            }
 
            // run the first for each loop below
            // X is a control and we will search for all controls in this loop
            foreach (Control x in this.Controls)
            {
                // if the X is a picture box and X has a tag AMMO
 
                if (x is PictureBox && x.Tag == "ammo")
                {
                    // check is X in hitting the player picture box
 
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        // once the player picks up the ammo
 
                        this.Controls.Remove(((PictureBox)x)); // remove the ammo picture box
 
                        ((PictureBox)x).Dispose(); // dispose the picture box completely from the program
                        ammo += 5; // add 5 ammo to the integer
                    }
                }
 
                // if the bullets hits the 4 borders of the game
                // if x is a picture box and x has the tag of bullet
 
                if (x is PictureBox && x.Tag == "bullet")
                {
                    // if the bullet is less the 1 pixel to the left
                    // if the bullet is more then 930 pixels to the right
                    // if the bullet is 10 pixels from the top
                    // if the bullet is 700 pixels to the buttom
 
        if (((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 930 || ((PictureBox)x).Top < 10 || ((PictureBox)x).Top > 700)
                    {
                        this.Controls.Remove(((PictureBox)x)); // remove the bullet from the display
                        ((PictureBox)x).Dispose(); // dispose the bullet from the program
                    }
                }
 
                // below is the if statement which will be checking if the player hits a alien
 
                if (x is PictureBox && x.Tag == "alien")
                {
 
                    // below is the if statament thats checking the bounds of the player and the alien
 
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealth -= 1; // if the alien hits the player then we decrease the health by 1
                    }
 
                    //move alien towards the player picture box
 
                    if (((PictureBox)x).Left > player.Left)
                    {
                     ((PictureBox)x).Left -= alienSpeed; // move alien towards the left of the player
                     ((PictureBox)x).Image = Properties.Resources.aleft; // change the alien image to the left
                    }
 
                    if (((PictureBox)x).Top > player.Top)
                    {
                        ((PictureBox)x).Top -= alienSpeed; // move alien upwards towards the players top
                        ((PictureBox)x).Image = Properties.Resources.aup; // change the alien picture to the top pointing image
                    }
                    if (((PictureBox)x).Left < player.Left)
                    {
                     ((PictureBox)x).Left += alienSpeed; // move alien towards the right of the player
                     ((PictureBox)x).Image = Properties.Resources.aright; // change the image to the right image
                    }
                    if (((PictureBox)x).Top < player.Top)
                    {
                      ((PictureBox)x).Top += alienSpeed; // move the alien towards the bottom of the player
                      ((PictureBox)x).Image = Properties.Resources.adown; // change the image to the down alien
                    }
                }
 
                // below is the second for loop, this is nexted inside the first one
                // the bullet and alien needs to be different than each other
                // then we can use that to determine if the hit each other
 
                foreach (Control j in this.Controls)
                {
                    // below is the selection thats identifying the bullet and alien
 
                    if ((j is PictureBox && j.Tag == "bullet") && (x is PictureBox && x.Tag == "alien"))
                    {
                        // below is the if statement thats checking if bullet hits the alien
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++; // increase the kill score by 1 
                            this.Controls.Remove(j); // this will remove the bullet from the screen
                            j.Dispose(); // this will dispose the bullet all together from the program
                            this.Controls.Remove(x); // this will remove the alien from the screen
                            x.Dispose(); // this will dispose the alien from the program
                            makeAlien(); // this function will invoke the make aliens function to add another alien to the game
                        }
                    }
                }
            }
        }
