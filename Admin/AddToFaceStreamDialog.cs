using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoOS.Platform;
using VLAlarmViewer.Common;

namespace VLAlarmViewer.Admin
{
    public partial class AddToFaceStreamDialog : Form
    {
        // maps list_data to list id
        private Dictionary<string, string> loadedLists = new Dictionary<string, string>();

        private static string tokenId = "6d071cca-fda5-4a03-84d5-5bea65904480";

        // FS url.
        // Always take url from credentials when building requests, never take text from txt boxes directly!
        private Credentials credentials;

        public AddToFaceStreamDialog(Credentials credentials, string streamUrl, string streamName)
        {
            InitializeComponent();

            tbName.Text = streamName;
            tbUrl.Text = streamUrl;

            this.credentials = credentials;
        }

        /// <summary>
        /// This is for json serialization only.
        /// Note the lowercase properties.
        /// </summary>
        private class Stream
        {
            public string name { get; set; }

            public class Input
            {
                public string url { get; set; }
                public string transport { get; set; }
            }

            private Input _input = new Input();

            public Input input
            {
                get { return _input; }
                set { _input = value; }
            }

            public class Output
            {
                public string url { get; set; }
                public string luna_account_id { get; set; }

            }

            private Output _output = new Output();

            public Output output
            {
                get { return _output; }
                set { _output = value; }
            }
        }

        private Stream MakeStreamFromCurrentSettings()
        {
            if (cbLists.SelectedItem == null)
            {
                MessageBox.Show("Select a list!");
                return null;
            }

            var listId = loadedLists[(string)cbLists.SelectedItem];

            var stream = new Stream();
            stream.name = tbName.Text;

            stream.input.url = tbUrl.Text;
            stream.input.transport = cbUdp.Checked ? "udp" : "tcp";
            stream.output.url = Luna.MakeSearchURL(credentials.Luna.Url, listId);
            stream.output.luna_account_id = tokenId;//luna account

            return stream;
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                var url = FaceStream.MakeStreamsURL(credentials.FaceStream.Url);
                var request = WebRequest.Create(url);

                request.ContentType = "application/json";
                request.Method = "POST";

                var stream = MakeStreamFromCurrentSettings();
                if (stream == null)
                    return;

                var body = new List<Stream> { stream };


                var bodyJson = JsonConvert.SerializeObject(body, Formatting.None);
                bodyJson = bodyJson.Replace("luna_account_id", "luna-account-id");
                var bodyBytes = Encoding.UTF8.GetBytes(bodyJson);

                request.ContentLength = bodyBytes.Length;

                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bodyBytes, 0, bodyBytes.Length);

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        if (cbOpenPreview.Checked)
                        {
                            var o = Util.ParseResponseJsonGeneric(response);
                            var streamIds = (JArray)o;

                            if (streamIds.Count > 0)
                                Util.OpenBrowser(FaceStream.MakeStreamPreviewURL(
                                    credentials.FaceStream.Url, (string)(streamIds[0])));
                        }
                        else
                            MessageBox.Show("Camera added successfully.", "Success");
                    }
                }
            }
            catch (System.Net.WebException exc)
            {
                if (!Util.TryHandleError(exc, "Failed to add camera"))
                    throw;
            }
            catch (System.UriFormatException)
            {
                Util.DisplayError($"Invalid FaceStream endpoint specified");
            }
            catch (System.NotSupportedException)
            {
                Util.DisplayError($"Invalid FaceStream endpoint protocol specified");
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Failed to add camera due to error {exc}", "Error");
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static void AppendListFromJson(Dictionary<string, string> lists, JToken token)
        {
            var id = (string)token["handler_id"];
            var data = (string)token["description"];

            lists.Add(data, id);
        }
        private int GetNumOfHandlers()
        {
            try
            {
                var baseUrl = Luna.MakeHandlersCountURL(credentials.Luna.Url);
                var request = WebRequest.Create(baseUrl);
                credentials.Luna.AddBasicAuth(request);
                int count;
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var o = Util.ParseResponseJson(response);
                    count = Convert.ToInt32(((JValue)o["handlers_count"]).ToString());
                    return count;
                }

            }
            catch (WebException exc)
            {
                if (!Util.TryHandleError(exc, "Failed to download list info"))
                    throw;
                return 0;
            }
        }
        private void AddToFaceStreamDialog_Load(object sender, EventArgs e)
        {

            try
            {
                GetNumOfHandlers();
                loadedLists.Clear();
                var currentPage = 1;
                var totalLists = GetNumOfHandlers();
                var baseUrl = Luna.MakeHandlersURL(credentials.Luna.Url);

                do
                {
                    var url = Luna.AppendPagination(baseUrl, currentPage);
                    var request = WebRequest.Create(url);
                    credentials.Luna.AddBasicAuth(request);

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        var o = Util.ParseResponseJsonArray(response);
                        foreach (var list in o)
                        {
                            var id = (string)list["handler_id"];
                            var data1 = (string)list["description"];
                            if (loadedLists.ContainsKey(data1))
                            { data1 = data1 + loadedLists.Count.ToString(); }
                            loadedLists.Add(data1, id);
                        }

                    }

                    currentPage++;
                }
                while (loadedLists.Count < totalLists);

                var data = new List<string>();
                foreach (var list in loadedLists)
                    data.Add(list.Key.Length > 0 ? list.Key : list.Value);

                cbLists.DataSource = data;

            }
            catch (System.Net.WebException exc)
            {
                if (!Util.TryHandleError(exc, "Failed to download list info"))
                    throw;
            }
            catch (System.UriFormatException)
            {
                Util.DisplayError($"Invalid LUNA endpoint specified");
            }
            catch (System.NotSupportedException)
            {
                Util.DisplayError($"Invalid LUNA endpoint protocol specified");
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Failed to download list info due to error {exc}", "Error");
            }
        }

        private void tbEndpoint_TextChanged(object sender, EventArgs e)
        {
            credentials.FaceStream.Url = tbEndpoint.Text;
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            if (tbName.Text.Length > 20)
            { btnOk.Enabled = false; }
            else { btnOk.Enabled = true; }
        }
    }
}
