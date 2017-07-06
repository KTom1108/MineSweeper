using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for FrmMain.xaml
    /// </summary>
    public partial class FrmMain : Window
    {
        private OptionEntity optionEntity;
        private FrmOption frmOption;

        private int[] bomb = null;
        public FrmMain()
        {
            InitializeComponent();
            frmOption = FrmOption.getInstance;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetOptionValue();            
        }

        private void GameStart() 
        {
            CreateArrForMineButton();
            SetDataForBombButton();
            SetDataForNumButton();
            CreateButton();

            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void GetOptionValue() 
        {
            frmOption.Owner = Window.GetWindow(this);

            if (optionEntity == null)
            {
                optionEntity = new OptionEntity();                
            }
            frmOption.SetOptionEntity(optionEntity);
            frmOption.ShowDialog();

            lblLevel.Content = CommonMethod.UseStringBuilder("Level : ", optionEntity.difficultyType, " Class");

            GameStart();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            frmOption.Close();
        }

        private void OptionMenu_Click(object sender, RoutedEventArgs e)
        {
            GetOptionValue();
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetDataForBombButton() 
        {
            var rd = new Random();
            int rdNum = 0;
            for (int i = 0; i < optionEntity.numForMine; i++)
            {
                rdNum = rd.Next(0, bomb.Length - 1);

                if (bomb[rdNum] == 9)
                {
                    i -= 1;
                }
                else 
                {
                    bomb[rdNum] = 9;
                }                
            }
        }       

        private void SetDataForNumButton() 
        {
            //for (int i = 0; i < bomb.Length; i++)
            //{
            //    if (bomb[i] == 9)
            //    {
            //        if (i == 0)
            //        {
            //            if (bomb[i + 1] != 9) { bomb[i + 1] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton] != 9) { bomb[i + optionEntity.hNumForButton] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton + 1] != 9) { bomb[i + optionEntity.hNumForButton + 1] += 1; }
            //        }
            //        else if (i == optionEntity.hNumForButton - 1)
            //        {
            //            if (bomb[i - 1] != 9) { bomb[i - 1] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton - 1] != 9) { bomb[i + optionEntity.hNumForButton - 1] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton] != 9) { bomb[i + optionEntity.hNumForButton] += 1; }
            //        }
            //        else if (i == optionEntity.hNumForButton * (optionEntity.vNumForButton - 1) + 1)
            //        {
            //            if (bomb[i - optionEntity.hNumForButton] != 9) { bomb[i - optionEntity.hNumForButton] += 1; }
            //            if (bomb[i - optionEntity.hNumForButton + 1] != 9) { bomb[i - optionEntity.hNumForButton + 1] += 1; }
            //            if (bomb[i + 1] != 9) { bomb[i + 1] += 1; }
            //        }
            //        else if (i == optionEntity.hNumForButton * optionEntity.vNumForButton - 1)
            //        {
            //            if (bomb[i - optionEntity.hNumForButton - 1] != 9) { bomb[i - optionEntity.hNumForButton - 1] += 1; }
            //            if (bomb[i - optionEntity.hNumForButton] != 9) { bomb[i - optionEntity.hNumForButton] += 1; }
            //            if (bomb[i - 1] != 9) { bomb[i - 1] += 1; }
            //        }
            //        else if (i > 0
            //            && i < optionEntity.hNumForButton - 1)
            //        {
            //            if (bomb[i - 1] != 9) { bomb[i - 1] += 1; }
            //            if (bomb[i + 1] != 9) { bomb[i + 1] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton - 1] != 9) { bomb[i + optionEntity.hNumForButton - 1] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton] != 9) { bomb[i + optionEntity.hNumForButton] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton + 1] != 9) { bomb[i + optionEntity.hNumForButton + 1] += 1; }
            //        }
            //        else if (i > optionEntity.hNumForButton * (optionEntity.vNumForButton - 1) + 1
            //            && i < optionEntity.hNumForButton * optionEntity.vNumForButton - 1)
            //        {
            //            if (bomb[i - 1] != 9) { bomb[i - 1] += 1; }
            //            if (bomb[i + 1] != 9) { bomb[i + 1] += 1; }
            //            if (bomb[i - optionEntity.hNumForButton - 1] != 9) { bomb[i - optionEntity.hNumForButton - 1] += 1; }
            //            if (bomb[i - optionEntity.hNumForButton] != 9) { bomb[i - optionEntity.hNumForButton] += 1; }
            //            if (bomb[i - optionEntity.hNumForButton + 1] != 9) { bomb[i - optionEntity.hNumForButton + 1] += 1; }
            //        }
            //        else if (i % optionEntity.hNumForButton == 0)
            //        {
            //            if (bomb[i - optionEntity.hNumForButton] != 9) { bomb[i - optionEntity.hNumForButton] += 1; }
            //            if (bomb[i - optionEntity.hNumForButton + 1] != 9) { bomb[i - optionEntity.hNumForButton + 1] += 1; }
            //            if (bomb[i + 1] != 9) { bomb[i + 1] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton] != 9) { bomb[i + optionEntity.hNumForButton] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton + 1] != 9) { bomb[i + optionEntity.hNumForButton + 1] += 1; }
            //        }
            //        else if (i % optionEntity.hNumForButton == optionEntity.hNumForButton - 1)
            //        {
            //            if (bomb[i - optionEntity.hNumForButton - 1] != 9) { bomb[i - optionEntity.hNumForButton - 1] += 1; }
            //            if (bomb[i - optionEntity.hNumForButton] != 9) { bomb[i - optionEntity.hNumForButton] += 1; }
            //            if (bomb[i - 1] != 9) { bomb[i - 1] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton - 1] != 9) { bomb[i + optionEntity.hNumForButton - 1] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton] != 9) { bomb[i + optionEntity.hNumForButton] += 1; }
            //        }
            //        else
            //        {
            //            if (bomb[i - optionEntity.hNumForButton - 1] != 9) { bomb[i - optionEntity.hNumForButton - 1] += 1; }
            //            if (bomb[i - optionEntity.hNumForButton] != 9) { bomb[i - optionEntity.hNumForButton] += 1; }
            //            if (bomb[i - optionEntity.hNumForButton + 1] != 9) { bomb[i - optionEntity.hNumForButton + 1] += 1; }
            //            if (bomb[i - 1] != 9) { bomb[i - 1] += 1; }
            //            if (bomb[i + 1] != 9) { bomb[i + 1] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton - 1] != 9) { bomb[i + optionEntity.hNumForButton - 1] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton] != 9) { bomb[i + optionEntity.hNumForButton] += 1; }
            //            if (bomb[i + optionEntity.hNumForButton + 1] != 9) { bomb[i + optionEntity.hNumForButton + 1] += 1; }
            //        }
            //    }
            //}
        }

        private void CreateArrForMineButton() 
        {
            bomb = new int[optionEntity.hNumForButton * optionEntity.vNumForButton];
        }


        private void SetRowAndColDefinition() 
        {
            int rCnt = mainGrid.RowDefinitions.Count;
            int cCnt = mainGrid.ColumnDefinitions.Count;

            for (int i = 0; i < rCnt; i++)
			{
			    mainGrid.RowDefinitions.RemoveAt(0);
			}

            for (int i = 0; i < cCnt; i++)
            {
                mainGrid.ColumnDefinitions.RemoveAt(0);
            }

            for (int i = 0; i < optionEntity.hNumForButton; i++)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(33) });
            }

            for (int i = 0; i < optionEntity.vNumForButton; i++)
            {
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(33) });
            }
        }
        private void CreateButton() 
        {
            SetRowAndColDefinition();

            //var imgBrush = new ImageBrush();
            //imgBrush.ImageSource = new BitmapImage(new Uri(@"../../Image/Bomb.ico", UriKind.Relative));

            Uri uri = new Uri(@"../../Image/Bomb.ico", UriKind.Relative);
            BitmapImage bitmap = new BitmapImage(uri);


            for (int i = 0; i < bomb.Length; i++)
            {
                var mineButton = new ToggleButton();
                
                mineButton.Name = CommonMethod.UseStringBuilder("mine", i.ToString());
                if (bomb[i] == 9)
                {
                    Image img = new Image();
                    img.Source = bitmap;
                    img.Stretch = Stretch.Fill;
                    mineButton.Content = img;
                }
                else 
                {
                    mineButton.Content = bomb[i].ToString();
                    mineButton.FontSize = 14;
                    mineButton.FontWeight = FontWeights.Bold;

                    if (bomb[i] == 1 )
                    {
                        mineButton.Foreground = Brushes.Blue;
                    }
                    else if (bomb[i] == 2)
                    {
                        mineButton.Foreground = Brushes.Green;
                    }
                    else if (bomb[i] == 3)
                    {
                        mineButton.Foreground = Brushes.Orange;
                    }
                    else if (bomb[i] == 4)
                    {
                        mineButton.Foreground = Brushes.Brown;
                    }
                    else if (bomb[i] == 5)
                    {
                        mineButton.Foreground = Brushes.Pink;
                    }

                    else if (bomb[i] == 6)
                    {
                        mineButton.Foreground = Brushes.Black;
                    }

                    else if (bomb[i] == 7)
                    {
                        mineButton.Foreground = Brushes.Gray;
                    }

                    else if (bomb[i] == 8)
                    {
                        mineButton.Foreground = Brushes.Gold;
                    }
                }

                Grid.SetRow(mineButton, i / optionEntity.vNumForButton);
                Grid.SetColumn(mineButton, i % optionEntity.vNumForButton);

                mainGrid.Children.Add(mineButton);
            }


        }

    }
}
