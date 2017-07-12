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
        private Image img;
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

            if (optionEntity.isContinue)
	        {
                lblLevel.Content = CommonMethod.UseStringBuilder("Level : ", optionEntity.difficultyType, " Class");
                GameStart();
	        }
  
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
            for (int i = 0; i < bomb.Length; i++)
            {
                if (bomb[i] == 9)
                {
                    //좌상
                    if (i == 0)
                    {
                        if (bomb[i + 1] != 9) { bomb[i + 1] += 1; }
                        if (bomb[i + optionEntity.vNumForButton] != 9) { bomb[i + optionEntity.vNumForButton] += 1; }
                        if (bomb[i + optionEntity.vNumForButton + 1] != 9) { bomb[i + optionEntity.vNumForButton + 1] += 1; }
                    }
                    //우상
                    else if (i == optionEntity.vNumForButton - 1)
                    {
                        if (bomb[i - 1] != 9) { bomb[i - 1] += 1; }
                        if (bomb[i + optionEntity.vNumForButton - 1] != 9) { bomb[i + optionEntity.vNumForButton - 1] += 1; }
                        if (bomb[i + optionEntity.vNumForButton] != 9) { bomb[i + optionEntity.vNumForButton] += 1; }
                    }
                    //좌하
                    else if (i == optionEntity.vNumForButton * (optionEntity.hNumForButton - 1))
                    {
                        if (bomb[i - optionEntity.vNumForButton] != 9) { bomb[i - optionEntity.vNumForButton] += 1; }
                        if (bomb[i - optionEntity.vNumForButton + 1] != 9) { bomb[i - optionEntity.vNumForButton + 1] += 1; }
                        if (bomb[i + 1] != 9) { bomb[i + 1] += 1; }
                    }
                    //우하
                    else if (i == (optionEntity.vNumForButton * optionEntity.hNumForButton) - 1)
                    {
                        if (bomb[i - optionEntity.vNumForButton - 1] != 9) { bomb[i - optionEntity.vNumForButton - 1] += 1; }
                        if (bomb[i - optionEntity.vNumForButton] != 9) { bomb[i - optionEntity.vNumForButton] += 1; }
                        if (bomb[i - 1] != 9) { bomb[i - 1] += 1; }
                    }
                    //윗줄
                    else if (i > 0 && i < optionEntity.vNumForButton - 1)
                    {
                        if (bomb[i - 1] != 9) { bomb[i - 1] += 1; }
                        if (bomb[i + 1] != 9) { bomb[i + 1] += 1; }
                        if (bomb[i + optionEntity.vNumForButton - 1] != 9) { bomb[i + optionEntity.vNumForButton - 1] += 1; }
                        if (bomb[i + optionEntity.vNumForButton] != 9) { bomb[i + optionEntity.vNumForButton] += 1; }
                        if (bomb[i + optionEntity.vNumForButton + 1] != 9) { bomb[i + optionEntity.vNumForButton + 1] += 1; }
                    }
                    //아랫줄
                    else if (i > optionEntity.vNumForButton * (optionEntity.hNumForButton - 1)
                        && i < (optionEntity.vNumForButton * optionEntity.hNumForButton) - 1)
                    {
                        if (bomb[i - 1] != 9) { bomb[i - 1] += 1; }
                        if (bomb[i + 1] != 9) { bomb[i + 1] += 1; }
                        if (bomb[i - optionEntity.vNumForButton - 1] != 9) { bomb[i - optionEntity.vNumForButton - 1] += 1; }
                        if (bomb[i - optionEntity.vNumForButton] != 9) { bomb[i - optionEntity.vNumForButton] += 1; }
                        if (bomb[i - optionEntity.vNumForButton + 1] != 9) { bomb[i - optionEntity.vNumForButton + 1] += 1; }
                    }
                    //왼쪽 줄
                    else if (i > 0
                        && i < (optionEntity.vNumForButton * optionEntity.hNumForButton - 1)
                        && i % optionEntity.vNumForButton == 0)
                    {
                        if (bomb[i - optionEntity.vNumForButton] != 9) { bomb[i - optionEntity.vNumForButton] += 1; }
                        if (bomb[i - optionEntity.vNumForButton + 1] != 9) { bomb[i - optionEntity.vNumForButton + 1] += 1; }
                        if (bomb[i + 1] != 9) { bomb[i + 1] += 1; }
                        if (bomb[i + optionEntity.vNumForButton] != 9) { bomb[i + optionEntity.vNumForButton] += 1; }
                        if (bomb[i + optionEntity.vNumForButton + 1] != 9) { bomb[i + optionEntity.vNumForButton + 1] += 1; }
                    }
                    //오른쪽 줄
                    else if (i > optionEntity.vNumForButton - 1
                        && i < (optionEntity.vNumForButton * optionEntity.hNumForButton) - 1
                        && i % optionEntity.vNumForButton == optionEntity.vNumForButton - 1)
                    {
                        if (bomb[i - optionEntity.vNumForButton - 1] != 9) { bomb[i - optionEntity.vNumForButton - 1] += 1; }
                        if (bomb[i - optionEntity.vNumForButton] != 9) { bomb[i - optionEntity.vNumForButton] += 1; }
                        if (bomb[i - 1] != 9) { bomb[i - 1] += 1; }
                        if (bomb[i + optionEntity.vNumForButton - 1] != 9) { bomb[i + optionEntity.vNumForButton - 1] += 1; }
                        if (bomb[i + optionEntity.vNumForButton] != 9) { bomb[i + optionEntity.vNumForButton] += 1; }
                    }
                    else
                    {
                        if (bomb[i - optionEntity.vNumForButton - 1] != 9) { bomb[i - optionEntity.vNumForButton - 1] += 1; }
                        if (bomb[i - optionEntity.vNumForButton] != 9) { bomb[i - optionEntity.vNumForButton] += 1; }
                        if (bomb[i - optionEntity.vNumForButton + 1] != 9) { bomb[i - optionEntity.vNumForButton + 1] += 1; }
                        if (bomb[i - 1] != 9) { bomb[i - 1] += 1; }
                        if (bomb[i + 1] != 9) { bomb[i + 1] += 1; }
                        if (bomb[i + optionEntity.vNumForButton - 1] != 9) { bomb[i + optionEntity.vNumForButton - 1] += 1; }
                        if (bomb[i + optionEntity.vNumForButton] != 9) { bomb[i + optionEntity.vNumForButton] += 1; }
                        if (bomb[i + optionEntity.vNumForButton + 1] != 9) { bomb[i + optionEntity.vNumForButton + 1] += 1; }
                    }
                }
            }
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
                mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
            }

            for (int i = 0; i < optionEntity.vNumForButton; i++)
            {
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
            }
        }

        private void CreateButton() 
        {
            SetRowAndColDefinition();

            for (int i = 0; i < bomb.Length; i++)
            {
                var mineButton = new ToggleButton();
                
                mineButton.Name = CommonMethod.UseStringBuilder("mine", i.ToString());
                mineButton.Tag = i;
                mineButton.FontSize = 14;
                mineButton.FontWeight = FontWeights.Bold;
                mineButton.Background = Brushes.Beige;
                mineButton.Checked += BtnOnChecked;
                mineButton.Unchecked += BtnOnChecked;

                Grid.SetRow(mineButton, i / optionEntity.vNumForButton);
                Grid.SetColumn(mineButton, i % optionEntity.vNumForButton);

                mainGrid.Children.Add(mineButton);
            }


        }

        void BtnOnChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = sender as ToggleButton;

            int aa = (int)btn.Tag;
            if (bomb[aa] == 9)
            {
                if ((bool)btn.IsChecked)
                {
                    Uri uri = new Uri(@"../../Image/Bomb.ico", UriKind.Relative);
                    BitmapImage bitmap = new BitmapImage(uri);

                    img = new Image();
                    img.Source = bitmap;
                    img.Stretch = Stretch.Fill;

                    btn.Content = img;
                    AllButtonClick();
                }
                else 
                {
                    btn.IsChecked = true;
                }
            }
            else 
            {
                if ((bool)btn.IsChecked)
                {
                    btn.Content = bomb[aa].ToString();
                }
                else 
                {
                    btn.IsChecked = true;
                }               
            }
        }

        private void AllButtonClick() 
        {

            foreach (ToggleButton btn in mainGrid.Children)
            {
                btn.IsChecked = true;
            }
        }
    }
}
