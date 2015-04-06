using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ZeroMQ;

namespace ShipmentModule
{
    /// <summary>
    /// Interaction logic for ShipmentMainView.xaml
    /// </summary>
    public partial class ShipmentMainView : UserControl
    {
        public ShipmentMainView()
        {
            InitializeComponent();

            DataContext = new ShipmentMainViewModel();
        }
    }

    public class ShipmentMainViewModel : INotifyPropertyChanged
    {
        private bool _dealActivated;

        public bool DealActivated
        {
            get { return _dealActivated; }
            set
            {
                _dealActivated = value;
                OnPropertyChanged("DealActivated");
            }
        }

        private Dispatcher _mainDispatcher;

        public ShipmentMainViewModel()
        {

            _mainDispatcher = Dispatcher.CurrentDispatcher;

            Task.Factory.StartNew(SubscribeToModules);
            Task.Factory.StartNew(PublishMessage);

        }


        

        public static string Frontend = "tcp://127.0.0.1:2772";
        //public static string Backend = "tcp://127.0.0.1:2773";
        private void SubscribeToModules()
        {
            MessageBoxShow("b4 SubscribeAll");

            try
            {
                using (var ctx = ZmqContext.Create())
                {
                    MessageBoxShow("b4 SubscribeAll");

                    using (var socket = ctx.CreateSocket(SocketType.SUB))
                    {
                        MessageBoxShow("b4 SubscribeAll");

                        socket.SubscribeAll();


                        while (true)
                        {
                            Thread.Sleep(5000);
                            var msg = socket.Receive(Encoding.UTF8);

                            MessageBoxShow(msg + "received");

                            if (msg == "DealActivated")
                                DealActivated = true;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBoxShow(exp.Message);
            }
        }

        void MessageBoxShow(string msg)
        {
            _mainDispatcher.BeginInvoke(new Action(() => MessageBox.Show(msg)));
        }

        private void PublishMessage()
        {
            try
            {
                using (var ctx = ZmqContext.Create())
                {
                    using (var socket = ctx.CreateSocket(SocketType.PUB))
                    {
                        socket.Bind(Frontend);

                        long msgCptr = 0;
                        int msgIndex = 0;
                        while (true)
                        {
                            Thread.Sleep(10000);
                            socket.Send("ShipmentActivated", Encoding.UTF8);
                            MessageBox.Show("ShipmentActivated sent");
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
