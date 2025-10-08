using SCME.WpfControlLibrary.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SCME.WpfControlLibrary.CustomControls.ProfilesPageComponents
{
    /// <summary>
    /// Логика взаимодействия для AddTestParametrUserControl.xaml
    /// </summary>
    public partial class AddTestParameterUserControl : UserControl
    {
        public bool IsReadOnly { get; set; }
        
        public event Action AddTestParametersEvent;

        public AddTestParameterUserControl()
        {
            InitializeComponent();
        }

        private void AddTestParameters_Click(object sender, RoutedEventArgs e)
        {
            string test = TestComboBox.Text;
            DependencyObject parent = VisualTreeHelper.GetParent(sender as Button);
            while (parent != null && !(parent is Page))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            ((ProfilesPage)parent).ProfileVm.SelectedTest = test;

            AddTestParametersEvent?.Invoke();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedIndex == -1)
                cb.SelectedIndex = 0;
        }
    }
}
