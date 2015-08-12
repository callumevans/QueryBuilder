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
using System.Windows.Shapes;

namespace VisualQueryApplication
{
    /// <summary>
    /// Interaction logic for GeneratedQueryView.xaml
    /// </summary>
    public partial class GeneratedQueryView : Window
    {
        private readonly Action CallbackOnClose;

        public GeneratedQueryView(Action parentCallbackOnClose)
        {
            InitializeComponent();

            CallbackOnClose = parentCallbackOnClose;
            this.Closed += GeneratedQueryView_Closed;
        }

        private void GeneratedQueryView_Closed(object sender, EventArgs e)
        {
            CallbackOnClose.Invoke();
        }
    }
}