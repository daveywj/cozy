﻿using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using CozySql.Configure;

namespace CozySql.Exe
{
    /// <summary>
    /// Interaction logic for MainFrame.xaml
    /// </summary>
    public partial class MainFrame : MetroWindow
    {
        public class UIControlInfo
        {
            public string Title { get; set; }
            public UserControl Content { get; set; }
        }

        public MainFrame()
        {
            this.DataContext = this;
            _MainTabItems.Add(new UIControlInfo { Title = "Welcome!", Content = new UserControls.WelcomePage() });
            _MainTabItems.Add(new UIControlInfo { Title = "Sql Favorites", Content = new UserControls.SqlFavorites() });
            _MainTabItems.Add(new UIControlInfo { Title = "Query!", Content = new UserControls.SqlInput() });
            _MainTabItems.Add(new UIControlInfo { Title = "SqlView1", Content = new UserControls.SqlView() });
            _MainTabItems.Add(new UIControlInfo { Title = "SqlView2", Content = new UserControls.SqlView() });
            _MainTabItems.Add(new UIControlInfo { Title = "SqlInput", Content = new UserControls.SqlInput() });
            InitializeComponent();
        }

        private ObservableCollection<UIControlInfo> _MainTabItems = new ObservableCollection<UIControlInfo>();
        public IEnumerable<UIControlInfo> MainTabItems
        {
            get
            {
                return _MainTabItems;
            }
        }

        private void OnOpenSqlite(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("open");
        }

        private void ShowLeft(object sender, RoutedEventArgs e)
        {
            ShowTreeList();
            this.ToggleFlyout(0);
        }

        private void ToggleFlyout(int index)
        {
            var flyout = this.Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return;
            }
            flyout.IsOpen = !flyout.IsOpen;
        }

        private void ShowTreeList()
        {
            List<SelectPropertyNodeItem> itemList = new List<SelectPropertyNodeItem>();

            SelectPropertyNodeItem item1 = new SelectPropertyNodeItem
            {
                DisplayName = "MainFrame",
            };
            itemList.Add(item1);

            SelectPropertyNodeItem In1item1 = new SelectPropertyNodeItem
            {
                DisplayName = "Inner Nodes Floder 1",
            };
            item1.Children.Add(In1item1);

            SelectPropertyNodeItem In1item2 = new SelectPropertyNodeItem
            {
                DisplayName = "Inner Nodes Floder 2",
            };
            item1.Children.Add(In1item2);

            SelectPropertyNodeItem In1item3 = new SelectPropertyNodeItem
            {
                DisplayName = "Inner Nodes Floder 3",
            };
            In1item2.Children.Add(In1item3);

            SelectPropertyNodeItem In1itemFile1 = new SelectPropertyNodeItem
            {
                DisplayName = "Inner Nodes File 1",
            };
            In1item3.Children.Add(In1itemFile1);

            SelectPropertyNodeItem item2 = new SelectPropertyNodeItem
            {
                DisplayName = "Nodes Floder 2",
            };
            itemList.Add(item2);

            for (int i = 0; i < 20; ++i)
            {
                SelectPropertyNodeItem item = new SelectPropertyNodeItem
                {
                    DisplayName = String.Format("Nodes Floder {0}", i),
                };
                itemList.Add(item);
            }

            SelectTreeView.ItemsSource = itemList;
        }
    }
}
