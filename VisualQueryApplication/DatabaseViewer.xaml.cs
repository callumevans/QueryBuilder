﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
using VisualQueryApplication.ViewModels;

namespace VisualQueryApplication
{
    /// <summary>
    /// Interaction logic for DatabaseViewer.xaml
    /// </summary>
    public partial class DatabaseViewer : Window
    {
        private readonly Action CallbackOnClose;

        public DatabaseViewer(Action parentCallbackOnClose)
        {
            InitializeComponent();

            CallbackOnClose = parentCallbackOnClose;
            this.Closed += DatabaseViewer_Closed;
        }

        private void DatabaseViewer_Closed(object sender, EventArgs e)
        {
            CallbackOnClose.Invoke();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (DatabaseViewerViewModel)this.DataContext;
            viewModel.SelectedTableChangedCommand.Execute(DatabaseTablesList.SelectedItem.ToString());
        }
    }
}
