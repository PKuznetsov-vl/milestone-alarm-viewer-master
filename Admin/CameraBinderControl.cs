using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Admin;
using VLAlarmViewer.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VideoOS.Platform.Messaging;
using VideoOS.Platform.Data;
using VideoOS.Platform.ConfigurationItems;

namespace VLAlarmViewer.Admin
{
    public partial class CameraBinderControl : ItemNodeUserControl
    {
        // maps token_data to token id
        private Dictionary<string, string> loadedTokens = new Dictionary<string, string>();

        private FQID currentCameraId = null;
        public List<string> camerasList;
        // Luna url user and password.
        // Always take those from credentials when building requests, never take text from txt boxes directly!
        private Credentials credentials = null;

        public CameraBinderControl(Credentials credentials)
        {
            InitializeComponent();

            // we will be modifying these from the ui
            this.credentials = credentials;
            credentials.Luna.Url = "http://127.0.0.1:5000/";
            // preinit with initial info
            tbEndpoint.Text = credentials.Luna.Url;
            //  tbUser.Text = credentials.Luna.User;
            //tbPass.Text = credentials.Luna.Pass;

            itemPickerUserControl.ShowDisabledItems = false;
            itemPickerUserControl.SingleSelect = true;
            itemPickerUserControl.KindFilter = new List<Guid>() { Kind.Camera };

            itemPickerUserControl.Init();
            //var list3 = Configuration.Instance.GetItemsByKind(Kind.Camera, ItemHierarchy.SystemDefined);


            var list1 = Configuration.Instance.GetItems(ItemHierarchy.SystemDefined);
            itemPickerUserControl.ItemsToSelectFromServer = list1;
            var list2 = Configuration.Instance.GetItems(ItemHierarchy.UserDefined);
            itemPickerUserControl.ItemsToSelectFromGroup = list2;

            itemPickerUserControl.ValidateSelectionEvent += this.ValidateSelectionHandler;
            DisableControls();
        }

        private void DisableControls()
        {
            const string defaultText = "(no camera selected)";

            lblCamera.Text = defaultText;
            //   lbStatus.Text = defaultText;

            //btnBind.Enabled = false;
            // btnUnbind.Enabled = false;
            btnAddToFS.Enabled = false;

        }
        /*  private void write(string item)
          {
             string filename = Luna.filename; 
              if (!File.Exists(filename))
              {
                  File.Create(filename);
              }
              camerasList = File.ReadAllLines(filename).ToList();


              if (camerasList == null || camerasList.Contains(item) == false)
              {
                  using (StreamWriter sw = new StreamWriter(filename,true))
                  {
                      sw.WriteLine(item);
                  }
              }
             else return;
          }*/


        private void ValidateSelectionHandler(VideoOS.Platform.UI.ItemPickerForm.ValidateEventArgs e)
        {
            //HandleCameraChanged() may call XProtcect configuration API internally, that in turn may throw.
            try
            {
                if (e.Item.FQID.Kind == Kind.Camera)
                {
                    HandleCameraChanged(e.Item.FQID);
                }

                else
                {
                    HandleCameraChanged(null);
                }
                e.AcceptSelection = true;
            }
            catch (Exception)
            {
                HandleCameraChanged(null);
            }
        }

        private void HandleBound(bool isBound, FQID cameraId)
        {

            var cameraIdStr = cameraId != null ? cameraId.ObjectId.ToString() : "";
            var displayName = GetCameraDisplayName(cameraId);

            lblCamera.Text = $"{displayName} (Id: {cameraIdStr})";

            if (isBound)
            {
                btnAddToFS.Enabled = true;
            }
            else
            {
                btnAddToFS.Enabled = false;
            }
            currentCameraId = cameraId;

        }

        private void HandleCameraChanged(FQID cameraId)
        {
            if (cameraId == null)
            {
                DisableControls();
                return;
            }
            HandleBound(true, cameraId);
        }

        private string GetCameraDisplayName(FQID cameraId)
        {
            if (cameraId != null)
            {
                var camera = new VideoOS.Platform.ConfigurationItems.Camera(cameraId);
                return camera.DisplayName;
            }
            else
                return "";
        }

        private string GetCameraIP(FQID cameraId)
        {
            Item cameraItem = Configuration.Instance.GetItem(cameraId);

            string ip;

            if (cameraItem.Properties.TryGetValue("Address", out ip))
                return ip.Replace("http:", "").Replace("/", "");
            else
                return "";
        }

        private string MakeRTSPUrl(string ip)
        {
            return $"rtsp://{ip}:554/";
        }

     
        private void btnAddToFS_Click(object sender, EventArgs e)
        {

            var currentCameraIdStr = currentCameraId.ObjectId.ToString();
            var streamUrl = MakeRTSPUrl(GetCameraIP(currentCameraId));
            var streamName = GetCameraIP(currentCameraId)+"mlstn";

            var dlg = new AddToFaceStreamDialog(credentials, streamUrl, streamName);

            dlg.ShowDialog();
        }


        private WebRequest MakeWebRequest(string url)
        {
            var request = WebRequest.Create(url);
            credentials.Luna.AddBasicAuth(request);
            return request;
        }

        private void tbEndpoint_TextChanged(object sender, EventArgs e)
        {
            // this will clear irrelevant text in the ui, like token id
            //   HandleCameraChanged(currentCameraId);

            credentials.Luna.Url = tbEndpoint.Text;
        }
    }
}
