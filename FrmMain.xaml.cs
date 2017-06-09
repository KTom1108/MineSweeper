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
    /// Interaction logic for FrmMain.xaml
    /// </summary>
    public partial class FrmMain : Window
    {
        private OptionEntity optionEntity;
        private FrmOption frmOption;

        public FrmMain()
        {
            InitializeComponent();
            frmOption = FrmOption.getInstance;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetOptionValue();
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
    }
}
