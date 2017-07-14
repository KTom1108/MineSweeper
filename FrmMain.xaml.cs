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
        private Image imgBomb;
        private Image imgFlag;

        private int[] bomb;

        private bool isClear;
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
            isClear = false;
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
            bomb = null;
            bomb = new int[optionEntity.hNumForButton * optionEntity.vNumForButton];
        }

        private void SetRowAndColDefinition() 
        {
            int rCnt = mainGrid.RowDefinitions.Count;
            int cCnt = mainGrid.ColumnDefinitions.Count;

            mainGrid.RowDefinitions.Clear();
            mainGrid.ColumnDefinitions.Clear();
            mainGrid.Children.Clear();
            //for (int i = 0; i < rCnt; i++)
            //{
            //    mainGrid.RowDefinitions.RemoveAt(0);
            //}

            //for (int i = 0; i < cCnt; i++)
            //{
            //    mainGrid.ColumnDefinitions.RemoveAt(0);
            //}

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
                if (bomb[i].ToString() == "9")
                {
                    mineButton.Click += BtnOnClickForMine;    
                }
                else 
                {
                    mineButton.Checked += BtnOnCheckedForBtn;
                    mineButton.Click += BtnOnClickForBtn;
                }
                mineButton.MouseRightButtonDown += BtnOnCheckForMine;
                mineButton.Unchecked += BtnOnUnChecked;

                Grid.SetRow(mineButton, i / optionEntity.vNumForButton);
                Grid.SetColumn(mineButton, i % optionEntity.vNumForButton);

                mainGrid.Children.Add(mineButton);
            }
        }

        void BtnOnCheckForMine(object sender, RoutedEventArgs e) 
        {
            ToggleButton btn = sender as ToggleButton;
            if (!(bool)btn.IsChecked)
            {
                if (btn.Background == null)
                {
                    btn.Content = null;
                    btn.Background = Brushes.Beige;
                }
                else
                {
                    Uri uri = new Uri(@"\image\Flag.ico", UriKind.Relative);
                    BitmapImage bitmap = new BitmapImage(uri);

                    imgFlag = new Image();
                    imgFlag.Source = bitmap;
                    imgFlag.Stretch = Stretch.Fill;
                    btn.Background = null;
                    btn.Content = imgFlag;
                }
            }
        }


        void BtnOnClickForMine(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = sender as ToggleButton;

            if (btn.Background == null)
            {
                btn.IsChecked = false;
                return;
            }


            foreach (ToggleButton tBtn in mainGrid.Children)
            {
                if (bomb[(int)tBtn.Tag] == 9)
                {
                    Uri uri = new Uri(@"\image\Bomb.ico", UriKind.Relative);
                    BitmapImage bitmap = new BitmapImage(uri);

                    imgBomb = new Image();
                    imgBomb.Source = bitmap;
                    imgBomb.Stretch = Stretch.Fill;

                    tBtn.Content = imgBomb;

                    tBtn.IsChecked = true;
                }
            }

            MessageBox.Show("You Lose");
            GameStart();
        }

        void BtnOnClickForBtn(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = sender as ToggleButton;

            if (btn.Background == null)
            {
                btn.IsChecked = false;
                return;
            }

            int tagCnt = 0;
            int btnCnt = 0;

            foreach (ToggleButton tBtn in mainGrid.Children)
            {
                tagCnt = (int)tBtn.Tag;
                if ((bool)tBtn.IsChecked && bomb[tagCnt] != 9)
                {
                    btnCnt++;
                }
            }

            if (bomb.Length == btnCnt + optionEntity.numForMine)
            {
                isClear = true;
            }

            if (isClear)
            {
                MessageBox.Show("You Win");
                GameStart();
            }
        }

        void BtnOnUnChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = sender as ToggleButton;
            btn.IsChecked = true;
        }

        void BtnOnCheckedForBtn(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = sender as ToggleButton;

            if (btn.Background == null)
            {
                btn.IsChecked = false;
                return;
            }

            int num = (int)btn.Tag;

            if ((bool)btn.IsChecked)
            {
                btn.Content = bomb[num].ToString();
                btn.Foreground = CommonMethod.GetNumByColorKey<SolidColorBrush>(bomb[num], CommonCode.numColorDi);
                if (bomb[num] == 0)
                {
                    BtnCheckedForReguler(num);
                }
            }     
        }

        private void BtnCheckedForReguler(int num) 
        {
            int tagNum = 0;

            foreach (ToggleButton btn in mainGrid.Children)
            {
                if (bomb[num] == 0)
                {
                    tagNum = (int)btn.Tag;

                    //좌상
                    if (num == 0)
                    {
                        if (tagNum == num + 1
                            || tagNum == num + optionEntity.vNumForButton
                            || tagNum == num + optionEntity.vNumForButton + 1)
                        {
                            if (!(bool)btn.IsChecked)
                            {
                                btn.IsChecked = true;
                            }
                        }
                    }
                    //우상
                    else if (num == optionEntity.vNumForButton - 1)
                    {
                        if (tagNum == num - 1
                            || tagNum == num + optionEntity.vNumForButton - 1
                            || tagNum == num + optionEntity.vNumForButton)
                        {
                            if (!(bool)btn.IsChecked)
                            {
                                btn.IsChecked = true;
                            }
                        }
                    }
                    //좌하
                    else if (num == optionEntity.vNumForButton * (optionEntity.hNumForButton - 1))
                    {
                        if (tagNum == num - optionEntity.vNumForButton
                            || tagNum == num - optionEntity.vNumForButton + 1
                            || tagNum == num + 1)
                        {
                            if (!(bool)btn.IsChecked)
                            {
                                btn.IsChecked = true;
                            }
                        }
                    }
                    //우하
                    else if (num == (optionEntity.vNumForButton * optionEntity.hNumForButton) - 1)
                    {                        
                        if (tagNum == num - optionEntity.vNumForButton - 1
                            || tagNum == num - optionEntity.vNumForButton
                            || tagNum == num - 1)
                        {
                            if (!(bool)btn.IsChecked)
                            {
                                btn.IsChecked = true;
                            }
                        }
                    }
                    //윗줄
                    else if (num > 0 && num < optionEntity.vNumForButton - 1)
                    {
                        if (tagNum == num - 1
                            || tagNum == num + 1
                            || tagNum == num + optionEntity.vNumForButton - 1
                            || tagNum == num + optionEntity.vNumForButton
                            || tagNum == num + optionEntity.vNumForButton + 1)
                        {
                            if (!(bool)btn.IsChecked)
                            {
                                btn.IsChecked = true;
                            }
                        }
                    }
                    //아랫줄
                    else if (num > optionEntity.vNumForButton * (optionEntity.hNumForButton - 1)
                        && num < (optionEntity.vNumForButton * optionEntity.hNumForButton) - 1)
                    {
                        if (tagNum == num - 1
                            || tagNum == num + 1
                            || tagNum == num - optionEntity.vNumForButton - 1
                            || tagNum == num - optionEntity.vNumForButton
                            || tagNum == num - optionEntity.vNumForButton + 1
                        )
                        {
                            if (!(bool)btn.IsChecked)
                            {
                                btn.IsChecked = true;
                            }
                        }
                    }
                    //왼쪽 줄
                    else if (num > 0
                        && num < (optionEntity.vNumForButton * optionEntity.hNumForButton - 1)
                        && num % optionEntity.vNumForButton == 0)
                    {
                        if (tagNum == num - optionEntity.vNumForButton
                            || tagNum == num - optionEntity.vNumForButton + 1
                            || tagNum == num + 1
                            || tagNum == num + optionEntity.vNumForButton
                            || tagNum == num + optionEntity.vNumForButton + 1
                        )
                        {
                            if (!(bool)btn.IsChecked)
                            {
                                btn.IsChecked = true;
                            }
                        }
                    }
                    //오른쪽 줄
                    else if (num > optionEntity.vNumForButton - 1
                        && num < (optionEntity.vNumForButton * optionEntity.hNumForButton) - 1
                        && num % optionEntity.vNumForButton == optionEntity.vNumForButton - 1)
                    {
                        if (tagNum == num - optionEntity.vNumForButton - 1
                            || tagNum == num - optionEntity.vNumForButton
                            || tagNum == num - 1
                            || tagNum == num + optionEntity.vNumForButton - 1
                            || tagNum == num + optionEntity.vNumForButton
                            )
                        {
                            if (!(bool)btn.IsChecked)
                            {
                                btn.IsChecked = true;
                            }
                        }
                    }
                    else
                    {
                        if (tagNum == num - optionEntity.vNumForButton - 1
                            || tagNum == num - optionEntity.vNumForButton
                            || tagNum == num - optionEntity.vNumForButton + 1
                            || tagNum == num - 1
                            || tagNum == num + 1
                            || tagNum == num + optionEntity.vNumForButton - 1
                            || tagNum == num + optionEntity.vNumForButton
                            || tagNum == num + optionEntity.vNumForButton + 1
                            )
                        {
                            if (!(bool)btn.IsChecked)
                            {
                                btn.IsChecked = true;
                            }
                        }
                    }     
                }
            }
        }
    }
}
