using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApplication1
{
    public delegate void ProgDel(string prog);

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DasClass_UpdateProg(string prog)
        {
            this.muhLabel.Content = prog;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MuhClass dasClass = new MuhClass();
            dasClass.UpdateProg += DasClass_UpdateProg;
            TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory.StartNew(async () => {
                await dasClass.DoStuff();
            }, CancellationToken.None, TaskCreationOptions.None, uiScheduler);
        }
    }

    public class MuhClass
    {
        public event ProgDel UpdateProg;

        public MuhClass() { }

        public async Task DoStuff()
        {
            int progress = 10;

            for (int i = 0; i < 100; i++)
            {
                UpdateProg(progress.ToString());
                Thread.Sleep(100);

                progress += 5;
            }
        }
    }
}
