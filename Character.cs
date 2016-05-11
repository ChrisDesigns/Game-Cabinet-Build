using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PortalToMyWorld
{
    class Character
    {

        public int CurrentLevel = 0;
        public int lastPortal = 0;

        Texture2D CharTex;
        Texture2D CharTex2;
        Texture2D CharRunTexL;
        Texture2D CharRunTexR;

        public Rectangle CharRect;
        public Rectangle SourceRect;

        GamePadState pad1;

        public int playerHealth = 100;

        public string direction = "";
        public string directionX = "right";
        public int PlayerSpeedX = 4;
        public int PlayerSpeedY = -16;
        public bool OnGround;
        public int height, width;

        int ranonce;

        KeyboardState ks;

        public bool canWalkLeft = true;
        public bool canWalkRight = true;
        public bool PlayerintWall = false;
        public bool PlayerDead = false;
        public bool CharRun;

        public int[] XYPOS = new int[2];

        public int currentframe = 1;
        public Character(Texture2D CharTex, Texture2D CharTex2, Texture2D CharRunTexR, Texture2D CharRunTexL)
        {
            this.CharRunTexL = CharRunTexL;
            this.CharRunTexR = CharRunTexR;

            this.CharTex2 = CharTex2;
            this.CharTex = CharTex;
            //PlayerSpeedY = height;
            CharRect = new Rectangle(0, height, 50, 35);
        }
        public void update()
        {

            pad1 = GamePad.GetState(PlayerIndex.One);
            ks = Keyboard.GetState();


            SourceRect = new Rectangle((27 * currentframe), 0, 27, 35);

            SourceRect.X = currentframe * 27;

            if (ranonce == 0)
            {
                CharRect.Y = height - CharRect.Height;
                ranonce = 1;
            }
            if (PlayerSpeedY == 0)
            {
                direction = "down";
            }

            if (CharRect.X < 0)
            {
                CharRect.X = 0;
            }
            if (CharRect.X > width - CharRect.Width)
            {
                CharRect.X = width - CharRect.Width;
            }

            if (CharRect.Y <= (height - CharRect.Height) && direction != "")
            {
                CharRect.Y -= (int) (PlayerSpeedY * GravityLevel());
                PlayerSpeedY--;
            }
            if (CharRect.Y >= (height - CharRect.Height) && direction == "down")
            {
                CharRect.Y = (height - CharRect.Height);
                direction = "";
                PlayerSpeedY = -16;
            }
            /*
            if (pad1.ThumbSticks.Left.X < 0 && (CharRect.X > 0) && canWalkLeft)
                {
                    directionX = "left";
                    CharRect.X += (int)(pad1.ThumbSticks.Left.X * PlayerSpeedX);
                    CharRun = true;
                }
            else if (pad1.ThumbSticks.Left.X > 0 && (CharRect.X < (width - CharRect.Width)) && canWalkRight)
                {
                    directionX = "right";
                    CharRect.X += (int)(pad1.ThumbSticks.Left.X * PlayerSpeedX);
                    CharRun = true;
                }*/
            if (ks.IsKeyDown(Keys.Left) && (CharRect.X > 0) && canWalkLeft)
            {
                directionX = "left";
                CharRect.X += (int)(-1 * PlayerSpeedX);
                CharRun = true;
            }
            else if (ks.IsKeyDown(Keys.Right) && (CharRect.X < (width - CharRect.Width)) && canWalkRight)
            {
                directionX = "right";
                CharRect.X += (int)(1 * PlayerSpeedX);
                CharRun = true;
            }
            else
            {
                directionX = "";
                CharRun = false;
                currentframe = 1;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (PlayerDead != true)
            {
                if (directionX == "left")
                {
                    if (CharRun)
                    {
                        spriteBatch.Draw(CharRunTexL, CharRect, SourceRect, Color.White);
                    }
                }
                else if (directionX == "right")
                {
                    if (CharRun)
                    {
                        spriteBatch.Draw(CharRunTexR, CharRect, SourceRect, Color.White);
                    }
                    
                }
                else if (!CharRun && canWalkLeft && canWalkRight)
                {
                    if (directionX == "right")
                    {
                        spriteBatch.Draw(CharTex2, CharRect, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(CharTex, CharRect, Color.White);
                    }
                }
            }
        }

        public void PlayerPos()
        {
            switch(CurrentLevel)
            {

                case 1:
                    XYPOS[0] = 0;
                    XYPOS[1] = height;
                break;

                case 2:
                    XYPOS[0] = width - 75;
                    XYPOS[1] = 170;
                break;

                case 3:
                    XYPOS[0] = 0;
                    XYPOS[1] = height - CharRect.Height;
                break;

                case 4:
                    XYPOS[0] = 420;
                    XYPOS[1] = 170;
                break;

                case 5:
                    XYPOS[0] = 0;
                    XYPOS[1] = 0;
                break;

                default:
                    XYPOS[0] = 0;
                    XYPOS[1] = 0;
                break;

            }

        }
        public int GetPortalAmount()
        {
            int amount = 0;

            switch(CurrentLevel)
            {

                case 1:
                    amount = 3;
                break;

                case 2:
                    amount = 3;
                break;

                case 3:
                    amount = 3;
                break;

                case 4:
                    amount = 3;
                break;

                case 5:
                    amount = 0;
                break;

                default:
                    amount = 0;
                break;

            }
            return amount;
        }

        public float GravityLevel()
        {
           float amount = 0;

            switch (CurrentLevel)
            {

                case 1:
                    amount = 1.2f;
                    break;

                case 2:
                    amount = 1.05f;
                    break;

                case 3:
                    amount = 1.5f;
                    break;

                case 4:
                    amount = 1.4f;
                    break;

                case 5:
                    amount = 1.1f;
                    break;

                default:
                    amount = 0;
                    break;

            }
            return amount;
        }
        public int GetWallAmount()
        {
            int amount = 0;

            switch (CurrentLevel)
            {

                case 1:
                    amount = 13;
                    break;

                case 2:
                    amount = 9;
                    break;

                case 3:
                    amount = 12;
                    break;

                case 4:
                    amount = 16;
                    break;

                case 5:
                    amount = 0;
                    break;

                default:
                    amount = 0;
                    break;

            }
            return amount;
        }


        public int GetGroundAmount()
        {
            int amount = 0;

            switch (CurrentLevel)
            {

                case 1:
                    amount = 23;
                    break;

                case 2:
                    amount = 15;
                    break;

                case 3:
                    amount = 19;
                    break;

                case 4:
                    amount = 29;
                    break;

                case 5:
                    amount = 0;
                    break;

                default:
                    amount = 0;
                    break;

            }
            return amount;
        }

        public int GetSpikeAmount()
        {
            int amount = 0;

            switch (CurrentLevel)
            {

                case 1:
                    amount = 2;
                    break;

                case 2:
                    amount = 2;
                    break;

                case 3:
                    amount = 5;
                    break;

                case 4:
                    amount = 8;
                    break;

                case 5:
                    amount = 0;
                    break;

                default:
                    amount = 0;
                    break;

            }
            return amount;
        }
    }
}
