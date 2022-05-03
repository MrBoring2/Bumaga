using DemExBumagaProgramm.Data;
using DemExBumagaProgramm.Model;
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

namespace DemExBumagaProgramm.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AgentsList.xaml
    /// </summary>
    public partial class AgentsListWindow : BaseWindow
    {
        private readonly BumagaContext context;
        private ObservableCollection<Agent> displayedAgents;
        private ObservableCollection<int> pages;
        private ItemWithTitle<AgentType> selectedType;
        private SortParam selectedSort;
        private string search;
        private int itemsPerPage;
        private int selectedPage;
        public AgentsListWindow()
        {
            context = App.Context;
            InitializeComponent();
            InitializeFields();
            LoadData();
            DataContext = this;
        }
        public List<Agent> Agents { get; set; }
        public ObservableCollection<Agent> DisplayedAgents { get => displayedAgents; set { displayedAgents = value; OnPropertyChanged(); } }
        public ObservableCollection<int> Pages { get => pages; set { pages = value; OnPropertyChanged(); } }
        public ObservableCollection<ItemWithTitle<AgentType>> AgentTypes { get; set; }
        public ObservableCollection<SortParam> SortParams { get; set; }
        public ItemWithTitle<AgentType> SelectedType { get => selectedType; set { selectedType = value; OnPropertyChanged(); } }
        public SortParam SelectedSort { get => selectedSort; set { selectedSort = value; OnPropertyChanged(); } }
        public string Search { get => search; set { search = value; OnPropertyChanged(); } }

        private void InitializeFields()
        {
            search = string.Empty;
            itemsPerPage = 10;
            selectedPage = 1;
            SortParams = new ObservableCollection<SortParam>
            {
                new SortParam("Наименование", "Name", true),
                new SortParam("Наименование", "Name", false),
                new SortParam("Размер скидки", "Discount", true),
                new SortParam("Размер скидки", "Discount", false),
                new SortParam("Приоритет агента", "Priority", true),
                new SortParam("Приоритет агента", "Priority", false)
            };
            selectedSort = SortParams.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedSort));
        }

        private void LoadTypes()
        {
            AgentTypes = new ObservableCollection<ItemWithTitle<AgentType>>(context.AgentType.AsEnumerable().Select(p => new ItemWithTitle<AgentType>(p, p.Name)));
            AgentTypes.Insert(0, new ItemWithTitle<AgentType>(null, "Все"));
            selectedType = AgentTypes.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedType));
        }
        private void LoadAgents()
        {
            Agents = context.Agent.Include("ProductSale").Include("AgentType").ToList();
            RefreshAgents();
        }


        private void LoadData()
        {
            LoadTypes();
            LoadAgents();
        }
        private void RefreshAgents()
        {
            //Сортировка
            var filteredAgents = SortAgents(Agents);

            //Поиск
            filteredAgents = filteredAgents.Where(p => p.Name.ToLower().Contains(Search.ToLower()) ||
                                                       p.PhoneNumber.ToLower().Contains(Search.ToLower()) ||
                                                       p.Email.ToLower().Contains(Search.ToLower()));
            //Филтрация по типу
            filteredAgents = filteredAgents.Where(p => SelectedType.Item is null ? true : p.TypeId == SelectedType.Item.Id);

            DisplayedAgents = new ObservableCollection<Agent>(filteredAgents);
        }
        private IEnumerable<Agent> SortAgents(IEnumerable<Agent> agents)
        {
            if (SelectedSort.IsAscending)
            {
                return agents.OrderBy(p => p.GetProperty(SelectedSort.Property));
            }
            else
            {
                return agents.OrderByDescending(p => p.GetProperty(SelectedSort.Property));
            }
        }
        private int MaxPage(IEnumerable<Agent> agents, int itemsPerPage)
        {
            return (int)Math.Ceiling((float)agents.Count() / (float)itemsPerPage);
        }

    }
}
