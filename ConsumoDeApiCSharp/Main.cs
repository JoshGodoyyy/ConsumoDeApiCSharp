using ConsumoDeApiCSharp.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumoDeApiCSharp {
    public partial class Main : Form {
        private List<User> _users = new List<User>();
        public Main() {
            InitializeComponent();
            GetData();
        }

        private async Task GetData() {
            string url = "https://jsonplaceholder.typicode.com/";
            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(url) };
            var response = await httpClient.GetAsync("users");
            var result = await response.Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<User[]>(result);

            foreach(var user in users) {
                _users.Add(user);
            }

            lstPessoas.DataSource = _users;
            lstPessoas.DisplayMember = "Name";
            lstPessoas.ValueMember = "Id";
        }

        private void lstPessoas_SelectedIndexChanged(object sender, EventArgs e) {
            var model = (User)lstPessoas.SelectedItem;
            nomeTxt.Text = model.Name;
            emailTxt.Text = model.Email;
            ruaTxt.Text = model.Address.Street;
            complementoTxt.Text = model.Address.Suite;
            cidadeTxt.Text = model.Address.City;
            cepTxt.Text = model.Address.ZipCode;
            contatoTxt.Text = model.Phone;
            empresaTxt.Text = model.Company.Name;
        }
    }
}
