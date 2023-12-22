using Giny.Core.IO.Configuration;
using Giny.Uplauncher.Components;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Giny.Uplauncher
{
    public partial class MainForm : Form
    {
        public static MainForm Instance { get; private set; }

        private BlazorWebView view;
        public MainForm()
        {

            Instance = this;

            InitializeComponent();


            var backColor = AppState.GinyTheme.PaletteDark.Surface;

            this.BackColor = Color.FromArgb(backColor.R, backColor.G, backColor.B);

            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddBlazorWebView();
            serviceCollection.AddWindowsFormsBlazorWebView();
            serviceCollection.AddMudServices();

            view = new BlazorWebView();

            view.Dock = DockStyle.Fill;

            view.HostPage = @"wwwroot\index.html";
            view.WebView.DefaultBackgroundColor = Color.Transparent;
            view.RootComponents.Add<MainLayout>("#app");
            view.Services = serviceCollection.BuildServiceProvider();

            Controls.Add(view);



            ConfigManager<UplConfig>.Load(UplConfig.Filepath);

        }



        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public static void HandleMouseMove()
        {
            ReleaseCapture();
            SendMessage(Instance.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

    }
}