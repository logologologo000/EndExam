using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text;

namespace exam.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            ViewData["result"] = "result";
            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:44303");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var getres = await client.GetAsync("api/cal");
            getres.EnsureSuccessStatusCode();

            if (getres.IsSuccessStatusCode)
            {
                var getcals = await getres.Content.ReadFromJsonAsync<IEnumerable<Cal>>();
                ViewData["Cals"] = getcals;
            }
            
        }

        [BindProperty]
        public int num1 { get; set; }
        [BindProperty]
        public int num2 { get; set; }
        [BindProperty]
        public string marks { get; set; }
        [BindProperty]
        public int resu { get; set; }

        public async Task OnPost()
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:44303");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            

            //dynamic data = new ExpandoObject();
            //data.num1 = 12;
            //data.num2 = 35;

            Cal cals = new Cal
            {
                num1 = this.num1,
                num2 = this.num2,
                marks = this.marks
            };

            string convertJason = Newtonsoft.Json.JsonConvert.SerializeObject(cals);

            var httpcontent = new StringContent(convertJason.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/cal", httpcontent);
            var endpoint = await response.Content.ReadAsStringAsync();
            ViewData["result"] = endpoint;

            var getres = await client.GetAsync("api/cal");

            var getcals = await getres.Content.ReadFromJsonAsync<IEnumerable<Cal>>();
            ViewData["Cals"] = getcals;






            //Response.Redirect("/Privacy");

        }




    }
}