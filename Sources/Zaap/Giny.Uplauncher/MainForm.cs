using Giny.Uplauncher.Components;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Giny.Uplauncher
{
    public partial class MainForm : Form
    {
        private BlazorWebView view;
        public MainForm()
        {
            InitializeComponent();

            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddBlazorWebView();
            serviceCollection.AddMudServices();

            view = new BlazorWebView();
            view.Dock = DockStyle.Fill;
            
            view.HostPage = @"wwwroot\index.html";
            view.RootComponents.Add<MainLayout>("#app");
            view.Services = serviceCollection.BuildServiceProvider();


            Controls.Add(view);
        }
    }
}