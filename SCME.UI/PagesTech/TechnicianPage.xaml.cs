using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SCME.Types;
using SCME.UI.IO;
using SCME.WpfControlLibrary.Pages;

namespace SCME.UI.PagesTech
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class TechnicianPage
    {
        internal object PreviousPage;

        public TechnicianPage()
        {
            InitializeComponent();
            
        }

        internal void AreButtonsEnabled(TypeCommon.InitParams Param)
        {
            btnGate.IsEnabled = Param.IsGateEnabled;
            btnVtm.IsEnabled = Param.IsSLEnabled;
            btnBvt.IsEnabled = Param.IsBVTEnabled;
            btndVdt.IsEnabled = Param.IsdVdtEnabled;
            btnATU.IsEnabled = Param.IsATUEnabled;
            btnQrrTq.IsEnabled = Param.IsQrrTqEnabled;
            btnIH.IsEnabled = Param.IsIHEnabled;
            btnTOU.IsEnabled = Param.IsTOUEnabled;

        }

        private void Button_Click(object Sender, RoutedEventArgs E)
        {
            var btn = (Button)Sender;

            Page page = null;
            switch (Convert.ToUInt16(btn.CommandParameter))
            {
                case 1:
                    Cache.Gate = new GatePage();
                    page = Cache.Gate;
                    page.Margin = new Thickness(0,0,10,0);
                    break;
                case 2:
                    Cache.Sl = new SLPage();
                    page = Cache.Sl;
                    page.Margin = new Thickness(0,0,10,0);
                    break;
                case 3:
                    Cache.Bvt = new BvtPage();
                    page = Cache.Bvt;
                    page.Margin = new Thickness(0,0,10,0);
                    break;
                case 4:
                    page = Cache.Selftest;
                    break;
                case 6:
                    page = Cache.Console;
                    break;
                case 7:
                    page = Cache.ProfilesPage;
                    page.Margin = new Thickness(0, 0, 0, 0);
                    break;
                case 9:
                    page = Cache.ResultsPage;
                    break;
                case 10:
                    Cache.Welcome.IsBackEnable = true;
                    Cache.Welcome.IsRestartEnable = true;
                    page = Cache.Welcome;
                    break;
                case 11:
                    page = Cache.Clamp;
                    page.Margin = new Thickness(0,0,10,0);
                    break;
                case 12:
                    Cache.DVdt = new DVdtPage();
                    page = Cache.DVdt;
                    page.Margin = new Thickness(0,0,10,0);
                    break;
                case 13:
                    Cache.ATU = new ATUPage();
                    page = Cache.ATU;
                    page.Margin = new Thickness(0,0,10,0);
                    break;
                case 14:
                    Cache.QrrTq = new QrrTqPage();
                    page = Cache.QrrTq;
                    page.Margin = new Thickness(0,0,10,0);
                    break;
                case 15:
                    //Cache.RAC = new RACPage();
                    //page = Cache.RAC;
                    break;
                case 16:
                    Cache.IH = new IHPage();
                    page = Cache.IH;
                    page.Margin = new Thickness(0,0,10,0);
                    break;
                case 17:
                    Cache.TOU = new TOUPage();
                    page = Cache.TOU;
                    page.Margin = new Thickness(0,0,10,0);
                    break;
                case 18:
                    Cache.SSRTU = new SSRTUPage(Types.BaseTestParams.TestParametersType.OutputLeakageCurrent , 
                        () =>Cache.Net.StartSSRTU(new List<Types.BaseTestParams.BaseTestParametersAndNormatives> { Cache.SSRTU.ItemVM }, Cache.SSRTU.ItemVM.DutPackageType),
                        Cache.Net.StopSSRTU);
                    Cache.Main.VM.TopTitle = "Ток утечки на выходе";
                    Cache.SSRTUHandler = Cache.SSRTU.SSRTUHandler;
                    Cache.PostAlarmEvent = Cache.SSRTU.PostAlarmEvent;
                    Cache.PostSSRTUNotificationEvent = Cache.SSRTU.PostSSRTUNotificationEvent;
                    page = Cache.SSRTU;
                    break;
                case 19:
                    Cache.SSRTU = new SSRTUPage(Types.BaseTestParams.TestParametersType.OutputResidualVoltage,
                        () => Cache.Net.StartSSRTU(new List<Types.BaseTestParams.BaseTestParametersAndNormatives> { Cache.SSRTU.ItemVM }, Cache.SSRTU.ItemVM.DutPackageType),
                        Cache.Net.StopSSRTU);
                    Cache.Main.VM.TopTitle = "Выходное остаточное напряжение";
                    Cache.SSRTUHandler = Cache.SSRTU.SSRTUHandler;
                    Cache.PostAlarmEvent = Cache.SSRTU.PostAlarmEvent;
                    Cache.PostSSRTUNotificationEvent = Cache.SSRTU.PostSSRTUNotificationEvent;
                    page = Cache.SSRTU;
                    break;
                case 20:
                    Cache.SSRTU = new SSRTUPage(Types.BaseTestParams.TestParametersType.InputOptions,
                        () => Cache.Net.StartSSRTU(new List<Types.BaseTestParams.BaseTestParametersAndNormatives> { Cache.SSRTU.ItemVM }, Cache.SSRTU.ItemVM.DutPackageType),
                        Cache.Net.StopSSRTU);
                    Cache.Main.VM.TopTitle = "Параметры входа";
                    Cache.SSRTUHandler = Cache.SSRTU.SSRTUHandler;
                    Cache.PostAlarmEvent = Cache.SSRTU.PostAlarmEvent;
                    Cache.PostSSRTUNotificationEvent = Cache.SSRTU.PostSSRTUNotificationEvent;
                    page = Cache.SSRTU;
                    break;
                case 21:
                    Cache.SSRTU = new SSRTUPage(Types.BaseTestParams.TestParametersType.ProhibitionVoltage,
                        () => Cache.Net.StartSSRTU(new List<Types.BaseTestParams.BaseTestParametersAndNormatives> { Cache.SSRTU.ItemVM }, Cache.SSRTU.ItemVM.DutPackageType),
                        Cache.Net.StopSSRTU);
                    Cache.Main.VM.TopTitle = "Напряжение запрета";
                    Cache.SSRTUHandler = Cache.SSRTU.SSRTUHandler;
                    Cache.PostAlarmEvent = Cache.SSRTU.PostAlarmEvent;
                    Cache.PostSSRTUNotificationEvent = Cache.SSRTU.PostSSRTUNotificationEvent;
                    page = Cache.SSRTU;
                    break;
                case 24:
                    Cache.SSRTU = new SSRTUPage(Types.BaseTestParams.TestParametersType.AuxiliaryPower,
                        () => Cache.Net.StartSSRTU(new List<Types.BaseTestParams.BaseTestParametersAndNormatives> { Cache.SSRTU.ItemVM }, Cache.SSRTU.ItemVM.DutPackageType),
                        Cache.Net.StopSSRTU);
                    Cache.Main.VM.TopTitle = "Вспомогательное питание";
                    Cache.SSRTUHandler = Cache.SSRTU.SSRTUHandler;
                    Cache.PostAlarmEvent = Cache.SSRTU.PostAlarmEvent;
                    Cache.PostSSRTUNotificationEvent = Cache.SSRTU.PostSSRTUNotificationEvent;
                    page = Cache.SSRTU;
                    break;
                case 22:
                    page = new AttestationSelectPage();
                    break;
                case 23:
                    var reportFolder = SCME.UIServiceConfig.Properties.Settings.Default.ReportFolder;
                    if (Directory.Exists(reportFolder) == false)
                        reportFolder = Directory.GetCurrentDirectory();
                    Process.Start("explorer.exe", reportFolder);
                    break;
            }

            
            if (page != null && NavigationService != null)
                NavigationService.Navigate(page);
        }

        private void BtnBack_Click(object Sender, RoutedEventArgs E)
        {
            if (NavigationService != null)
            {
                if (PreviousPage == null)
                    NavigationService.Navigate(Cache.Login);
                    //PreviousPage = Cache.Login;

//                Cache.ProfileEdit.ClearFilter();
//                Cache.ProfileSelection.InitFilter();
//                Cache.ProfileSelection.InitSorting();
//                if (PreviousPage is ProfilesPage)
//                {
////                    NavigationService.Navigate(Cache.ProfileSelection);
//                    return;
//                }
                Cache.Password.AfterOkRoutine = delegate { Cache.Main.mainFrame.Navigate(Cache.Technician); };
                NavigationService.GoBack();
            }
        }

        private void TechnicianPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Cache.ProfilesPage.GoBackAction += () => { ProfilesDbLogic.LoadProfile(Cache.ProfilesPage.ProfileVm.Profiles.ToList());};
            Cache.Main.VM.AccountNameIsVisibility = false;
        }
    }
}