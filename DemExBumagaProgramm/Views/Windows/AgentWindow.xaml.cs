using DemExBumagaProgramm.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace DemExBumagaProgramm.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AgentWindow.xaml
    /// </summary>
    public partial class AgentWindow : BaseWindow
    {
        private readonly BumagaContext context;
        private bool isAdd;
        private string search;
        private Product selectedProduct;
        private ObservableCollection<ProductSale> agentSales;
        private ObservableCollection<Product> displayedProducts;
        private ObservableCollection<AgentType> agentTypes;
        private ProductSale selectedAgentSale;
        public AgentWindow()
        {
            context = App.Context;
            Agent = new Agent();
            InitializeComponent();
            Initialize();
            LoadData();
            if (AgentSales == null)
            {
                AgentSales = new ObservableCollection<ProductSale>();
            }
            isAdd = true;
            DataContext = this;
        }
        public AgentWindow(Agent agent) : this()
        {
            Agent = agent;
            isAdd = false;
            foreach (var item in Agent.ProductSale)
            {
                AgentSales.Add(new ProductSale { Id = item.Id, Agent = item.Agent, Count = item.Count, DatefRealization = item.DatefRealization, Product = item.Product });
            }
        }

        public Agent Agent { get; set; }
        public string AgentName { get => Agent.Name; set { Agent.Name = value; OnPropertyChanged(); } }
        public AgentType AgentType { get => Agent.AgentType; set { Agent.AgentType = value; Agent.TypeId = value.Id; OnPropertyChanged(); } }
        public int Priority { get => Agent.Priority; set { Agent.Priority = value; OnPropertyChanged(); } }
        public string Adres { get => Agent.Addres; set { Agent.Addres = value; OnPropertyChanged(); } }
        public string INN { get => Agent.INN; set { Agent.INN = value; OnPropertyChanged(); } }
        public string KPP { get => Agent.KPP; set { Agent.KPP = value; OnPropertyChanged(); } }
        public string DirectorName { get => Agent.Director; set { Agent.Director = value; OnPropertyChanged(); } }
        public string PhoneNumber { get => Agent.PhoneNumber; set { Agent.PhoneNumber = value; OnPropertyChanged(); } }
        public string Email { get => Agent.Email; set { Agent.Email = value; OnPropertyChanged(); } }
        public byte[] Image { get => Agent.GetImage; set { Agent.GetImage = value; OnPropertyChanged(); } }
        public Product SelectedProduct { get => selectedProduct; set { selectedProduct = value; OnPropertyChanged(); SelectProduct(); } }
        public ObservableCollection<ProductSale> AgentSales { get => agentSales; set { agentSales = value; OnPropertyChanged(); } }
        public ObservableCollection<Product> DisplayedProducts { get => displayedProducts; set { displayedProducts = value; OnPropertyChanged(); } }
        public ObservableCollection<AgentType> AgentTypes { get => agentTypes; set { agentTypes = value; OnPropertyChanged(); } }
        public ProductSale SelectedProductSale { get => selectedAgentSale; set { selectedAgentSale = value; OnPropertyChanged(); } }
        public string Search { get => search; set { search = value; OnPropertyChanged(); RefreshProducts(); } }
        public List<Product> Products { get; set; }
        private void Initialize()
        {
            search = string.Empty;
        }
        private void LoadData()
        {
            LoadProducts();
            LoadTypes();
        }
        private void LoadProducts()
        {
            Products = new List<Product>(context.Product);
            RefreshProducts();
        }
        private void LoadTypes()
        {
            AgentTypes = new ObservableCollection<AgentType>(context.AgentType);
            if (AgentType == null)
            {
                AgentType = AgentTypes.FirstOrDefault();
            }
        }
        private void RefreshProducts()
        {
            var list = Products.OrderBy(p => p.Name).AsEnumerable();

            list = list.Where(p => p.Name.ToLower().Contains(Search.ToLower()));

            DisplayedProducts = new ObservableCollection<Product>(list);
        }
        private void SelectProduct()
        {

            if (SelectedProduct != null)
            {
                var window = new ProductCountWindow();
                if (window.ShowDialog() == true)
                {
                    AgentSales.Add(new ProductSale { Agent = Agent, Count = window.Count, Product = SelectedProduct, DatefRealization = DateTime.Now.Date });
                }
            }
        }

        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения | *.PNG, *.JPG, *.JPEG";
            if (openFileDialog.ShowDialog() == true)
            {
                Image = File.ReadAllBytes(openFileDialog.FileName);
            }
        }

        private void RemoveFromSales_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProductSale != null)
            {
                AgentSales.Remove(SelectedProductSale);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                if (Agent.ProductSale.Count > 0)
                {
                    Agent.ProductSale = new List<ProductSale>(AgentSales);
                    context.Entry(Agent).State = System.Data.Entity.EntityState.Modified;
                    //foreach (var item in AgentSales)
                    //{
                    //    if (item.Id == 0)
                    //    {
                    //        Agent.ProductSale.Add(item);
                    //    }
                    //}
                }
                else
                {
                    Agent.ProductSale = new List<ProductSale>(AgentSales);
                }
                if (isAdd)
                {
                    context.Agent.Add(Agent);
                    context.SaveChanges();
                }
                else
                {
                    context.SaveChanges();
                }
                DialogResult = true;
            }
        }

        private bool Validate()
        {
            return !string.IsNullOrEmpty(AgentName) &&
                   Priority >= 0 &&
                   !string.IsNullOrEmpty(Adres) &&
                   !string.IsNullOrEmpty(INN) &&
                   !string.IsNullOrEmpty(KPP) &&
                   !string.IsNullOrEmpty(DirectorName) &&
                   !string.IsNullOrEmpty(PhoneNumber) &&
                   !string.IsNullOrEmpty(Email);
        }
    }
}
