using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Controls;
using System.Windows.Documents;
using MahApps.Metro.Controls;



namespace Scaner_port
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
       
        public MainWindow()
        {
            InitializeComponent();
            List<PortInfo> pi = FiltrPort();
            listView.ItemsSource = pi;
        }

        private static List<PortInfo> GetOpenPort()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcEndPoints = properties.GetActiveTcpListeners();
            TcpConnectionInformation[] tcpConnections = properties.GetActiveTcpConnections();

            return tcpConnections.Select(p =>
            {
                return  new PortInfo(
                    i: p.LocalEndPoint.Port,
                    local: string.Format("{0}", p.LocalEndPoint.Address),
                    remote: string.Format("{0}", p.RemoteEndPoint.Address),
                    state: p.State.ToString()
                    );
            }).ToList();
        }

        private static List<PortInfo> FiltrPort()
        {
            List<PortInfo> port = MainWindow.GetOpenPort();
            List<PortInfo> portnew = new List<PortInfo>();
            string[] ipTileService = new string[] { "91.236.50.81", "91.236.50.36", "91.236.50.33", "10.54.18.212", "10.54.18.210" };

            for (int i = 0; i < ipTileService.Length; i++)
                foreach (var portinf in port)
                {
                    if (portinf.Remote == ipTileService[i])
                    {
                        portnew.Add(portinf);
                    }
                }
            return portnew;
        }

        private void GridViewColumnHeader_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                listView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            listView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        private void btnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<PortInfo> pi = FiltrPort();
            listView.ItemsSource = pi;
        }
    }
}
