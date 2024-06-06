namespace ExmenProgreso2MateoHerrera;
using System;
using System.IO;
using System.Threading.Tasks;



    public partial class MainPage : ContentPage
    {
        public string montoSeleccionado;

        public MainPage()
        {
            InitializeComponent();
        }

    private void RecargaCantidad(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var radioButton = sender as RadioButton;
            montoSeleccionado = radioButton.Content.ToString();
            DisplayAlert("Monto seleccionado", $"Has seleccionado una recarga de {montoSeleccionado} dólares.", "OK");
        }
    }

    private async void RecargarClicked(object sender, EventArgs e)
    {
        var numeroTelefono = MHCelular.Text;
        var operador = MHSeleccionarOperado.SelectedItem?.ToString();

        if (string.IsNullOrEmpty(numeroTelefono) || string.IsNullOrEmpty(operador) || string.IsNullOrEmpty(montoSeleccionado))
        {
            await DisplayAlert("Error", "Existen campos vacios", "OK");
            return;
        }

        var confirm = await DisplayAlert("Confirmar", $"¿Desea realizar una recarga de {montoSeleccionado} dólares al número {numeroTelefono}?", "Sí", "No");
        if (confirm)
        {
            await RealizarRecargaAsync(numeroTelefono, montoSeleccionado);
        }
    }

    private async Task RealizarRecargaAsync(string numeroTelefono, int monto)
    {
        await Task.Delay(2000);

        var fecha = DateTime.Now.ToString("dd/MM/yyyy");
        var textoRecarga = $"Se hizo una recarga de {monto} dólares en la siguiente fecha: {fecha}";

        // Guardar archivo con el número de teléfono
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{numeroTelefono}.txt");
        File.WriteAllText(path, textoRecarga);

        // Mostrar mensaje de confirmación
        await DisplayAlert("Confirmación", "Recarga realizada con éxito", "OK");
        MHConfirmacionRecarga.Text = textoRecarga;
        MHConfirmacionRecarga.IsVisible = true;
    }


}

