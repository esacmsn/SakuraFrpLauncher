﻿using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;

using fastJSON;
using MaterialDesignThemes.Wpf;

using SakuraLauncher.View;
using SakuraLauncher.Data;
using SakuraLauncher.Helper;

namespace SakuraLauncher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public const int CONFIG_VERSION = 2;

        public static ulong Tick = 0;
        public static MainWindow Instance = null;

        public SnackbarMessageQueue snackbarMessageQueue { get; set; } = new SnackbarMessageQueue();

        public string ConfigPath = null;

        public bool CanEditToken => !LoggingIn.Value && !LoggedIn.Value;

        public Prop<bool> AutoRun { get; set; } = new Prop<bool>();
        public Prop<bool> SuppressInfo { get; set; } = new Prop<bool>();
        public Prop<bool> LogTextWrapping { get; set; } = new Prop<bool>(true);
        public Prop<bool> LoggedIn { get; set; } = new Prop<bool>();
        public Prop<bool> LoggingIn { get; set; } = new Prop<bool>();
        public Prop<string> UserToken { get; set; } = new Prop<string>();

        public UserControl[] Tabs = null;
        public TabIndexTester CurrentTabTester { get; set; }
        public Prop<int> CurrentTab { get; set; } = new Prop<int>();

        public Tunnel CurrentListener
        {
            get => this._currentListener;
            set
            {
                this._currentListener = value;
                foreach(var l in Tunnels)
                {
                    l.Real?.RaisePropertyChanged("Selected");
                }
            }
        }
        private Tunnel _currentListener = null;

        public List<string> AutoStart = null;
        public ObservableCollection<ITunnel> Tunnels { get; set; } = new ObservableCollection<ITunnel>();
        public ObservableCollection<NodeData> Nodes { get; set; } = new ObservableCollection<NodeData>();

        public int LogoIndex = 0;

        public MainWindow(bool autorun)
        {
            Instance = this;

            AutoRun.Value = autorun;
            CurrentTabTester = new TabIndexTester(this);

            Tabs = new UserControl[] {
                new TunnelTab(this),
                new LogTab(this),
                new SettingsTab(this),
                new AboutTab()
            };

            InitializeComponent();

            LoggedIn.PropertyChanged += (s, e) => RaisePropertyChanged("CanEditToken");
            LoggingIn.PropertyChanged += (s, e) => RaisePropertyChanged("CanEditToken");

            #region Load Config

            if(File.Exists("config.json"))
            {
                var json = JSON.ToObject<Dictionary<string, dynamic>>(File.ReadAllText("config.json"));
                if(json.ContainsKey("token"))
                {
                    UserToken.Value = json["token"];
                }
                if(json.ContainsKey("logo"))
                {
                    SetLogo((int)json["logo"]);
                }
                if(json.ContainsKey("suppressinfo") && json["suppressinfo"])
                {
                    SuppressInfo.Value = true;
                }
                if(!json.ContainsKey("log_text_wrapping") || json["log_text_wrapping"])
                {
                    LogTextWrapping.Value = true;
                }
                if(json.ContainsKey("enable_tunnels") && json["enable_tunnels"] is List<object> enable_tunnels)
                {
                    AutoStart = enable_tunnels.Select(s => s.ToString()).ToList();
                }
                if(json.ContainsKey("loggedin") && json["loggedin"])
                {
                    TryLogin();
                }
            }
            ConfigPath = "config.json";

            #endregion

            DataContext = this;
            SuppressInfo.PropertyChanged += (s, e) => Save();

            SwitchTab(LoggedIn.Value || LoggingIn.Value ? 0 : 2);
        }

        public void Log(string tunnel, string raw) => (Tabs[1] as LogTab).Log(tunnel, raw);

        public void Save()
        {
            if(ConfigPath == null)
            {
                return;
            }
            File.WriteAllText(ConfigPath, JSON.ToNiceJSON(new Dictionary<string, object>()
            {
                { "version", CONFIG_VERSION },
                { "logo", LogoIndex },
                { "token", UserToken.Value.Trim() },
                { "suppressinfo", SuppressInfo.Value },
                { "loggedin", LoggedIn.Value },
                { "log_text_wrapping", LogTextWrapping.Value },
                { "enable_tunnels", Tunnels.Where(t => t.IsReal && t.Real.Enabled).Select(t => t.Real.Name) }
            }));
        }

        public void TryLogin()
        {
            LoggingIn.Value = true;
            App.ApiRequest("get_tunnels").ContinueWith(t =>
            {
                var tunnels = t.Result;
                if(tunnels == null)
                {
                    LoggingIn.Value = false;
                    return;
                }
                Dispatcher.Invoke(() => App.ApiRequest("get_nodes").ContinueWith(t2 =>
                {
                    LoggingIn.Value = false;
                    var nodes = t2.Result;
                    if(nodes == null)
                    {
                        return;
                    }
                    Dispatcher.Invoke(() =>
                    {
                        Nodes.Clear();
                        foreach(Dictionary<string, dynamic> j in nodes["data"])
                        {
                            Nodes.Add(new NodeData()
                            {
                                ID = (int)j["id"],
                                Name = (string)j["name"],
                                AcceptNew = (bool)j["accept_new"]
                            });
                        }
                        if(AutoStart == null)
                        {
                            AutoStart = new List<string>();
                            foreach(var tunnel in Tunnels)
                            {
                                if(tunnel.IsReal)
                                {
                                    if(tunnel.Real.Enabled)
                                    {
                                        AutoStart.Add(tunnel.Real.Name);
                                    }
                                    tunnel.Real.Stop();
                                }
                            }
                        }
                        Tunnels.Clear();
                        foreach(Dictionary<string, dynamic> j in tunnels["data"])
                        {
                            AddTunnel(j);
                        }
                        if(AutoStart != null)
                        {
                            foreach(var tunnel in Tunnels)
                            {
                                if(tunnel.IsReal && AutoStart.Contains(tunnel.Real.Name))
                                {
                                    tunnel.Real.Enabled = true;
                                }
                            }
                            AutoStart = null;
                        }
                        Tunnels.Add(new FakeTunnel());
                    });
                    LoggedIn.Value = true;
                    Save();
                }));
            });
        }

        public void AddTunnel(dynamic json, bool insert = false)
        {
            var name = "未知节点";
            foreach (NodeData node in Nodes)
            {
                if (node.ID == (int)json["node"])
                {
                    name = node.Name;
                    break;
                }
            }

            var t = new Tunnel()
            {
                Id = (int)json["id"],
                Name = (string)json["name"],
                Type = ((string)json["type"]).ToUpper(),
                NodeID = (int)json["node"],
                NodeName = name,
                Description = (string)json["description"]
            };
            t.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Enabled")
                {
                    Save();
                }
            };

            if (insert)
            {
                Tunnels.Insert(Tunnels.Count - 1, t);
            }
            else
            {
                Tunnels.Add(t);
            }
        }

        public void SetLogo(int index)
        {
            switch(index)
            {
            case 1:
                logo.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/debian.png")));
                break;
            case 2:
                logo.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/fishcake.png")));
                break;
            default:
                logo.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/logo.png")));
                break;
            }
            LogoIndex = index;
            Save();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ConfigPath = null;
            foreach(var l in Tunnels)
            {
                if(l.IsReal)
                {
                    l.Real.Stop();
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.ReleaseCapture();
            App.SendMessage(new WindowInteropHelper(this).Handle, 0xA1, (IntPtr)0x2, IntPtr.Zero);
        }

        private void ButtonHide_Click(object sender, RoutedEventArgs e) => Hide();

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            foreach(var t in Tunnels)
            {
                if(t.IsReal && t.Real.Enabled)
                {
                    if (App.ShowMessage("确定要退出程序吗?\n退出后所有隧道都会被关闭.", "Confirm", MessageBoxImage.Asterisk, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        Close();
                    }
                    return;
                }
            }
            Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch(e.ClickCount)
            {
            case 3:
                SetLogo(1);
                break;
            case 5:
                SetLogo(2);
                break;
            }
        }

        private void TrayIcon_TrayLeftMouseUp(object sender, RoutedEventArgs e)
        {
            Show();
            Topmost = true; // Still using this in 2020?
            Topmost = false;
        }

        #region Tab Switching

        public void SwitchTab(int id)
        {
            CurrentTab.Value = id;
            tabContents.BeginStoryboard(Resources["TabHideAnimation"] as Storyboard);
        }

        private void ButtonTab_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse((sender as Button).Tag as string);
            if(CurrentTab.Value != id)
            {
                SwitchTab(id);
                RaisePropertyChanged("CurrentTabTester");
            }
        }

        private void StoryboardTabHideAnimation_Completed(object sender, EventArgs e)
        {
            tabContents.Child = Tabs[CurrentTab.Value];
            tabContents.BeginStoryboard(Resources["TabShowAnimation"] as Storyboard);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        #endregion
    }
}
