using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ListaMunicipioApp
{
    public partial class MainPage : ContentPage
    {
        async void retorno (string json)
        {
            if (json == "[]")
            {
                await DisplayAlert("Erro", "Sua UF esta invalida", "Ok");
                return;
            }
        }

        Boolean validateUf(string valor){
            if(valor != "[]"){
                return true;
            }
            return false;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var client = new HttpClient();
            string uf = txtUF.Text.ToUpper();
            var json = await client.GetStringAsync($"http://ibge.herokuapp.com/municipio/?val={uf}");
            var dados = JsonConvert.DeserializeObject<Object>(json);

            //retorno(json);

            if(!validateUf(json)){
                await DisplayAlert("Erro", "Sua UF esta invalida", "Ok");
                return;
            }

            JObject municipios = JObject.Parse(json);

            Dictionary<string, string> dadosMunicipios = municipios.ToObject<Dictionary<string, string>>();

            ArrayList lista = new ArrayList();

            foreach(KeyValuePair<string, string> municipio in dadosMunicipios){
                lista.Add(municipio.Key);
            }

            listaMunicipios.ItemsSource = lista;

        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
