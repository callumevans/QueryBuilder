using DataTypes;
using Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using VisualQueryApplication.ViewModels;

namespace VisualQueryApplication.Controls.GraphBuilder
{
    /// <summary>
    /// Interaction logic for VisualChartBox.xaml
    /// </summary>
    public partial class VisualGraphBox : UserControl
    {
        public VisualGraphBox()
        {
            InitializeComponent();
            Loaded += VisualGraphBox_Loaded;
        }

        private void VisualGraphBox_Loaded(object sender, RoutedEventArgs e)
        {
            // Update model and get data type labels

        }

        private void RefreshInputs()
        {
            /*
            foreach (FieldInfo input in ((VisualNodeViewModel)DataContext).Inputs)
            {
                StackPanel childPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    MinHeight = 20,
                    Margin = new Thickness(0, 4, 0, 4)
                };

                childPanel.Children.Add(new NodePin()
                {
                    Width = 12,
                    Height = 12,
                    Margin = new Thickness(3),
                    HorizontalAlignment = HorizontalAlignment.Right
                });

                childPanel.Children.Add(new TextBlock()
                {
                    Text = input.Name,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Width = 50,
                    Height = 12,
                    LineHeight = 12,
                    FontSize = 9,
                    VerticalAlignment = VerticalAlignment.Center
                });

                InputsPanel.Children.Add(childPanel);
            }*/
        }

        private void RefreshOutputs()
        {
            /*
            foreach (FieldInfo output in ((VisualNodeViewModel)DataContext).Outputs)
            {
                StackPanel childPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    MinHeight = 20,
                    Margin = new Thickness(0, 4, 0, 4)
                };

                childPanel.Children.Add(new TextBlock()
                {
                    Text = output.Name,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    TextAlignment = TextAlignment.Right,
                    Width = 50,
                    Height = 12,
                    LineHeight = 12,
                    FontSize = 9,
                    VerticalAlignment = VerticalAlignment.Center
                });

                childPanel.Children.Add(new NodePin()
                {
                    Width = 12,
                    Height = 12,
                    Margin = new Thickness(3),
                    HorizontalAlignment = HorizontalAlignment.Right
                });

                OutputsPanel.Children.Add(childPanel);
            }*/
        }
    }
}
