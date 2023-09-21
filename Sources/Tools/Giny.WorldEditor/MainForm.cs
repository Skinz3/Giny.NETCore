using Giny.WorldEditor.Components;
using Microsoft.AspNetCore.Components.Web.Infrastructure;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Giny.WorldEditor
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
            serviceCollection.AddWindowsFormsBlazorWebView();
            serviceCollection.AddSingleton<AppState>();

            view = new BlazorWebView();
            view.AutoScroll = false;
            view.Dock = DockStyle.Fill;

            view.HostPage = @"wwwroot\index.html";
            view.RootComponents.Add<Home>("#app");
            view.Services = serviceCollection.BuildServiceProvider();

            Controls.Add(view);
        }
    }
}