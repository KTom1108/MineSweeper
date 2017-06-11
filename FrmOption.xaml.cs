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

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for FrmOption.xaml
    /// </summary>
    public partial class FrmOption : Window
    {
        private static readonly FrmOption _frmOption = new FrmOption();
        public static FrmOption getInstance { get { return _frmOption; } }

        OptionEntity entity;

        public FrmOption()
        {
            InitializeComponent();
        }

        public void SetOptionEntity(OptionEntity _Entity)
        {
            this.entity = _Entity;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (!entity.difficultyType.Equals(CommonMethod.GetRegistryKey(CommonCode.REGKEY_LEVEL)))
            {
                if (entity.difficultyType.Equals(CommonCode.REGKEY_LEVELVALUE_LOW))
                {
                    SetClassValue(CommonCode.REGKEY_LEVELVALUE_LOW, 9, 9, 10);
                }
                else if (entity.difficultyType.Equals(CommonCode.REGKEY_LEVELVALUE_MIDDLE))
                {
                    SetClassValue(CommonCode.REGKEY_LEVELVALUE_MIDDLE, 16, 16, 40);
                }
                else if (entity.difficultyType.Equals(CommonCode.REGKEY_LEVELVALUE_HIGH))
                {
                    SetClassValue(CommonCode.REGKEY_LEVELVALUE_HIGH, 16, 30, 90);
                }
                else
                {
                    MessageBox.Show("Please Select Difficulty Class", "Apply", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            this.Hide();
        }

        private void SetClassValue(string level, int hNum, int vNum, int mNum) 
        {
            entity.difficultyType = level;

            entity.hNumForButton = hNum;
            entity.vNumForButton = vNum;

            entity.numForMine = mNum;

            CommonMethod.SetRegistryKey(CommonCode.REGKEY_LEVEL, level);
        }  

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            entity.difficultyType = CommonMethod.GetRegistryKey(CommonCode.REGKEY_LEVEL);

            if (entity.difficultyType.Equals(CommonCode.REGKEY_LEVELVALUE_LOW))
            {
                rdoLow.IsChecked = true;
                SetClassValue(CommonCode.REGKEY_LEVELVALUE_LOW, 9, 9, 10);
                this.Hide();
                return;
            }
            else if (entity.difficultyType.Equals(CommonCode.REGKEY_LEVELVALUE_MIDDLE))
            {
                rdoMiddle.IsChecked = true;
                SetClassValue(CommonCode.REGKEY_LEVELVALUE_MIDDLE, 16, 16, 40);
                this.Hide();
                return;
            }
            else if (entity.difficultyType.Equals(CommonCode.REGKEY_LEVELVALUE_HIGH))
            {
                rdoHigh.IsChecked = true;
                SetClassValue(CommonCode.REGKEY_LEVELVALUE_HIGH, 16, 30, 90);
                this.Hide();
                return;
            }

            rdoLow.IsChecked = true;
            SetClassValue(CommonCode.REGKEY_LEVELVALUE_LOW, 9, 9, 10);
        }

        private void rdoLow_Checked(object sender, RoutedEventArgs e)
        {
            entity.difficultyType = CommonCode.REGKEY_LEVELVALUE_LOW;
        }

        private void rdoMiddle_Checked(object sender, RoutedEventArgs e)
        {
            entity.difficultyType = CommonCode.REGKEY_LEVELVALUE_MIDDLE;
        }

        private void rdoHigh_Checked(object sender, RoutedEventArgs e)
        {
            entity.difficultyType = CommonCode.REGKEY_LEVELVALUE_HIGH;
        }
    }
}
