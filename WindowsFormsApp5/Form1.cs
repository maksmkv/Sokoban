using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        Graphics g;

        Bitmap image1 = new Bitmap(@"C:\Users\MAKSIM\source\repos\LoadLevel\WindowsFormsApp5\Image\wall.png");
        Bitmap image2 = new Bitmap(@"C:\Users\MAKSIM\source\repos\LoadLevel\WindowsFormsApp5\Image\ball.png");
        Bitmap image3 = new Bitmap(@"C:\Users\MAKSIM\source\repos\LoadLevel\WindowsFormsApp5\Image\null.png");
        Bitmap image4 = new Bitmap(@"C:\Users\MAKSIM\source\repos\LoadLevel\WindowsFormsApp5\Image\goal.png");
        Bitmap image5 = new Bitmap(@"C:\Users\MAKSIM\source\repos\LoadLevel\WindowsFormsApp5\Image\left.png");
        Bitmap image6 = new Bitmap(@"C:\Users\MAKSIM\source\repos\LoadLevel\WindowsFormsApp5\Image\ball_goal.png");

        private static int heroX;
        private static int heroY;

        public static int positionBall = 0;
        private static int countPoint = 0;

        int width = 20;
        int height = 12;

        private static int[,] map = new int[20, 12];

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            g = CreateGraphics();
            g.TranslateTransform(0, 35);
        }

        public void DrawHero()
        {
            g.DrawImage(image5, heroX * 32, heroY * 32, 32, 32);
        }

        public void GameOver()
        {
            String drawString = "Вы выйграли";
            Font drawFont = new Font("Arial", 25);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            if (positionBall == countPoint)
            {
                g.DrawString(drawString, drawFont, drawBrush, 100, 100);
            }
        }



        public void HideHero()
        {
            if (map[heroX, heroY] == 'p')
            {
                g.DrawImage(image4, heroX * 32, heroY * 32, 32, 32);
            }
            else
            {
                g.DrawImage(image3, heroX * 32, heroY * 32, 32, 32);
            }

        }

        public static bool HeroCanStep(int x, int y)
        {
            if (map[x, y] != 'x')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool HeroCanMoveBoxLeft(int x, int y, Graphics g, Bitmap image2, Bitmap image3, Bitmap image6)
        {
            if (map[x, y] == 'b')
            {
                if (map[x - 1, y] == ' ') //место свободно слева
                {
                    map[x, y] = ' ';
                    g.DrawImage(image3, x * 32, y * 32);
                    map[x - 1, y] = 'b';
                    g.DrawImage(image2, (x - 1) * 32, y * 32);
                    return true;
                }

                if (map[x - 1, y] == 'p')
                {
                    map[x, y] = 'g';
                    g.DrawImage(image6, (x - 1) * 32, y * 32);
                    positionBall = positionBall + 1;
                    return true;
                }

                else
                {
                    return false;
                }
            }
            return true;
        }



        public static bool HeroCanMoveBoxRight(int x, int y, Graphics g, Bitmap image2, Bitmap image3, Bitmap image6)
        {
            if (map[x, y] == 'b')
            {
                if (map[x + 1, y] == ' ') //место свободно справа
                {
                    map[x, y] = ' ';
                    g.DrawImage(image3, x * 32, y * 32);
                    map[x + 1, y] = 'b';
                    g.DrawImage(image2, (x + 1) * 32, y * 32);
                    return true;
                }

                if (map[x + 1, y] == 'p')
                {
                    map[x, y] = 'g';
                    g.DrawImage(image6, (x + 1) * 32, y * 32);
                    positionBall = positionBall + 1;
                    return true;
                }

                else
                {
                    return false;
                }
            }
            return true;
        }

        public static bool HeroCanMoveBoxUp(int x, int y, Graphics g, Bitmap image2, Bitmap image3, Bitmap image6)
        {
            if (map[x, y] == 'b')
            {
                if (map[x, y - 1] == ' ') //место свободно сверху
                {
                    map[x, y] = ' ';
                    g.DrawImage(image3, x * 32, y * 32);
                    map[x, y - 1] = 'b';
                    g.DrawImage(image2, x * 32, (y - 1) * 32);
                    return true;
                }


                if (map[x, y-1] == 'p')
                {
                    map[x, y] = 'g';
                    g.DrawImage(image6, x * 32, (y-1) * 32);
                    positionBall = positionBall + 1;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static bool HeroCanMoveBoxDown(int x, int y, Graphics g, Bitmap image2, Bitmap image3, Bitmap image6)
        {
            if (map[x, y] == 'b')
            {
                if (map[x, y + 1] == ' ') //место свободно снизу
                {
                    map[x, y] = ' ';
                    g.DrawImage(image3, x * 32, y * 32);
                    map[x, y + 1] = 'b';
                    g.DrawImage(image2, x * 32, (y + 1) * 32);
                    return true;
                }


                if (map[x, y + 1] == 'p')
                {
                    map[x, y] = 'g';
                    g.DrawImage(image6, x * 32, (y + 1) * 32);
                    positionBall = positionBall + 1;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


        public void DrawMap()
        {
            var lines = File.ReadAllLines(@"C:\Users\MAKSIM\source\repos\LoadLevel\WindowsFormsApp5\Level\level1.txt");

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    switch (lines[j][i])
                    {
                        case 'x':
                            g.DrawImage(image1, i * 32, j * 32, 32, 32);
                            map[i, j] = 'x';
                            break;
                        case 'b':
                            g.DrawImage(image2, i * 32, j * 32, 32, 32);
                            map[i, j] = 'b';
                            break;
                        case ' ':
                            g.DrawImage(image3, i * 32, j * 32, 32, 32);
                            map[i, j] = ' ';
                            break;
                        case 'p':
                            g.DrawImage(image4, i * 32, j * 32, 32, 32);
                            map[i, j] = 'p';
                            countPoint = countPoint + 1;
                            break;
                        case 's':
                            heroX = i; heroY = j;
                            map[i, j] = 's';
                            break;
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            GameOver();
            switch (keyData)
            {
                case Keys.Up:
                    if (HeroCanStep(heroX, heroY - 1))
                    {
                        if (HeroCanMoveBoxUp(heroX, heroY - 1, g, image2, image3, image6))
                        {
                            HideHero();
                            heroY--;
                            image5 = new Bitmap(@"C:\Users\MAKSIM\source\repos\LoadLevel\WindowsFormsApp5\Image\up.png");
                            g.DrawImage(image5, heroX * 32, heroY * 32);
                        }
                    }
                    break;

                case Keys.Down:
                    if (HeroCanStep(heroX, heroY + 1))
                    {
                        if (HeroCanMoveBoxDown(heroX, heroY + 1, g, image2, image3, image6))
                        {
                            HideHero();
                            heroY++;
                            image5 = new Bitmap(@"C:\Users\MAKSIM\source\repos\LoadLevel\WindowsFormsApp5\Image\down.png");
                            g.DrawImage(image5, heroX * 32, heroY * 32);
                        }
                    }
                    break;

                case Keys.Left:
                    if (HeroCanStep(heroX - 1, heroY))
                    {
                        if (HeroCanMoveBoxLeft(heroX - 1, heroY, g, image2, image3, image6))
                        {
                            HideHero();
                            heroX--;
                            image5 = new Bitmap(@"C:\Users\MAKSIM\source\repos\LoadLevel\WindowsFormsApp5\Image\left.png");
                            g.DrawImage(image5, heroX * 32, heroY * 32);
                        }
                    }
                    break;

                case Keys.Right:
                    if (HeroCanStep(heroX + 1, heroY))
                    {
                        if (HeroCanMoveBoxRight(heroX + 1, heroY, g, image2, image3, image6))
                        {
                            HideHero();
                            heroX++;
                            image5 = new Bitmap(@"C:\Users\MAKSIM\source\repos\LoadLevel\WindowsFormsApp5\Image\right.png");
                            g.DrawImage(image5, heroX * 32, heroY * 32);
                        }
                    }
                    break;

                case Keys.Escape: Application.Exit(); break;

                default: return base.ProcessCmdKey(ref msg, keyData);
            }
            return true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawMap();
            DrawHero();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameOver();
        }
    }
}
