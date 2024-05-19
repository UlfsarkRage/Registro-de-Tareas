using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TareasApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}

public class Tarea
{
    
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Categoria { get; set; }
    public bool EstaCompletada { get; set; }
}

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}


public class MainPageViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Tarea> TareasPendientes { get; set; }
    public ObservableCollection<Categoria> Categorias { get; set; }
    public Tarea SelectedTarea { get; set; }

    public Command AgregarTareaCommand { get; }
    public Command<Tarea> EliminarTareaCommand { get; }
    public Command<Tarea> MarcarComoCompletadaCommand { get; }

    public MainPageViewModel()
    {
        // Inicializar las colecciones y comandos
        TareasPendientes = new ObservableCollection<Tarea>();
        Categorias = new ObservableCollection<Categoria>();

        AgregarTareaCommand = new Command(OnAgregarTarea);
        EliminarTareaCommand = new Command<Tarea>(OnEliminarTarea);
        MarcarComoCompletadaCommand = new Command<Tarea>(OnMarcarComoCompletada);

        // Cargar las tareas y categorías
        CargarTareas();
        CargarCategorias();
    }

    private void CargarTareas()
    {
        // Cargar tareas desde la base de datos
    }

    private void CargarCategorias()
    {
        // Llamar al WebAPI para obtener las categorías
    }

    private void OnAgregarTarea()
    {
        // Navegar a la página de agregar tarea
    }

    private void OnEliminarTarea(Tarea tarea)
    {
        // Eliminar tarea de la base de datos y actualizar la lista
    }

    private void OnMarcarComoCompletada(Tarea tarea)
    {
        // Marcar tarea como completada y actualizar la lista
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}



public class TareaDatabase
{
    private readonly SQLiteAsyncConnection _database;

    public TareaDatabase(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Tarea>().Wait();
    }

    public Task<List<Tarea>> GetTareasAsync()
    {
        return _database.Table<Tarea>().ToListAsync();
    }

    public Task<int> SaveTareaAsync(Tarea tarea)
    {
        if (tarea.Id != 0)
        {
            return _database.UpdateAsync(tarea);
        }
        else
        {
            return _database.InsertAsync(tarea);
        }
    }

    public Task<int> DeleteTareaAsync(Tarea tarea)
    {
        return _database.DeleteAsync(tarea);
    }
}

public class ApiService
{
    private readonly HttpClient _client;

    public ApiService()
    {
        _client = new HttpClient();
    }

    public async Task<List<Categoria>> GetCategoriasAsync()
    {
        var response = await _client.GetStringAsync("https://eqtools.eqtax.com:8585/api/TechnicalTest");
        return JsonConvert.DeserializeObject<List<Categoria>>(response);
    }
}
