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
        private Agent selectedAgent;
        private SortParam selectedSort;
        private Visibility changePriorityVisibility;
        private string search;
        private int lastPage;
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
        public ItemWithTitle<AgentType> SelectedType { get => selectedType; set { selectedType = value; OnPropertyChanged(); RefreshAgents(); } }
        public SortParam SelectedSort { get => selectedSort; set { selectedSort = value; OnPropertyChanged(); RefreshAgents(); } }
        public Agent SelectedAgent { get => selectedAgent; set { selectedAgent = value; OnPropertyChanged(); } }
        public Visibility ChangePriorityVisibility { get => changePriorityVisibility; set { changePriorityVisibility = value; OnPropertyChanged(); } }
        public string Search { get => search; set { search = value; OnPropertyChanged(); RefreshAgents(); } }
        public int SelectedPage { get => selectedPage; set { selectedPage = value; OnPropertyChanged(); } }

        private void InitializeFields()
        {
            search = string.Empty;
            itemsPerPage = 10;
            selectedPage = 1;
            lastPage = selectedPage;
            Pages = new ObservableCollection<int>();

            SortParams = new ObservableCollection<SortParam>
            {
                new SortParam("Наименование по возрастанию", "Name", true),
                new SortParam("Наименование по убыванию", "Name", false),
                new SortParam("Размер скидки по возрастанию", "Discount", true),
                new SortParam("Размер скидки по убыванию", "Discount", false),
                new SortParam("Приоритет агента по возрастанию", "Priority", true),
                new SortParam("Приоритет агента по убыванию", "Priority", false)
            };
            selectedSort = SortParams.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedSort));
            OnPropertyChanged(nameof(SelectedPage));
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
            //.Include("ProductSale").Include("AgentType")
            Agents = context.Agent.ToList();
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

            //Постаничный вывод
            filteredAgents = filteredAgents.Skip((SelectedPage - 1) * itemsPerPage).Take(itemsPerPage);

            DisplayedAgents = new ObservableCollection<Agent>(filteredAgents);

            Pages.Clear();
            var maxPage = MaxPage(Agents, itemsPerPage);
            for (int i = 1; i <= maxPage; i++)
            {
                Pages.Add(i);
            }
            SelectedPage = Pages.FirstOrDefault(p => p == SelectedPage);
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
            var filteredAgents = agents.Where(p => p.Name.ToLower().Contains(Search.ToLower()) ||
                                                       p.PhoneNumber.ToLower().Contains(Search.ToLower()) ||
                                                       p.Email.ToLower().Contains(Search.ToLower()));
            //Филтрация по типу
            filteredAgents = filteredAgents.Where(p => SelectedType.Item is null ? true : p.TypeId == SelectedType.Item.Id);
            return (int)Math.Ceiling((float)filteredAgents.Count() / (float)itemsPerPage);
        }

        private void Forward_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedPage <= MaxPage(Agents, itemsPerPage))
            {
                SelectedPage++;
            }
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedPage > 0)
            {
                SelectedPage--;
            }
        }

        private void pagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItems.Count > 1)
            {
                ChangePriorityVisibility = Visibility.Visible;
            }
            else
            {
                ChangePriorityVisibility = Visibility.Collapsed;
            }
            if (SelectedPage != lastPage)
            {
                lastPage = SelectedPage;
                RefreshAgents();
            }
        }

        private void ChangeOn_Click(object sender, RoutedEventArgs e)
        {
            var window = new ChangePriorityWindow(agentsList.SelectedItems.Cast<Agent>().Max(p => p.Priority));
            if (window.ShowDialog() == true)
            {
                foreach (var item in agentsList.SelectedItems.Cast<Agent>())
                {
                    item.Priority = window.Priority;
                }
                context.SaveChanges();
                LoadAgents();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = new AgentWindow();
            if (window.ShowDialog() == true)
            {
                LoadAgents();
                MessageBox.Show($"Агент {window.Agent.Name} успешно добавлен.", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (agentsList.SelectedItems.Count == 1)
            {
                var window = new AgentWindow(SelectedAgent);
                if(window.ShowDialog() == true)
                {
                    LoadAgents();
                    MessageBox.Show($"Агент {SelectedAgent.Name} успешно изменён.", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выбрано больше 1 элемента!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var window = new AgentWindow(SelectedAgent);
            if (window.ShowDialog() == true)
            {
                LoadAgents();
                MessageBox.Show($"Агент {SelectedAgent.Name} успешно изменён.", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
