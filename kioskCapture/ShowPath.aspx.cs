using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace kioskCapture
{
    public partial class ShowPath : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Request.QueryString["uid"] != null)
                {
                    string uid = Request.QueryString["uid"].ToString();
                    if (uid != null && uid != "")
                    {
                        using (var httpClientHandler = new HttpClientHandler())
                        {
                            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                            using (var client = new HttpClient(httpClientHandler))
                            {
                                client.BaseAddress = new Uri("https://localhost:7274/Image/");
                                //HTTP GET
                                var responseTask = client.GetAsync(uid);
                                responseTask.Wait();

                                var result = responseTask.Result;
                                if (result.IsSuccessStatusCode)
                                {
                                    var readTask = result.Content.ReadAsStreamAsync();

                                    string path = AppDomain.CurrentDomain.BaseDirectory + "\\images\\img1.png";
                                    using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
                                    {
                                        readTask.Result.CopyTo(outputFileStream);
                                    }

                                    Image1.ImageUrl = "\\images\\img1.png";
                                }
                            }
                        }
                    }
                }               
                
            }
        }
    }
}