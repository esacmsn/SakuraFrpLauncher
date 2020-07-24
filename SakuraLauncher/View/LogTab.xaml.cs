﻿using System.Windows;
using System.Threading;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Text.RegularExpressions;

namespace SakuraLauncher.View
{
    /// <summary>
    /// LogTab.xaml 的交互逻辑
    /// </summary>
    public partial class LogTab : UserControl
    {
        public static Regex LogPattern = new Regex(@"(?<Time>\d{4}/\d{2}/\d{2} \d{2}:\d{2}:\d{2}) (?<Level>\[[DIWE]\]) \[(?<Source>[a-zA-Z0-9\-_\.]+:\d+)\] (?<Content>.+)", RegexOptions.Compiled | RegexOptions.Singleline);

        public static readonly SolidColorBrush BrushInfo = new SolidColorBrush(Colors.White),
             BrushWarning = new SolidColorBrush(Colors.Orange),
             BrushError = new SolidColorBrush(Color.FromRgb(220, 80, 54)),
             BrushTime = new SolidColorBrush(Color.FromRgb(80, 141, 220)),
             BrushText = new SolidColorBrush(Colors.Silver),
             BrushTunnel = new SolidColorBrush(Colors.Wheat);

        private readonly MainWindow Main = null;

        public string failedData = "";

        public LogTab(MainWindow main)
        {
            InitializeComponent();
            DataContext = Main = main;
        }

        public void Log(string tunnel, string raw)
        {
            bool bottom = ScrollViewerLog.ScrollableHeight - ScrollViewerLog.VerticalOffset < 1;
            if(TextBlockLog.Inlines.Count != 0)
            {
                AddLineBreak();
            }
            AddRun(tunnel + " ", BrushTunnel);
            var match = LogPattern.Match(raw);
            if(!match.Success)
            {
                failedData += raw + "\n";
                AddRun(raw, BrushText);
                AddRun("", BrushText); // Dirty Patch
                AddRun("", BrushText);
                AddRun("", BrushText);
            }
            else
            {
                AddRun(match.Groups["Time"].Value + " ", BrushTime);
                if(failedData != "")
                {
                    if(Main.IsVisible && !Main.SuppressInfo.Value)
                    {
                        string failedData_ = failedData;
                        ThreadPool.QueueUserWorkItem(s => App.ShowMessage(failedData_, "隧道日志 " + tunnel, MessageBoxImage.Information));
                    }
                    failedData = "";
                }
                AddRun(match.Groups["Time"].Value + " ", BrushTime);
                var levelColor = BrushInfo;
                switch(match.Groups["Level"].Value)
                {
                case "W":
                    levelColor = BrushWarning;
                    break;
                case "E":
                    levelColor = BrushError;
                    break;
                }
                AddRun(match.Groups["Level"].Value + ":" + match.Groups["Source"].Value + " ", levelColor);
                AddRun(match.Groups["Content"].Value, BrushText);
            }
            while(TextBlockLog.Inlines.Count > 600 - 1)
            {
                TextBlockLog.Inlines.Remove(TextBlockLog.Inlines.FirstInline);
            }
            if(bottom)
            {
                ScrollViewerLog.ScrollToBottom();
            }
        }

        public void AddRun(string text, Brush color) => TextBlockLog.Inlines.Add(new Run(text)
        {
            Foreground = color
        });

        public void AddLineBreak() => TextBlockLog.Inlines.Add(new LineBreak());

        private void ButtonClear_Click(object sender, RoutedEventArgs e) => TextBlockLog.Inlines.Clear();
    }
}
