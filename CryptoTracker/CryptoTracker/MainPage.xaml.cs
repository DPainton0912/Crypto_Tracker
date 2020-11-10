using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using CryptoTracker.Models;
using Xamarin.Forms;

namespace CryptoTracker
{
    public partial class MainPage : ContentPage
    {
        private string apiKey = "A9B8F488-3EAD-4F67-B2BB-05EADF44FBAE";
        private string baseImageUrl = "https://s3.eu-central-1.amazonaws.com/bbxt-static-icons/type-id/png_64/";
        public MainPage()
        {
            InitializeComponent();
            CoinListView.ItemsSource = GetCoins();
        }

        private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            CoinListView.ItemsSource = GetCoins();
        }

        private List<Coins> GetCoins()
        {
            List<Coins> coins;

            var client = new RestClient("https://rest.coinapi.io/v1/assets?filter_asset_id=BTC;ETH;XMR;LTC;DASH");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-CoinAPI-Key", apiKey);

            var response = client.Execute(request);

            coins = JsonConvert.DeserializeObject<List<Coins>>(response.Content);

            foreach (var c in coins)
            {
                if (!String.IsNullOrEmpty(c.Id_Icon))
                    c.Icon_Url = baseImageUrl + c.Id_Icon.Replace("-", "") + ".png";
                else
                    c.Icon_Url = "";
            }

            return coins;
        }
    }
}
