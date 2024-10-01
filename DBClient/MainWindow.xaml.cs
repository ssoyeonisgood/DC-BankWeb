using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using RestSharp;
using APIClasses;
using System.Windows.Markup;
using System.IO;

namespace DBClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RestClient _client = new RestClient("http://localhost:5292");

        public MainWindow()
        {
            InitializeComponent();
            LoadTotalAccounts();
            LoadImage();
        }

        private async Task LoadImage()
        {
            try
            {
                RestRequest request = new RestRequest("api/image/getimage", Method.Get);
                RestResponse response = await _client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    // Convert the response data to a byte array and display it as an image
                    var imageBytes = response.RawBytes;
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = stream;
                        image.EndInit();

                        // Set the Image control source to the downloaded image
                        ImageBox.Source = image;
                    }
                }
                else
                {
                    MessageBox.Show("Error loading image: " + response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the image: " + ex.Message);
            }
        }

        private async void LoadTotalAccounts()
        {
            try
            {
                RestRequest request = new RestRequest("/api/GetValues/total", Method.Get);
                RestResponse response = await _client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    int totalAccounts = JsonConvert.DeserializeObject<int>(response.Content);
                    NoItems.Content = "Total Items: " + totalAccounts;
                }
                else
                {
                    MessageBox.Show("Error fetching data: " + response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchData mySearch = new SearchData();
                mySearch.searchString = LastnameBox.Text;
                RestRequest request = new RestRequest("api/search/", Method.Post);
                request.AddJsonBody(mySearch);
                RestResponse response = await _client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    DataIntermed data = JsonConvert.DeserializeObject<DataIntermed>(response.Content);
                    if (data != null) // Check if data is not null
                    {
                        FirstName.Text = data.firstName;
                        LastName.Text = data.lastName;
                        Balance.Text = data.balance.ToString("C");
                        AcctNo.Text = data.acctNo.ToString();
                        Pin.Text = data.pin.ToString("D4");
                    }
                    else
                    {
                        MessageBox.Show("No data found for the given last name.");
                    }
                }
                else
                {
                    MessageBox.Show("Error fetching data: " + response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }



        private async void GoButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = Int32.Parse(IndexBox.Text);
                RestRequest request = new RestRequest("api/getvalues/" + index.ToString());
                RestResponse response = await _client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    DataIntermed data = JsonConvert.DeserializeObject<DataIntermed>(response.Content);
                    FirstName.Text = data.firstName;
                    LastName.Text = data.lastName;
                    Balance.Text = data.balance.ToString("C");
                    AcctNo.Text = data.acctNo.ToString();
                    Pin.Text = data.pin.ToString("D4");
                }
                else
                {
                    MessageBox.Show("Error fetching data: " + response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
