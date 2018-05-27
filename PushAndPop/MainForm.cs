using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameLib;

namespace PushAndPop
{
    public partial class MainForm : Form
    {
        private Square[,] squares = new Square[5, 5];

        private int startX = 10;
        private int startY = 10;
        private int boxWidth = 100;
        private int boxHeight = 100;
        private int gutter = 10;

        private int positionX = 2;
        private int positionY = 2;

        public MainForm()
        {
            InitializeComponent();
            Render();
            //防止圖片閃爍
            this.DoubleBuffered = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void Render()
        {
            Initialize(squares);
            squares[positionX, positionY].Image = 1;
            SquareGenerate(5);
            DrawPictureBox();
        }

        private void Initialize(Square[,] squares)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    squares[row, col] = new Square();
                    squares[row, col].Image = 0;
                    squares[row, col].X = startX + col * (boxWidth + gutter);
                    squares[row, col].Y = startY + row * (boxHeight + gutter);
                }
            }
        }

        private void DrawPictureBox()
        {
            this.Controls.Clear();
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (squares[row, col].Image == 1)
                    {
                        positionX = row;
                        positionY = col;
                    }
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Location = new Point(squares[row, col].X, squares[row, col].Y);
                    pictureBox.Name = "pictureBox";
                    pictureBox.Size = new Size(boxWidth, boxHeight);
                    pictureBox.Image = SelectImage(squares[row, col].Image);
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    this.Controls.Add(pictureBox);
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            int squareCount = 0;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    for (int index = 0; index <= positionY; index++)
                    {
                        if (squares[positionX, index].Image == -1)
                            squareCount++;
                    }
                    for (int index = 0; index <= positionY; index++)
                    {
                        if (squareCount > 0)
                        {
                            squares[positionX, index].Image = -1;
                            squareCount--;
                        }
                        else if (squareCount == 0)
                        {
                            squares[positionX, index].Image = 1;
                            squareCount--;
                        }
                        else
                            squares[positionX, index].Image = 0;
                    }
                    break;

                case Keys.Right:
                    for (int index = 4; index >= positionY; index--)
                    {
                        if (squares[positionX, index].Image == -1)
                            squareCount++;
                    }
                    for (int index = 4; index >= positionY; index--)
                    {
                        if (squareCount > 0)
                        {
                            squares[positionX, index].Image = -1;
                            squareCount--;
                        }
                        else if (squareCount == 0)
                        {
                            squares[positionX, index].Image = 1;
                            squareCount--;
                        }
                        else
                            squares[positionX, index].Image = 0;
                    }
                    break;

                case Keys.Up:
                    for (int index = 0; index <= positionX; index++)
                    {
                        if (squares[index, positionY].Image == -1)
                            squareCount++;
                    }
                    for (int index = 0; index <= positionX; index++)
                    {
                        if (squareCount > 0)
                        {
                            squares[index, positionY].Image = -1;
                            squareCount--;
                        }
                        else if (squareCount == 0)
                        {
                            squares[index, positionY].Image = 1;
                            squareCount--;
                        }
                        else
                            squares[index, positionY].Image = 0;
                    }
                    break;

                case Keys.Down:
                    for (int index = 4; index >= positionX; index--)
                    {
                        if (squares[index, positionY].Image == -1)
                            squareCount++;
                    }
                    for (int index = 4; index >= positionX; index--)
                    {
                        if (squareCount > 0)
                        {
                            squares[index, positionY].Image = -1;
                            squareCount--;
                        }
                        else if (squareCount == 0)
                        {
                            squares[index, positionY].Image = 1;
                            squareCount--;
                        }
                        else
                            squares[index, positionY].Image = 0;
                    }
                    break;
            }
            SquareGenerate(2);
            CheckLine();
            DrawPictureBox();
            GameOver();
        }

        private void SquareGenerate(int count)
        {
            Random random = new Random();
            int x, y;
            for (int i = 0; i < count; i++)
            {
                do
                {
                    x = random.Next(0, 5);
                    y = random.Next(0, 5);
                } while (squares[x, y].Image != 0);
                squares[x, y].Image = -1;
            }
        }

        private void CheckLine()
        {
            bool check;

            #region Row

            for (int i = 0; i < 5; i++)
            {
                check = true;
                for (int j = 0; j < 5; j++)
                {
                    if (squares[i, j].Image != -1)
                        check = false;
                }
                if (check == true)
                    squares[i, 0].Image = squares[i, 1].Image = squares[i, 2].Image = squares[i, 3].Image = squares[i, 4].Image = 0;
            }

            #endregion Row

            #region Col

            for (int i = 0; i < 5; i++)
            {
                check = true;
                for (int j = 0; j < 5; j++)
                {
                    if (squares[j, i].Image != -1)
                        check = false;
                }
                if (check == true)
                    squares[0, i].Image = squares[1, i].Image = squares[2, i].Image = squares[3, i].Image = squares[4, i].Image = 0;
            }

            #endregion Col
        }

        private void GameOver()
        {
            int count = 0;
            for (int index = 0; index < 5; index++)
            {
                if (squares[index, positionY].Image == -1)
                    count++;
                if (squares[positionX, index].Image == -1)
                    count++;
            }
            if (count == 8)
                MessageBox.Show("Game Over");
        }

        private Image SelectImage(int image)
        {
            switch (image)
            {
                case 1:
                    return Properties.Resources.MainSquare;

                case -1:
                    return Properties.Resources.Square;

                default:
                    return Properties.Resources.NormalSquare;
            }
        }
    }
}