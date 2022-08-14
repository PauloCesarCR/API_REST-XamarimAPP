using App2.Service;
using System;
using WebApplication1.Models.Entities.Users;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        private PostUsersRequest user;
        private Api api;

        public MainPage()
        {
            InitializeComponent();
            api = new Api();
        }

        private async void btCadastrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                user = new PostUsersRequest();
                user.firstName = FirstName.Text;
                user.surName = SurName.Text;
                user.age = Convert.ToInt32(Age.Text);

                await api.CreateUser(user);
                await DisplayAlert("Alerta", "Operação realizada com sucesso", "Ok");
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "Ok");
            }
        }

    }
}
