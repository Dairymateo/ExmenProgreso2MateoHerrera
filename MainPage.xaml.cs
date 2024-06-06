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

        }
    }

    private async void RecargarClicked(object sender, EventArgs e)
    {
        var numeroTelefono = MHCelular.Text;
        var operador = MHSeleccionarOperado.SelectedItem?.ToString();

        if (string.IsNullOrEmpty(numeroTelefono) || string.IsNullOrEmpty(operador) || string.IsNullOrEmpty(montoSeleccionado))
        {
            await DisplayAlert("Error", "Por favor complete todos los campos.", "OK");
            return;
        }

        var confirm = await DisplayAlert("Confirmar", $"¿Desea realizar una recarga de {montoSeleccionado} dólares al número {numeroTelefono}?", "Sí", "No");
        if (confirm)
        {
            await RealizarRecargaAsync(numeroTelefono, montoSeleccionado);
            //GuardarEnArchivo(numeroTelefono, montoSeleccionado);
        }
    }

    private async Task RealizarRecargaAsync(string numeroTelefono, string monto)
    {
        

        var fecha = DateTime.Now.ToString("dd/MM/yyyy");
        var textoRecarga = $"Se hizo una recarga de {monto} dólares en la siguiente fecha: {fecha}";

        
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{numeroTelefono}.txt");
        File.WriteAllText(path, textoRecarga);
        await DisplayAlert("Confirmación", "Recarga realizada con éxito", "OK");
        MHConfirmacionRecarga.Text = textoRecarga;
        MHConfirmacionRecarga.IsVisible = true;
    }




   }


