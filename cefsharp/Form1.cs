using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using System.Windows.Forms.VisualStyles;
using CefSharp.Internals;
using System.Web;
using Newtonsoft.Json;

namespace cefsharp
{
    public partial class Form1 : Form
    {
        private ChromiumWebBrowser chromeobject;
        public Form1()
        {
            InitializeComponent();
            initialise();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        public void initialise()
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            chromeobject = new ChromiumWebBrowser(string.Format(@"{0}\Items.html",Application.StartupPath));
            BrowserSettings browsersetting = new BrowserSettings
            {
                FileAccessFromFileUrls = CefState.Enabled,
                UniversalAccessFromFileUrls = CefState.Enabled
            };
            chromeobject.BrowserSettings = browsersetting;
            this.Controls.Add(chromeobject);
            chromeobject.Dock = DockStyle.Fill;
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            CefSharpSettings.WcfEnabled = true;
            chromeobject.JavascriptObjectRepository.Register("sendobj", new Val(), isAsync: false, options: BindingOptions.DefaultBinder);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
    public class Val
    {
        private List<string> Values = new List<string>();
        public void send(string Val) => Values.Add(Val);
        public string Array()
        {
            return JsonConvert.SerializeObject(Values);
        }
    }
}
