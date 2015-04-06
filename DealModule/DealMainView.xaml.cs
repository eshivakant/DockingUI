using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZeroMQ;

namespace DealModule
{
    /// <summary>
    /// Interaction logic for DealMainView.xaml
    /// </summary>
    public partial class DealMainView : UserControl
    {
        public DealMainView()
        {
            InitializeComponent();

            this.DataContext = new DealMainViewModel();
        }
    }

    public class DealMainViewModel:INotifyPropertyChanged
    {
        private bool _shipmentActivated;

        public bool ShipmentActivated
        {
            get { return _shipmentActivated; }
            set
            {
                _shipmentActivated = value;
                OnPropertyChanged("ShipmentActivated");
            }
        }


        public DealMainViewModel()
        {
            
            Task.Factory.StartNew(SubscribeToModules);

            Task.Factory.StartNew(PublishMessage);
        }




        public static string Frontend = "tcp://127.0.0.1:2772";
        //public static string Backend = "tcp://127.0.0.1:2773";

        private void SubscribeToModules()
        {
            try
            {
                using (var ctx = ZmqContext.Create())
                {
                    using (var socket = ctx.CreateSocket(SocketType.SUB))
                    {
                        socket.SubscribeAll();
                        

                        while (true)
                        {
                            Thread.Sleep(5000);
                            var msg = socket.Receive(Encoding.UTF8);

                            if (msg == "ShipmentActivated")
                                ShipmentActivated = true;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
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
                            socket.Send("DealActivated", Encoding.UTF8);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                System.Windows.Forms.MessageBox.Show(exp.Message);
            }
        }

        //private void Process(ZMessage message)
        //{
        //    var msg = message.PopString(Encoding.UTF8);
        //    MessageBox.Show(msg);
        //    if (msg == "ShipmentActivated")
        //    {
        //        ShipmentActivated = true;
        //    }
        //}



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if(PropertyChanged!=null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
