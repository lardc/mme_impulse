using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SCME.Types;
using SCME.Types.Profiles;
using SCME.UI.IO;
using SCME.UIServiceConfig.Properties;
using SCME.WpfControlLibrary;
using SCME.WpfControlLibrary.Pages;

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
                NavigationService?.Navigate(Cache.UserWorkMode);
        }

        private void ButtonNext_OnClick(object Sender, RoutedEventArgs E)
        {
//            if (!Cache.Main.IsProfilesParsed)
//                ProfilesDbLogic.ImportProfilesFromDb();
            
            if (tbPassword.Text == CurrentAccount.Password && NavigationService != null)
            {
                lblIncorrect.Content = "";
                Cache.Main.VM.AccountName = CurrentAccount.Name;
//                Cache.ProfileEdit.ClearFilter();
//                Cache.ProfileSelection.InitFilter();
//                Cache.ProfileSelection.InitSorting();
                //NavigationService.Navigate(Cache.ProfileSelection);
                PrepareMoveToSelectProfilePage(Cache.ProfilesPageSelectForTest, CurrentAccount.Name);
                Debug.Assert(NavigationService != null, nameof(NavigationService) + " != null");
                NavigationService.Navigate(Cache.ProfilesPageSelectForTest);
            }
            else
                lblIncorrect.Content = Properties.Resources.PasswordIncorrect;

            tbPassword.Text = string.Empty;
        }



        public static void PrepareMoveToSelectProfilePage(ProfilesPage profilesPage, string userName)
        {
            Cache.Main.VM.AccountNameIsVisibility = true;
            profilesPage.LoadTopProfiles();
            profilesPage.Title = $"{Properties.Resources.Total} профилей: {profilesPage.ProfileVm.Profiles.Count}";
//            Cache.ProfilesPageSelectForTest.GoBackAction += () =>
//            {
//                var navigationService = Cache.ProfilesPageSelectForTest.NavigationService;
//                Debug.Assert(navigationService != null, nameof(navigationService) + " != null");
//                navigationService.Navigate(Cache.Login);
//            };
            profilesPage.ProfileVm.NextAction = () =>
            {
                var navigationService = profilesPage.NavigationService;
                
                var page = Cache.SSRTUResultPage = new SSRTUResultPage(userName, Cache.Main.VM.MmeCode, profilesPage.ProfileVm.SelectedProfile.ToProfile(),
                    () => Cache.Net.StartSSRTU(profilesPage.ProfileVm.SelectedProfile.DeepData.TestParametersAndNormatives.ToList(), profilesPage.ProfileVm.SelectedProfile.DeepData.DutPackageType),
                    Cache.Net.StopSSRTU, Cache.Net.NeedStart)
                {
                    Title = $"Тестирование {profilesPage.ProfileVm.SelectedProfile.Name}",
                };
                Cache.SSRTUHandler = page.SSRTUHandler;
                Cache.PostAlarmEvent = page.PostAlarmEvent;
                Cache.PostSSRTUNotificationEvent = page.PostSSRTUNotificationEvent;
                navigationService.Navigate(page);


                //Cache.UserTest.Profile = profilesPage.ProfileVm.SelectedProfile.ToProfile();
                //Cache.UserTest.Title = Properties.Resources.UserTestPage_Title + ", " + Properties.Resources.Profile.ToLower() + ": " + "\n" + Cache.UserTest.Profile;
                ////запоминаем в UserTest флаг 'Режим специальных измерений' для возможности корректной работы её MultiIdentificationFieldsToVisibilityConverter 
                //Cache.UserTest.SpecialMeasureMode = (Cache.WorkMode == UserWorkMode.SpecialMeasure);

                //var navigationService = profilesPage.NavigationService;

                //Cache.UserTest.InitSorting();
                //Cache.UserTest.InitTemp();
                    
                //Debug.Assert(navigationService != null, "navigationService != null");
                //navigationService.Navigate(Cache.UserTest);
            };
            
        }

        private void AccountsListBox_OnSelectionChanged(object Sender, SelectionChangedEventArgs E)
        {
            CurrentAccount = (CAccount) accountsListBox.SelectedItem;
            lblIncorrect.Content = "";
        }

        private void LoginPage_Loaded(object Sender, RoutedEventArgs E)
        {
            lblIncorrect.Content = "";
//            Cache.Main.VM.AccountName = string.Empty;
        }

        //private void TbPassword_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (WpfControlLibrary.Properties.Settings.Default.IsTouchUI && FindParent<Window>(this) is IMainWindow window)
        //        window.ShowKeyboard(true, sender as );
        //}
    }
}