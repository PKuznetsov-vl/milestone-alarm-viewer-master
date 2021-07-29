using System.Windows.Forms;
using VideoOS.Platform.Admin;
using VLAlarmViewer.Common;

namespace VLAlarmViewer.Admin
{
    public class VLAlarmViewerItemManager : ItemManager
    {
        Credentials credentials = new Credentials();
        CameraBinderControl binder = null;

        public override void Init()
        {
        #if DEBUG
           // credentials.Luna.Url = "http://10.16.30.20:5000/";
          //  credentials.Luna.User = "i.samuolis@visionlabs.ru";
            //credentials.Luna.Pass = "polly890";
            credentials.FaceStream.Url = "http://10.16.6.41:60001/";
        #endif
            binder = new CameraBinderControl(credentials);
        }

        public override void Close()
        {
        }

        /// <summary>
        /// A user control to display when the administrator clicks on the treeNode.
        /// This can be a help page or a status over of the configuration
        /// </summary>
        public override ItemNodeUserControl GenerateOverviewUserControl()
        {
            return binder;
        }
    }
}