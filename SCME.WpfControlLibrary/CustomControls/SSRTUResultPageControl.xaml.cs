using SCME.WpfControlLibrary.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SCME.WpfControlLibrary.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для SSRTUResultPageControl.xaml
    /// </summary>
    public partial class SSRTUResultPageControl : Grid
    {
        public SSRTUResultPageControl()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ((SSRTUResultComponentVM)DataContext).NeedsManualAmperage1 = InputAmp1.Visibility == Visibility.Visible;
            ((SSRTUResultComponentVM)DataContext).NeedsManualAmperage2 = InputAmp2.Visibility == Visibility.Visible;
            ((SSRTUResultComponentVM)DataContext).NeedsManualAmperage3 = InputAmp3.Visibility == Visibility.Visible;
            ((SSRTUResultComponentVM)DataContext).NeedsManualAmperage4 = InputAmp4.Visibility == Visibility.Visible;

            ((SSRTUResultComponentVM)DataContext).NeedsManualVoltage1 = InputVol1.Visibility == Visibility.Visible;
            ((SSRTUResultComponentVM)DataContext).NeedsManualVoltage2 = InputVol2.Visibility == Visibility.Visible;
            ((SSRTUResultComponentVM)DataContext).NeedsManualVoltage3 = InputVol3.Visibility == Visibility.Visible;
            ((SSRTUResultComponentVM)DataContext).NeedsManualVoltage4 = InputVol4.Visibility == Visibility.Visible;
        }

        private void ValidatingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidatingTextBox validatingTextBox = sender as ValidatingTextBox;
            if (double.TryParse(validatingTextBox.Text, out _))
                return;

            var binding = validatingTextBox.GetBindingExpression(ValidatingTextBox.TextProperty);
            var path = binding.ParentBinding.Path.Path;
            object source = binding.DataItem;
            var property = source.GetType().GetProperty(path);
            property?.SetValue(source, null);
        }
    }
}
