using System;
using System.Collections.Generic;
using Gecko.WebWallpaper;
using Gecko.WebWallpaper.Model;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace HtmlAgilityDemo
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        Downloader webWorker = new Downloader();

        protected void btnGetImages_Click(object sender, EventArgs e)
        {
    
            var cacheItem = Cache[ConfigAppOptions.cacheKey] as IEnumerable<SearchItemResult>;
            if (cacheItem == null)
            {
               var output = webWorker.GetImages(txtUrl.Text);
               Cache[ConfigAppOptions.cacheKey] = JsonConvert.DeserializeObject<IEnumerable<SearchItemResult>>(output);
            }
            else
            {
                lblSummary.Text = "using the cache!!!";
            }

            Panel2.Visible = true;
       
            ImageGrid.DataSource = Cache[ConfigAppOptions.cacheKey] as IEnumerable<SearchItemResult>;
            ImageGrid.DataBind();
        }

        protected void btnDnloadImages_Click(object sender, EventArgs e)
        {
           
            var results = new List<SearchItemResult>();
            var downloadfilefolder = txtDownloadFolder.Text;

            if (downloadfilefolder.Length == 0)
            {
                downloadfilefolder = ConfigAppOptions.ImageFolder;
            }

            // Looping through all the rows in the GridView
            foreach (GridViewRow row in ImageGrid.Rows)
            {
                CheckBox checkbox = (CheckBox)row.FindControl("CheckBox1");

                //Check if the checkbox is checked.
                //value in the HtmlInputCheckBox's Value property is set as the //value of the delete command's parameter.
                if (checkbox.Checked)
                {
                    // Retreive the Employee ID
                   Label urlField = (Label)row.FindControl("ThumbImageUrl");
                   results.Add(new SearchItemResult
                   {
                       ThumbImageUrl = urlField.Text
                   });
       
                }
            }

            webWorker.DownloadImage(downloadfilefolder, JsonConvert.SerializeObject(results));
        }

        protected void btnSaveResults_Click(object sender, EventArgs e)
        {
            var results = Cache[ConfigAppOptions.cacheKey] as IEnumerable<SearchItemResult>;
            string json = JsonConvert.SerializeObject(results);
            webWorker.Save(json);
        }

        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
            }
        }
    }
}