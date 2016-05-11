using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PortalToMyWorld
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Portal[] portal = new Portal[3];

        Ground[] ground = new Ground[34];

        wall[] Wall = new wall[16];

        Spike[] spike = new Spike[10];

        Character player;

        int intsec;

        bool gameOn;

        KeyboardState ks;
        KeyboardState ks2;
        GamePadState oldpad1, pad1;

        SpriteFont font;

        double time;
        SoundEffectInstance musicInstance = null;
        SoundEffect song;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            player = new Character(this.Content.Load<Texture2D>("char"), this.Content.Load<Texture2D>("char2"), this.Content.Load<Texture2D>("sheetr"), this.Content.Load<Texture2D>("sheetl"));

            for (int i = 0; i < portal.Length; i++)
            {
                    portal[i] = new Portal();
            }

            for (int i = 0; i < Wall.Length; i++)
            {
                Wall[i] = new wall(this.Content.Load<Texture2D>("wall"));
            }

            for (int i = 0; i < ground.Length; i++)
            {
                ground[i] = new Ground(this.Content.Load<Texture2D>("wall"));
            }

            for (int i = 0; i < spike.Length; i++)
            {
                spike[i] = new Spike(this.Content.Load<Texture2D>("spike"));
            }

            font = Content.Load<SpriteFont>("SpriteFont1");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.graphics.IsFullScreen = true;

            player.width = GraphicsDevice.DisplayMode.Width;
            player.height = GraphicsDevice.DisplayMode.Height;
            song = this.Content.Load<SoundEffect>("Deep");
            this.graphics.PreferredBackBufferWidth = player.width;
            this.graphics.PreferredBackBufferHeight = player.height;

            this.graphics.ApplyChanges();
        }

        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {

            pad1 = GamePad.GetState(PlayerIndex.One);
            ks = Keyboard.GetState();
            PlayMainMusic();
                if (ks.IsKeyDown(Keys.Enter) && ks2.IsKeyUp(Keys.Enter))
                {
                    if (player.CurrentLevel == 0 || player.CurrentLevel == 6)
                    {
                        player.CurrentLevel = 1;
                        player.PlayerDead = false;
                        for (int i = 0; i < portal.Length; i++)
                        {
                            portal[i] = new Portal();
                        }

                        for (int i = 0; i < Wall.Length; i++)
                        {
                            Wall[i] = new wall(this.Content.Load<Texture2D>("wall"));
                        }

                        for (int i = 0; i < ground.Length; i++)
                        {
                            ground[i] = new Ground(this.Content.Load<Texture2D>("wall"));
                        }

                        for (int i = 0; i < spike.Length; i++)
                        {
                            spike[i] = new Spike(this.Content.Load<Texture2D>("spike"));
                        }
                        intsec = -1;
                        player.OnGround = false;
                        player.PlayerintWall = false;
                        player.PlayerPos();
                        player.CharRect.X = player.XYPOS[0];
                        player.CharRect.Y = player.XYPOS[1];
                        gameOn = true;
                    }
                    else
                    {
                        player.CurrentLevel = 0;
                        player.PlayerDead = true;
                        for (int i = 0; i < portal.Length; i++)
                        {
                            portal[i] = new Portal();
                        }

                        for (int i = 0; i < Wall.Length; i++)
                        {
                            Wall[i] = new wall(this.Content.Load<Texture2D>("wall"));
                        }

                        for (int i = 0; i < ground.Length; i++)
                        {
                            ground[i] = new Ground(this.Content.Load<Texture2D>("wall"));
                        }

                        for (int i = 0; i < spike.Length; i++)
                        {
                            spike[i] = new Spike(this.Content.Load<Texture2D>("spike"));
                        }
                        intsec = -1;
                        player.OnGround = false;
                        player.PlayerintWall = false;
                        player.PlayerPos();
                        player.CharRect.X = player.XYPOS[0];
                        player.CharRect.Y = player.XYPOS[1];
                        gameOn = false;
                    }
                }

                ks2 = ks;
            if (gameOn)
            {
                if (ks.IsKeyDown(Keys.Back) && (ks.IsKeyUp(Keys.Space) || player.PlayerSpeedY == 0 && player.direction != "up"))
                {
                    player.PlayerDead = false;
                    for (int i = 0; i < portal.Length; i++)
                    {
                        portal[i] = new Portal();
                    }

                    for (int i = 0; i < Wall.Length; i++)
                    {
                        Wall[i] = new wall(this.Content.Load<Texture2D>("wall"));
                    }

                    for (int i = 0; i < ground.Length; i++)
                    {
                        ground[i] = new Ground(this.Content.Load<Texture2D>("wall"));
                    }

                    for (int i = 0; i < spike.Length; i++)
                    {
                        spike[i] = new Spike(this.Content.Load<Texture2D>("spike"));
                    }
                    intsec = -1;
                    player.OnGround = false;
                    player.PlayerintWall = false;
                    player.PlayerPos();
                    player.CharRect.X = player.XYPOS[0];
                    player.CharRect.Y = player.XYPOS[1];
                    player.direction = "";
                    player.PlayerSpeedY = -16;
                }
                ks2 = ks;
                if (player.CurrentLevel == 1)
                {
                    //Array.Resize(ref portal, player.GetPortalAmount());
                    //Array.Resize(ref Wall, player.GetWallAmount());
                    //Array.Resize(ref spike, player.GetSpikeAmount());
                    //Array.Resize(ref ground, player.GetGroundAmount());

                    spike[0].SpikeRect = new Rectangle(840, 930, 70, 120);
                    spike[1].SpikeRect = new Rectangle(260, 920, 70, 130);

                    portal[0].PortalRect = new Rectangle(1080, 920, 60, 80);
                    portal[1].PortalRect = new Rectangle(1100, 50, 60, 80);
                    portal[2].PortalRect = new Rectangle(560, 130, 60, 80);

                    Wall[0].wallRect = new Rectangle(290, 120, 10, 350);
                    Wall[1].wallRect = new Rectangle(710, 340, 10, 135);
                    Wall[2].wallRect = new Rectangle(540, 0, 10, 230);
                    Wall[3].wallRect = new Rectangle(740, 0, 10, 95);
                    Wall[4].wallRect = new Rectangle(1035, 0, 10, 195);
                    Wall[5].wallRect = new Rectangle(1170, 185, 10, 235);
                    Wall[6].wallRect = new Rectangle(685, 860, 10, 170);
                    Wall[7].wallRect = new Rectangle(845, 545, 10, 240);
                    Wall[8].wallRect = new Rectangle(1120, 750, 10, 95);
                    Wall[9].wallRect = new Rectangle(1050, 840, 10, 195);
                    Wall[10].wallRect = new Rectangle(250, 890, 10, 145);
                    Wall[11].wallRect = new Rectangle(580, 545, 10, 50);
                    //Wall[12].wallRect = new Rectangle(580, 745, 10, 115);


                    ground[0].GroundRect = new Rectangle(145, 142, 95, 10);
                    ground[1].GroundRect = new Rectangle(25, 233, 95, 10);
                    ground[2].GroundRect = new Rectangle(160, 320, 95, 10);
                    ground[3].GroundRect = new Rectangle(330, 260, 95, 10);
                    ground[4].GroundRect = new Rectangle(0, 540, 1280, 10);
                    ground[5].GroundRect = new Rectangle(480, 350, 155, 10);
                    ground[6].GroundRect = new Rectangle(25, 390, 95, 10);
                    ground[7].GroundRect = new Rectangle(195, 460, 95, 10);
                    ground[8].GroundRect = new Rectangle(710, 340, 460, 10);
                    ground[9].GroundRect = new Rectangle(950, 480, 135, 10);
                    ground[10].GroundRect = new Rectangle(810, 260, 95, 10);
                    ground[11].GroundRect = new Rectangle(540, 230, 185, 10);
                    ground[12].GroundRect = new Rectangle(1035, 185, 135, 10);
                    ground[13].GroundRect = new Rectangle(580, 855, 115, 10);
                    ground[14].GroundRect = new Rectangle(740, 910, 100, 10);
                    ground[15].GroundRect = new Rectangle(910, 910, 100, 10);
                    ground[16].GroundRect = new Rectangle(740, 780, 115, 10);
                    ground[17].GroundRect = new Rectangle(1060, 840, 145, 10);
                    ground[18].GroundRect = new Rectangle(25, 810, 95, 10);
                    ground[19].GroundRect = new Rectangle(175, 690, 115, 10);
                    ground[20].GroundRect = new Rectangle(405, 745, 185, 10);
                    ground[21].GroundRect = new Rectangle(335, 940, 95, 10);
                    ground[22].GroundRect = new Rectangle(95, 895, 115, 10);

                }
                if (player.CurrentLevel == 3)
                {
                    //Array.Resize(ref portal, player.GetPortalAmount());
                    //Array.Resize(ref Wall, player.GetWallAmount());
                    //Array.Resize(ref spike, player.GetSpikeAmount());
                    //Array.Resize(ref ground, player.GetGroundAmount())
                    for (int i = 0; i < Wall.Length; i++)
                    {
                        Wall[i] = new wall(this.Content.Load<Texture2D>("wall"));
                    }

                    for (int i = 0; i < ground.Length; i++)
                    {
                        ground[i] = new Ground(this.Content.Load<Texture2D>("wall"));
                    }


                    for (int i = 0; i < spike.Length; i++)
                    {
                        spike[i] = new Spike(this.Content.Load<Texture2D>("spike"));
                    }
                    Wall[0].wallRect = new Rectangle(350, 110, 10, 100);
                    Wall[1].wallRect = new Rectangle(565, 185, 10, 180);
                    Wall[2].wallRect = new Rectangle(1080, 105, 10, 95);
                    Wall[3].wallRect = new Rectangle(860, 20, 10, 300);
                    Wall[4].wallRect = new Rectangle(880, 555, 10, 4670);
                    Wall[5].wallRect = new Rectangle(230, 924, 10, 100);
                    Wall[6].wallRect = new Rectangle(630, 475, 10, 305);
                    Wall[7].wallRect = new Rectangle(360, 605, 10, 120);
                    Wall[8].wallRect = new Rectangle(265, 470, 10, 180);
                    Wall[9].wallRect = new Rectangle(120, 650, 10, 140);
                    Wall[10].wallRect = new Rectangle(305, 800, 10, 100);
                    Wall[11].wallRect = new Rectangle(740, 320, 10, 235);

                    ground[0].GroundRect = new Rectangle(15, 450, 120, 10);
                    ground[1].GroundRect = new Rectangle(445, 65, 230, 10);
                    ground[2].GroundRect = new Rectangle(135, 315, 160, 10);
                    ground[3].GroundRect = new Rectangle(370, 380, 130, 10);
                    ground[4].GroundRect = new Rectangle(1090, 190, 175, 10);
                    ground[5].GroundRect = new Rectangle(750, 320, 290, 10);
                    ground[6].GroundRect = new Rectangle(740, 555, 140, 10);
                    ground[7].GroundRect = new Rectangle(1135, 480, 130, 10);
                    ground[8].GroundRect = new Rectangle(930, 590, 120, 10);
                    ground[9].GroundRect = new Rectangle(900, 900, 100, 10);
                    ground[10].GroundRect = new Rectangle(1075, 735, 150, 10);
                    ground[11].GroundRect = new Rectangle(90, 150, 155, 10);
                    ground[12].GroundRect = new Rectangle(735, 820, 120, 10);
                    ground[13].GroundRect = new Rectangle(465, 780, 175, 10);
                    ground[14].GroundRect = new Rectangle(490, 465, 150, 10);
                    ground[15].GroundRect = new Rectangle(15, 790, 300, 10);
                    ground[16].GroundRect = new Rectangle(305, 890, 180, 10);
                    ground[17].GroundRect = new Rectangle(120, 640, 150, 10);
                    ground[18].GroundRect = new Rectangle(370, 605, 190, 10);

                    portal[1].PortalRect = new Rectangle(1160, 113, 60, 80);
                    portal[0].PortalRect = new Rectangle(100, 65, 60, 80);
                    portal[2].PortalRect = new Rectangle(1145, 945, 60, 80);


                    //spike[0].SpikeRect = new Rectangle(150, 965, 60, 60);
                    //spike[0].SpikeRect = new Rectangle(365, 545, 60, 60);
                    spike[1].SpikeRect = new Rectangle(795, 965, 60, 60);
                    spike[2].SpikeRect = new Rectangle(550, 965, 60, 60);
                    spike[3].SpikeRect = new Rectangle(680, 965, 60, 60);
                    spike[4].SpikeRect = new Rectangle(1050, 970, 60, 60);
                }
                if (player.CurrentLevel == 4)
                {
                    //Array.Resize(ref portal, player.GetPortalAmount());
                    //Array.Resize(ref Wall, player.GetWallAmount());
                    //Array.Resize(ref spike, player.GetSpikeAmount());
                    //Array.Resize(ref ground, player.GetGroundAmount());

                    spike[0].SpikeRect = new Rectangle(290, 195, 25, 25);
                    spike[1].SpikeRect = new Rectangle(190, 375, 30, 30);
                    spike[2].SpikeRect = new Rectangle(0, 595, 40, 90);
                    spike[3].SpikeRect = new Rectangle(10, 940, 60, 90);
                    spike[4].SpikeRect = new Rectangle(775, 985, 40, 40);
                    spike[5].SpikeRect = new Rectangle(965, 995, 30, 30);
                    //spike[6].SpikeRect = new Rectangle(920, 780, 50, 50);
                    spike[6] = null;
                    spike[7].SpikeRect = new Rectangle(1200, 780, 50, 50);

                    portal[2].PortalRect = new Rectangle(680, 450, 60, 80);
                    portal[1].PortalRect = new Rectangle(570, 250, 60, 80);
                    portal[0].PortalRect = new Rectangle(1140, 920, 60, 80);


                    Wall[0].wallRect = new Rectangle(220, 230, 10, 630);
                    Wall[1].wallRect = new Rectangle(520, 230, 10, 620);
                    Wall[2].wallRect = new Rectangle(750, 720, 10, 350);
                    Wall[3].wallRect = new Rectangle(640, 0, 10, 380);
                    Wall[4].wallRect = new Rectangle(750, 380, 10, 190);
                    Wall[5].wallRect = new Rectangle(380, 400, 10, 630);
                    Wall[6].wallRect = new Rectangle(150, 755, 10, 70);
                    Wall[7].wallRect = new Rectangle(150, 930, 10, 270);
                    //Wall[8].wallRect = new Rectangle(230, 930, 10, 270);

                    Wall[8] = null;
                    Wall[9].wallRect = new Rectangle(1110, 130, 10, 520);
                    Wall[10].wallRect = new Rectangle(1000, 420, 10, 80);
                    Wall[11].wallRect = new Rectangle(960, 500, 10, 90);
                    Wall[12].wallRect = new Rectangle(870, 580, 10, 140);
                    Wall[13].wallRect = new Rectangle(1050, 130, 10, 300);
                    Wall[14].wallRect = new Rectangle(1020, 690, 10, 145);
                    Wall[15].wallRect = new Rectangle(870, 830, 10, 95);

                    ground[0].GroundRect = new Rectangle(520, 840, 110, 10);
                    ground[1].GroundRect = new Rectangle(670, 920, 80, 10);
                    ground[2].GroundRect = new Rectangle(640, 720, 240, 10);
                    ground[3].GroundRect = new Rectangle(520, 560, 240, 10);
                    ground[4].GroundRect = new Rectangle(600, 370, 330, 10);
                    ground[5].GroundRect = new Rectangle(120, 405, 100, 10);
                    ground[6].GroundRect = new Rectangle(0, 290, 100, 10);
                    ground[7].GroundRect = new Rectangle(0, 485, 90, 10);
                    ground[8].GroundRect = new Rectangle(0, 685, 90, 10);
                    ground[9].GroundRect = new Rectangle(140, 575, 80, 10);
                    ground[10].GroundRect = new Rectangle(150, 755, 70, 10);
                    ground[11].GroundRect = new Rectangle(220, 595, 70, 10);
                    ground[12].GroundRect = new Rectangle(220, 445, 70, 10);
                    ground[13].GroundRect = new Rectangle(220, 795, 80, 10);
                    ground[14].GroundRect = new Rectangle(300, 900, 80, 10);
                    ground[15].GroundRect = new Rectangle(300, 685, 80, 10);
                    //ground[16].GroundRect = new Rectangle(340, 515, 50, 10);

                    ground[16] = null;
                    ground[17].GroundRect = new Rectangle(820, 645, 50, 10);
                    ground[18].GroundRect = new Rectangle(960, 495, 50, 10);
                    ground[19].GroundRect = new Rectangle(1000, 420, 50, 10);
                    ground[20].GroundRect = new Rectangle(870, 580, 90, 10);
                    ground[21].GroundRect = new Rectangle(1050, 130, 70, 10);
                    ground[22].GroundRect = new Rectangle(920, 140, 70, 10);
                    ground[23].GroundRect = new Rectangle(770, 190, 100, 10);
                    ground[24].GroundRect = new Rectangle(640, 255, 100, 10);
                    ground[25].GroundRect = new Rectangle(960, 690, 70, 10);
                    ground[26].GroundRect = new Rectangle(870, 830, 595, 10);
                    ground[27].GroundRect = new Rectangle(310, 520, 70, 10);
                    ground[28].GroundRect = new Rectangle(220, 220, 430, 10);
                }
                if (player.CurrentLevel == 2)
                {
                    //Array.Resize(ref portal, player.GetPortalAmount());
                    //Array.Resize(ref Wall, player.GetWallAmount());
                    //Array.Resize(ref spike, player.GetSpikeAmount());
                    //Array.Resize(ref ground, player.GetGroundAmount());

                    portal[2].PortalRect = new Rectangle(70, 900, 60, 80);
                    portal[1].PortalRect = new Rectangle(550, 320, 60, 80);
                    portal[0].PortalRect = new Rectangle(1040, 650, 60, 80);
                    //portal[4].PortalRect = new Rectangle(240, 900, 60, 80);

                    for (int i = 0; i < 2; i++)
                    {
                        spike[i] = null;
                    }

                    for (int i = 9; i < 12; i++)
                    {
                        Wall[i] = null;
                    }

                    for (int i = 15; i < 23; i++)
                    {
                        ground[i] = null;
                    }


                    Wall[0].wallRect = new Rectangle(200, 0, 10, 270);
                    Wall[1].wallRect = new Rectangle(370, 260, 10, 80);
                    Wall[2].wallRect = new Rectangle(520, 220, 10, 240);
                    Wall[3].wallRect = new Rectangle(370, 460, 10, 80);
                    Wall[4].wallRect = new Rectangle(180, 540, 10, 500);
                    Wall[5].wallRect = new Rectangle(665, 270, 10, 125);
                    Wall[6].wallRect = new Rectangle(990, 495, 10, 250);
                    Wall[7].wallRect = new Rectangle(1135, 235, 10, 500);
                    Wall[8].wallRect = new Rectangle(940, 0, 10, 490);


                    ground[0].GroundRect = new Rectangle(0, 260, 380, 10);
                    ground[1].GroundRect = new Rectangle(445, 215, 95, 10);
                    ground[2].GroundRect = new Rectangle(370, 450, 150, 10);
                    ground[3].GroundRect = new Rectangle(180, 530, 200, 10);
                    ground[4].GroundRect = new Rectangle(0, 710, 90, 10);
                    ground[5].GroundRect = new Rectangle(670, 385, 70, 10);
                    ground[6].GroundRect = new Rectangle(630, 270, 70, 10);
                    ground[7].GroundRect = new Rectangle(675, 605, 140, 10);
                    ground[8].GroundRect = new Rectangle(790, 485, 160, 10);
                    ground[9].GroundRect = new Rectangle(800, 725, 115, 10);
                    ground[10].GroundRect = new Rectangle(650, 805, 165, 10);
                    ground[11].GroundRect = new Rectangle(530, 895, 165, 10);
                    ground[12].GroundRect = new Rectangle(400, 965, 165, 10);
                    ground[13].GroundRect = new Rectangle(990, 735, 155, 10);
                    ground[14].GroundRect = new Rectangle(1135, 235, 200, 10);
                }
                if (player.CurrentLevel == 5)
                {

                    for (int i = 16; i < 33; i++)
                    {
                        ground[i] = new Ground(this.Content.Load<Texture2D>("wall"));
                    }

                    for (int i = 8; i < 14; i++)
                    {
                        Wall[i] = new wall(this.Content.Load<Texture2D>("wall"));
                    }

                    for (int i = 6; i < 9; i++)
                    {
                        spike[i] = new Spike(this.Content.Load<Texture2D>("spike"));
                    }

                    ground[0].GroundRect.X = 0;
                    ground[0].GroundRect.Y = 100;
                    ground[0].GroundRect.Height = 15;
                    ground[0].GroundRect.Width = 100;

                    ground[1].GroundRect.X = 15;
                    ground[1].GroundRect.Y = 100;
                    ground[1].GroundRect.Height = 15;
                    ground[1].GroundRect.Width = 50;

                    ground[2].GroundRect.X = 100;
                    ground[2].GroundRect.Y = 200;
                    ground[2].GroundRect.Height = 15;
                    ground[2].GroundRect.Width = 100;

                    ground[3].GroundRect.X = 200;
                    ground[3].GroundRect.Y = 200;
                    ground[3].GroundRect.Height = 15;
                    ground[3].GroundRect.Width = 100;

                    ground[4].GroundRect.X = 190;
                    ground[4].GroundRect.Y = 100;
                    ground[4].GroundRect.Height = 15;
                    ground[4].GroundRect.Width = 100;

                    ground[5].GroundRect.X = 350;
                    ground[5].GroundRect.Y = 100;
                    ground[5].GroundRect.Height = 15;
                    ground[5].GroundRect.Width = 100;

                    ground[6].GroundRect.X = 670;
                    ground[6].GroundRect.Y = 350;
                    ground[6].GroundRect.Height = 15;
                    ground[6].GroundRect.Width = 100;

                    ground[7].GroundRect.X = 570;
                    ground[7].GroundRect.Y = 250;
                    ground[7].GroundRect.Height = 15;
                    ground[7].GroundRect.Width = 100;

                    ground[8].GroundRect.X = 570;
                    ground[8].GroundRect.Y = 50;
                    ground[8].GroundRect.Height = 15;
                    ground[8].GroundRect.Width = 100;

                    ground[9].GroundRect.X = 670;
                    ground[9].GroundRect.Y = 150;
                    ground[9].GroundRect.Height = 15;
                    ground[9].GroundRect.Width = 100;

                    ground[10].GroundRect.X = 0;
                    ground[10].GroundRect.Y = 500;
                    ground[10].GroundRect.Height = 15;
                    ground[10].GroundRect.Width = 785;

                    ground[11].GroundRect.X = 400;
                    ground[11].GroundRect.Y = 450;
                    ground[11].GroundRect.Height = 15;
                    ground[11].GroundRect.Width = 100;

                    ground[12].GroundRect.X = 400;
                    ground[12].GroundRect.Y = 270;
                    ground[12].GroundRect.Height = 15;
                    ground[12].GroundRect.Width = 180;

                    ground[13].GroundRect.X = 220;
                    ground[13].GroundRect.Y = 340;
                    ground[13].GroundRect.Height = 15;
                    ground[13].GroundRect.Width = 180;

                    ground[14].GroundRect.X = 60;
                    ground[14].GroundRect.Y = 280;
                    ground[14].GroundRect.Height = 15;
                    ground[14].GroundRect.Width = 100;

                    ground[15].GroundRect.X = 60;
                    ground[15].GroundRect.Y = 420;
                    ground[15].GroundRect.Height = 15;
                    ground[15].GroundRect.Width = 200;

                    ground[16].GroundRect.X = 770;
                    ground[16].GroundRect.Y = 250;
                    ground[16].GroundRect.Height = 15;
                    ground[16].GroundRect.Width = 510;

                    ground[17].GroundRect.X = 570;
                    ground[17].GroundRect.Y = 250;
                    ground[17].GroundRect.Height = 15;
                    ground[17].GroundRect.Width = 100;

                    ground[18].GroundRect.X = 865;
                    ground[18].GroundRect.Y = 500;
                    ground[18].GroundRect.Height = 15;
                    ground[18].GroundRect.Width = 85;

                    ground[19].GroundRect.X = 750;
                    ground[19].GroundRect.Y = 900;
                    ground[19].GroundRect.Height = 15;
                    ground[19].GroundRect.Width = 95;

                    ground[20].GroundRect.X = 865;
                    ground[20].GroundRect.Y = 800;
                    ground[20].GroundRect.Height = 15;
                    ground[20].GroundRect.Width = 85;

                    ground[21].GroundRect.X = 780;
                    ground[21].GroundRect.Y = 400;
                    ground[21].GroundRect.Height = 15;
                    ground[21].GroundRect.Width = 85;

                    ground[22].GroundRect.X = 750;
                    ground[22].GroundRect.Y = 725;
                    ground[22].GroundRect.Height = 15;
                    ground[22].GroundRect.Width = 85;

                    ground[23].GroundRect.X = 600;
                    ground[23].GroundRect.Y = 650;
                    ground[23].GroundRect.Height = 15;
                    ground[23].GroundRect.Width = 85;

                    ground[24].GroundRect.X = 780;
                    ground[24].GroundRect.Y = 600;
                    ground[24].GroundRect.Height = 15;
                    ground[24].GroundRect.Width = 85;

                    ground[25].GroundRect.X = 0;
                    ground[25].GroundRect.Y = 850;
                    ground[25].GroundRect.Height = 15;
                    ground[25].GroundRect.Width = 100;

                    ground[26].GroundRect.X = 120;
                    ground[26].GroundRect.Y = 925;
                    ground[26].GroundRect.Height = 15;
                    ground[26].GroundRect.Width = 85;

                    ground[27].GroundRect.X = 320;
                    ground[27].GroundRect.Y = 700;
                    ground[27].GroundRect.Height = 15;
                    ground[27].GroundRect.Width = 100;

                    ground[28].GroundRect.X = 320;
                    ground[28].GroundRect.Y = 950;
                    ground[28].GroundRect.Height = 15;
                    ground[28].GroundRect.Width = 150;

                    ground[29].GroundRect.X = 1125;
                    ground[29].GroundRect.Y = 900;
                    ground[29].GroundRect.Height = 15;
                    ground[29].GroundRect.Width = 150;

                    ground[30].GroundRect.X = 1150;
                    ground[30].GroundRect.Y = 150;
                    ground[30].GroundRect.Height = 15;
                    ground[30].GroundRect.Width = 150;

                    ground[31].GroundRect.X = 950;
                    ground[31].GroundRect.Y = 650;
                    ground[31].GroundRect.Height = 15;
                    ground[31].GroundRect.Width = 120;

                    ground[32].GroundRect.X = 1065;
                    ground[32].GroundRect.Y = 400;
                    ground[32].GroundRect.Height = 15;
                    ground[32].GroundRect.Width = 100;

                    ground[33].GroundRect.X = 100;
                    ground[33].GroundRect.Y = 750;
                    ground[33].GroundRect.Height = 15;
                    ground[33].GroundRect.Width = 100;

                    Wall[0].wallRect.X = 100;
                    Wall[0].wallRect.Y = 100;
                    Wall[0].wallRect.Height = 100;
                    Wall[0].wallRect.Width = 15;

                    Wall[1].wallRect.X = 450;
                    Wall[1].wallRect.Y = 100;
                    Wall[1].wallRect.Height = 100;
                    Wall[1].wallRect.Width = 15;

                    Wall[2].wallRect.X = 570;
                    Wall[2].wallRect.Y = 0;
                    Wall[2].wallRect.Height = 350;
                    Wall[2].wallRect.Width = 15;

                    Wall[3].wallRect.X = 770;
                    Wall[3].wallRect.Y = 65;
                    Wall[3].wallRect.Height = 450;
                    Wall[3].wallRect.Width = 15;

                    Wall[4].wallRect.X = 570;
                    Wall[4].wallRect.Y = 450;
                    Wall[4].wallRect.Height = 50;
                    Wall[4].wallRect.Width = 15;

                    Wall[5].wallRect.X = 400;
                    Wall[5].wallRect.Y = 270;
                    Wall[5].wallRect.Height = 85;
                    Wall[5].wallRect.Width = 15;

                    Wall[6].wallRect.X = 60;
                    Wall[6].wallRect.Y = 280;
                    Wall[6].wallRect.Height = 150;
                    Wall[6].wallRect.Width = 15;

                    Wall[7].wallRect.X = 600;
                    Wall[7].wallRect.Y = 900;
                    Wall[7].wallRect.Height = 150;
                    Wall[7].wallRect.Width = 15;

                    Wall[8].wallRect.X = 950;
                    Wall[8].wallRect.Y = 400;
                    Wall[8].wallRect.Height = 800;
                    Wall[8].wallRect.Width = 15;

                    Wall[9].wallRect.X = 1000;
                    Wall[9].wallRect.Y = 0;
                    Wall[9].wallRect.Height = 150;
                    Wall[9].wallRect.Width = 15;

                    Wall[10].wallRect.X = 200;
                    Wall[10].wallRect.Y = 700;
                    Wall[10].wallRect.Height = 350;
                    Wall[10].wallRect.Width = 15;

                    Wall[11].wallRect.X = 415;
                    Wall[11].wallRect.Y = 500;
                    Wall[11].wallRect.Height = 215;
                    Wall[11].wallRect.Width = 15;

                    Wall[12].wallRect.X = 460;
                    Wall[12].wallRect.Y = 950;
                    Wall[12].wallRect.Height = 100;
                    Wall[12].wallRect.Width = 15;

                    Wall[13].wallRect.X = 1070;
                    Wall[13].wallRect.Y = 650;
                    Wall[13].wallRect.Height = 100;
                    Wall[13].wallRect.Width = 15;

                    Wall[14].wallRect.X = 1150;
                    Wall[14].wallRect.Y = 250;
                    Wall[14].wallRect.Height = 150;
                    Wall[14].wallRect.Width = 15;

                    spike[0].SpikeRect.X = 135;
                    spike[0].SpikeRect.Y = 150;
                    spike[0].SpikeRect.Height = 50;
                    spike[0].SpikeRect.Width = 50;

                    spike[1].SpikeRect.X = 250;
                    spike[1].SpikeRect.Y = 150;
                    spike[1].SpikeRect.Height = 50;
                    spike[1].SpikeRect.Width = 50;

                    spike[2].SpikeRect.X = 150;
                    spike[2].SpikeRect.Y = 370;
                    spike[2].SpikeRect.Height = 50;
                    spike[2].SpikeRect.Width = 50;

                    spike[3].SpikeRect.X = 250;
                    spike[3].SpikeRect.Y = 975;
                    spike[3].SpikeRect.Height = 50;
                    spike[3].SpikeRect.Width = 50;

                    spike[4].SpikeRect.X = 500;
                    spike[4].SpikeRect.Y = 975;
                    spike[4].SpikeRect.Height = 50;
                    spike[4].SpikeRect.Width = 50;

                    spike[5].SpikeRect.X = 650;
                    spike[5].SpikeRect.Y = 975;
                    spike[5].SpikeRect.Height = 50;
                    spike[5].SpikeRect.Width = 50;

                    spike[6].SpikeRect.X = 700;
                    spike[6].SpikeRect.Y = 975;
                    spike[6].SpikeRect.Height = 50;
                    spike[6].SpikeRect.Width = 50;

                    spike[7].SpikeRect.X = 850;
                    spike[7].SpikeRect.Y = 975;
                    spike[7].SpikeRect.Height = 50;
                    spike[7].SpikeRect.Width = 50;

                    spike[8].SpikeRect.X = 900;
                    spike[8].SpikeRect.Y = 975;
                    spike[8].SpikeRect.Height = 50;
                    spike[8].SpikeRect.Width = 50;

                    spike[9].SpikeRect.X = 970;
                    spike[9].SpikeRect.Y = 975;
                    spike[9].SpikeRect.Height = 50;
                    spike[9].SpikeRect.Width = 50;

                    portal[2].PortalRect.X = 1150;
                    portal[2].PortalRect.Y = 945;
                    portal[2].PortalRect.Height = 80;
                    portal[2].PortalRect.Width = 60;

                    portal[0].PortalRect.X = 1150;
                    portal[0].PortalRect.Y = 70;
                    portal[0].PortalRect.Height = 80;
                    portal[0].PortalRect.Width = 60;

                    portal[1].PortalRect.X = 50;
                    portal[1].PortalRect.Y = 945;
                    portal[1].PortalRect.Height = 80;
                    portal[1].PortalRect.Width = 60;

                    //portal[3].PortalRect.X = 20;
                    //portal[3].PortalRect.Y = 20;
                    //portal[3].PortalRect.Height = 80;
                    //portal[3].PortalRect.Width = 60;

                }
                if (player.CurrentLevel == 6)
                {
                    for (int i = 0; i < portal.Length; i++)
                    {
                        portal[i] = null;
                    }

                    for (int i = 0; i < Wall.Length; i++)
                    {
                        Wall[i] = null;
                    }

                    for (int i = 0; i < ground.Length; i++)
                    {
                        ground[i] = null;
                    }

                    for (int i = 0; i < spike.Length; i++)
                    {
                        spike[i] = null;
                    }
                    player.PlayerDead = true;
                    gameOn = false;
                }
                for (int i = 0; i < portal.Length; i++)
                {
                    if (portal[i] != null)
                    {
                        if (player.CharRect.Intersects(portal[i].PortalRect))
                        {

                            if (i == (portal.Length - 1))
                            {
                                //player.lastPortal = -1;

                                player.CurrentLevel += 1;
                                /*Array.Resize(ref portal, player.GetPortalAmount());
                                Array.Resize(ref Wall, player.GetWallAmount());
                                Array.Resize(ref spike, player.GetSpikeAmount());
                                Array.Resize(ref ground, player.GetGroundAmount());*/
                                player.PlayerPos();
                                player.CharRect.X = player.XYPOS[0];
                                player.CharRect.Y = player.XYPOS[1];
                            }
                            else if ((i + 1) != (portal.Length - 1))
                            {
                                //player.lastPortal++;
                                player.CharRect.X = portal[i + 1].PortalRect.X + 10;
                                player.CharRect.Y = portal[i + 1].PortalRect.Y;
                            }
                            else if ((i + 1) != (portal.Length - 1))
                            {
                                //player.lastPortal--;
                                player.CharRect.X = portal[i + 1].PortalRect.X + (player.CharRect.Width + 10);
                                player.CharRect.Y = portal[i + 1].PortalRect.Y;
                            }
                            i = (portal.Length - 1);
                        }
                    }
                }



                for (int i = 0; i < Wall.Length; i++)
                {
                    if (Wall[i] != null)
                    {
                        if (player.CharRect.Intersects(Wall[i].wallRect) && !IsAbove(i, intsec) && IsCharAbove(i))
                        {
                            
                            if ((player.CharRect.X - (player.CharRect.Width) > Wall[i].wallRect.Width) && player.directionX == "left")
                            {
                                //player.canWalkLeft = false;
                                player.CharRect.X = (Wall[i].wallRect.X + Wall[i].wallRect.Width);

                            }
                            else
                            {
                                player.canWalkRight = true;
                            }
                            //gets stuck now
                            if ((player.CharRect.X + (player.CharRect.Width) > Wall[i].wallRect.Width) && player.directionX == "right")
                            {
                                //player.canWalkRight = false;
                                player.CharRect.X = (Wall[i].wallRect.X - (player.CharRect.Width));
                            }
                            else
                            {
                                player.canWalkRight = true;
                            }
                            player.PlayerintWall = true;
                        }
                        else if (!player.CharRect.Intersects(Wall[i].wallRect))
                        {
                            player.PlayerintWall = false;
                            player.canWalkLeft = true;
                            player.canWalkRight = true;
                        }
                        //intsec < 0 added fix hitting bottom ground to tele up to top of the wall
                        if (player.CharRect.Intersects(Wall[i].wallRect) && player.direction != "up" && intsec < 0 && !IsCharAbove(i) && (Wall[i].wallRect.Top - (player.CharRect.Height + 4) > 0))
                        {
                            player.PlayerintWall = true;
                            player.OnGround = true;
                            player.CharRect.Y = Wall[i].wallRect.Top - (player.CharRect.Height + 4);
                            player.PlayerSpeedY = -5;
                            player.direction = "";
                            i = (Wall.Length - 1);
                        }
                        else
                        {
                            player.PlayerintWall = false;
                        }
                            
                        }
                    }

                for (int i = 0; i < spike.Length; i++)
                {
                    if (spike[i] != null)
                    {
                        if (player.CharRect.Intersects(spike[i].SpikeRect) && player.direction != "up")
                        {
                            //player.PlayerDead = true;
                            player.PlayerPos();
                            player.PlayerintWall = false;
                            player.OnGround = false;
                            player.CharRect.X = player.XYPOS[0];
                            player.CharRect.Y = player.XYPOS[1];

                        }
                    }
                }

                for (int i = 0; i < ground.Length; i++)
                {
                    if (ground[i] != null)
                    {
                        if (player.CharRect.Intersects(ground[i].GroundRect) && player.direction != "up")
                        {
                            player.OnGround = true;
                            intsec = i;
                            player.CharRect.Y = ground[i].GroundRect.Top - (player.CharRect.Height - 1);
                            player.PlayerSpeedY = -5;
                            player.direction = "";
                            i = (ground.Length - 1);
                        }
                        else if (player.CharRect.Intersects(ground[i].GroundRect) && player.direction == "up")
                        {
                            player.CharRect.Y = ground[i].GroundRect.Bottom;
                            intsec = i;
                            player.direction = "down";
                            player.PlayerSpeedY = 0;
                            i = (ground.Length - 1);
                        }
                        else if (player.direction == "" && player.CharRect.Y != (GraphicsDevice.Viewport.Height - player.CharRect.Height))
                        {
                            player.direction = "down";
                        }
                        else
                        {
                            intsec = -1;
                            player.OnGround = false;
                        }
                    }
                }

                if (ks.IsKeyDown(Keys.Space))
                {
                    if (player.PlayerSpeedY == -16)
                    {
                        player.direction = "up";
                        player.PlayerSpeedY = 15;
                    }
                    else
                    {
                        for (int i = 0; i < ground.Length; i++)
                        {
                            if (ground[i] != null)
                            {
                                if ((player.CharRect.Intersects(ground[i].GroundRect) || player.PlayerintWall) && player.direction != "up")
                                {
                                    //fixes faze throughs?
                                    //i = ground.Length - 1;
                                    player.direction = "up";
                                    player.PlayerSpeedY = 15;
                                }
                            }
                        }
                    }
                 
                }
                if (player.canWalkLeft || player.canWalkRight)
                {
                    time += gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (time > 200)
                    {
                        if (player.currentframe == 2)
                        {
                            player.currentframe = 1;
                        }
                        else
                        {
                            player.currentframe++;
                        }
                        time = 0;
                    }

                }
                oldpad1 = pad1;

                player.update();
            }
            base.Update(gameTime);
        }

        public bool IsAbove(int i, int j) {
            if (j > 0)
            {
                if (Wall[i].wallRect.Y > ground[j].GroundRect.Y)
                {
                    return true;
                }
                else
                {
                    //Console.WriteLine("Wall - fail " + Wall[i].wallRect.Y);
                    //Console.WriteLine("ground - fail  " + ground[j].GroundRect.Y);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool IsCharAbove(int i)
        {
           if (player.CharRect.Y > Wall[i].wallRect.Y)
           {
                return true;
           }
           else
           {
                return false;
            }
        }
        void PlayMainMusic()
        {

            if (musicInstance == null)
            {
                musicInstance = song.CreateInstance();
                musicInstance.IsLooped = true;
                musicInstance.Play();

            }
            else if (musicInstance != null)
            {
                musicInstance.Play();
            }
        }
        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            if (gameOn)
            {
                if (player.CurrentLevel == 5)
                {
                    GraphicsDevice.Clear(Color.Gold);
                }
                if (player.CurrentLevel == 4)
                {
                    GraphicsDevice.Clear(new Color(210, 45, 45));
                }
                if (player.CurrentLevel == 3)
                {
                    GraphicsDevice.Clear(Color.Peru);
                }
                if (player.CurrentLevel == 2)
                {
                    GraphicsDevice.Clear(Color.Green);
                }
                if (player.CurrentLevel == 1)
                {
                    GraphicsDevice.Clear(Color.LightSkyBlue);
                }

                for (int i = 0; i < ground.Length; i++)
                {
                    if (ground[i] != null)
                    {
                        ground[i].Draw(spriteBatch);
                    }
                }
                for (int i = 0; i < portal.Length; i++)
                {
                    if (portal[i] != null)
                    {
                        if (i == (portal.Length - 1))
                        {
                            portal[i].Draw(spriteBatch, this.Content.Load<Texture2D>("portal 2"));
                        }
                        else
                        {
                            portal[i].Draw(spriteBatch, this.Content.Load<Texture2D>("portal 1"));
                        }
                    }
                }
                for (int i = 0; i < Wall.Length; i++)
                {
                    if (Wall[i] != null)
                    {
                        Wall[i].Draw(spriteBatch);
                    }
                }
                for (int i = 0; i < spike.Length; i++)
                {
                    if (spike[i] != null)
                    {
                        spike[i].Draw(spriteBatch);
                    }
                }

                player.draw(spriteBatch);
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
                Vector2 nowVector = new Vector2((graphics.GraphicsDevice.Viewport.X / 2 + 412.5f), 50);
                Vector2 nowVector1 = new Vector2((graphics.GraphicsDevice.Viewport.X / 2 + 500), 100);
                Vector2 nowVector2 = new Vector2((graphics.GraphicsDevice.Viewport.X / 2 + 300), 150);
                Vector2 nowVector3 = new Vector2((graphics.GraphicsDevice.Viewport.X / 2 + 450), 200);
                Vector2 nowVector4 = new Vector2((graphics.GraphicsDevice.Viewport.X / 2 + 500), 800);
                Vector2 nowVector5 = new Vector2((graphics.GraphicsDevice.Viewport.X / 2 + 350), 250);
                spriteBatch.Draw(this.Content.Load<Texture2D>("logo") , new Rectangle((graphics.GraphicsDevice.Viewport.X / 2 + 450), 400, 450, 350), Color.White);
                spriteBatch.DrawString(font, "Welcome to Gateway to Your World!", nowVector, Color.White);
                spriteBatch.DrawString(font, "Controls are simple:", nowVector1, Color.White);
                spriteBatch.DrawString(font, "Use the joystick to control player movements", nowVector2, Color.White);
                spriteBatch.DrawString(font, "Use the green button to Jump", nowVector3, Color.White);
                spriteBatch.DrawString(font, "Use the red button to restart the level", nowVector5, Color.White);
                spriteBatch.DrawString(font, "Press Start to Begin", nowVector4, Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
