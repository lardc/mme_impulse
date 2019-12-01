﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SCME.Types;
using SCME.Types.Profiles;
using SCME.UI.IO;
using SCME.UI.Properties;

namespace SCME.UI.PagesUser
{
    /// <summary>
    ///     Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage
    {
        private CAccount CurrentAccount { get; set; }

        public LoginPage()
        {
            InitializeComponent();

            CurrentAccount = new CAccount();

            accountsListBox.ItemsSource = new AccountEngine(Settings.Default.AccountsPath).Collection;

            if (accountsListBox.Items.Count > 0)
                accountsListBox.SelectedIndex = 0;
            else
                buttonNext.Visibility = Visibility.Hidden;
        }

        private void btBack_OnClick(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null)
                NavigationService.Navigate(Cache.UserWorkMode);
        }

        private void ButtonNext_OnClick(object Sender, RoutedEventArgs E)
        {
//            if (!Cache.Main.IsProfilesParsed)
//                ProfilesDbLogic.ImportProfilesFromDb();
            
            if (tbPassword.Text == CurrentAccount.Password && NavigationService != null)
            {
                lblIncorrect.Content = "";
                Cache.Main.AccountName = CurrentAccount.Name;
//                Cache.ProfileEdit.ClearFilter();
//                Cache.ProfileSelection.InitFilter();
//                Cache.ProfileSelection.InitSorting();
                //NavigationService.Navigate(Cache.ProfileSelection);
                Cache.ProfilesPageSelectForTest.LoadTopProfiles();
                Cache.Main.VM.TopTitle = $"{SCME.UI.Properties.Resources.Total} {SCME.UI.Properties.Resources.Profiles}: {Cache.ProfilesPageSelectForTest.ProfileVm.Profiles.Count}";
                Cache.ProfilesPageSelectForTest.ProfileVm.NextAction = () =>
                {
                    Cache.UserTest.Profile = Cache.ProfilesPageSelectForTest.ProfileVm.SelectedProfile.ToProfile();

                    //запоминаем в UserTest флаг 'Режим специальных измерений' для возможности корректной работы её MultiIdentificationFieldsToVisibilityConverter 
                    Cache.UserTest.SpecialMeasureMode = (Cache.WorkMode == UserWorkMode.SpecialMeasure);

                    if (NavigationService != null)
                    {
                        Cache.UserTest.InitSorting();
                        Cache.UserTest.InitTemp();
                        NavigationService.Navigate(Cache.UserTest);
                    }

                    Debug.Assert(Cache.ProfilesPageSelectForTest.NavigationService != null, "Cache.ProfilesPageSelectForTest.NavigationService != null");
                    Cache.ProfilesPageSelectForTest.NavigationService.Navigate(Cache.UserTest);
                };
                NavigationService.Navigate(Cache.ProfilesPageSelectForTest);
            }
            else
                lblIncorrect.Content = Properties.Resources.PasswordIncorrect;

            tbPassword.Text = string.Empty;
        }

        private void AccountsListBox_OnSelectionChanged(object Sender, SelectionChangedEventArgs E)
        {
            CurrentAccount = (CAccount) accountsListBox.SelectedItem;
            lblIncorrect.Content = "";
        }

        private void LoginPage_Loaded(object Sender, RoutedEventArgs E)
        {
            lblIncorrect.Content = "";
        }
    }
}