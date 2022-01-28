using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static ExtendMath.EMath;

namespace Raycasting2021
{
    public partial class Form1 : Form
    {
        List<Cube> walls;
        List<Plane> ground;
        int width;
        int height;

        bool UP, DOWN, LEFT, RIGHT, SPACE;
        Stopwatch watch = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
            walls = new List<Cube>();
            ground = new List<Plane>();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            width = Width / 2;
            height = Height / 2;

            Random rnd = new Random();
            /*    MAP   BUILD    */
            for (int z = -50; z < 50; z += 4)
            {
                for (int i = -100; i < 100; i += 4)
                {
                    ground.Add(new Plane()
                    {
                        Color = Color.DodgerBlue,
                        Location = new Vector3(i, 4, z)
                    });
                    ground.Add(new Plane()
                    {
                        Color = Color.Cyan,
                        Location = new Vector3(i + 2, 4, z)
                    });
                    ground.Add(new Plane()
                    {
                        Color = Color.Cyan,
                        Location = new Vector3(i, 4, z+2)
                    });
                    ground.Add(new Plane()
                    {
                        Color = Color.DodgerBlue,
                        Location = new Vector3(i + 2, 4, z+2)
                    });
                }
            }
            for(int z = -50; z<50; z+=rnd.Next(1,10) * 2)
            {
                for(int x = -100; x<100; x+= rnd.Next(5, 10) * 2)
                {
                    byte r = (byte)rnd.Next(0, 255);
                    byte g = (byte)rnd.Next(0, 255);
                    byte b = (byte)rnd.Next(0, 255);
                    walls.Add(new Cube()
                    {
                        Color = Color.FromArgb(r,g,b),
                        Location = new Vector3(x, 2, z)
                    });
                    walls.Add(new Cube()
                    {
                        Color = Color.FromArgb(b, r, g),
                        Location = new Vector3(x, 0, z)
                    });
                }
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            walls = walls.OrderByDescending(t => Math.Abs(t.Distance())).ToList();
            //ground = ground.OrderByDescending(t => Math.Abs(t.Distance())).ToList();
            int len = ground.Count;
            for (int i = 0; i < len; i++)
            {
                ground[i].Draw(width, height, e.Graphics);
            }
            len = walls.Count;
            for(int i = 0; i< len; i++)
            {
                walls[i].Draw(width, height, e.Graphics);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    UP = true;
                    break;
                case Keys.Down:
                    DOWN = true;
                    break;
                case Keys.Left:
                    LEFT = true;
                    break;
                case Keys.Right:
                    RIGHT = true;
                    break;
                case Keys.Space:
                    SPACE = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    UP = false;
                    break;
                case Keys.Down:
                    DOWN = false;
                    break;
                case Keys.Left:
                    LEFT = false;
                    break;
                case Keys.Right:
                    RIGHT = false;
                    break;
                case Keys.Space:
                    SPACE = false;
                    break;
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            float ticks = watch.ElapsedTicks * 0.0001f;
            watch.Restart();
            if (UP) Camera.Location -= ForwardVector(-Camera.Yaw, 0) * ticks;
            if (DOWN) Camera.Location += ForwardVector(-Camera.Yaw, 0) * ticks;
            if (LEFT) Camera.Yaw += 0.0025f * ticks;
            if (RIGHT) Camera.Yaw -= 0.0025f * ticks;
            Invalidate();
        }
    }
}
