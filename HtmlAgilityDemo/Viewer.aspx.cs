using System;
using HtmlAgilityDemo.Model;

namespace HtmlAgilityDemo
{
    public partial class WebForm2 : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
         {
            var _imagesRepository = new ImagesRepository();
            Repeater1.DataSource = _imagesRepository.GetDownloadedImages();
            Repeater1.DataBind();
        }
    }
}