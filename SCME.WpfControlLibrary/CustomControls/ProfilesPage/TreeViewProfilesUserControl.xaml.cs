﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SCME.WpfControlLibrary.CustomControls.ProfilesPage
{
    /// <summary>
    /// Логика взаимодействия для TreeViewProfilesUserControl.xaml
    /// </summary>
    public partial class TreeViewProfilesUserControl : UserControl
    {
        public TreeViewProfilesUserControl()
        {
            InitializeComponent();
        }

        private void ProfilesTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
