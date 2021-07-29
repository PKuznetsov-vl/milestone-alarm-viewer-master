using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using VideoOS.Platform;
using VideoOS.Platform.Data;
using VLAlarmViewer.Common;


namespace AlarmPreview.Client
{
    /// <summary>
    /// Interaction logic for AlarmPreviewWpfUserControl.xaml
    /// </summary>
    public partial class AlarmPreviewWpfUserControl : UserControl
    {
        /// <summary>
        /// Initialize UI form alarm or event.
        /// </summary>
        public AlarmPreviewWpfUserControl(object alarmOrBaseEvent)
        {
            InitializeComponent();

            try
            {
                var ad = LoadAlarmData(alarmOrBaseEvent);
                var vd = ad.vendorData;

                imageRef.Source = DownloadPortrait(Luna.MakePortraitURL(ad.vendorData.apiEndpoint, ad.vendorData.referenceId));
                imageRes.Source = DownloadPortrait(Luna.MakePortraitURL(ad.vendorData.apiEndpoint, ad.vendorData.candidateId));

                textPerson.Text = vd.candidateUserData;
                textScore.Text = Math.Round(vd.similarity * 100.0, 2).ToString() + "%";
                textList.Text = vd.listUserData;
                textAge.Text = vd.age;
                textEmotion.Text = vd.emotion;
                textGender.Text = vd.gender;
                textTime.Text = ad.timestamp.ToString() + " (Client local time)";
                textSource.Text = ad.source;
                textLoc.Text = ad.location;
                textDef.Text = ad.definition;
                textDesc.Text = ad.description;

                buttonViewRef.Click += (o, i) => { Util.OpenBrowser(LunaUI.MakeFaceURL(vd.uiEndpoint)); };
            }
            catch (Exception)
            {
                gridPortraits.Visibility = System.Windows.Visibility.Hidden;
                gridInfo.Visibility = System.Windows.Visibility.Hidden;

                textDesc.Text = "Unsupported event type";
            }
        }

        /// <summary>
        /// Download a portrait image from a given URL using TOKEN authorization method.
        /// Portrait URL should be constructed with MakePortraitURL() function.
        /// </summary>
        private ImageSource DownloadPortrait(string address)
        {
            EnvironmentManager.Instance.Log(false, "AlarmPreview", $"Fetching portrait image from {address}");

            ImageSource imageSource = null;

            try
            {
                var request = WebRequest.Create(address);
                Luna.AddTokenAuth(request);

                var response = request.GetResponse();
                var bitmapImage = new BitmapImage();

                using (var responseStream = new MemoryStream())
                {
                    response.GetResponseStream().CopyTo(responseStream);
                    var portraitBytes = responseStream.ToArray();

                    using (var ms = new System.IO.MemoryStream(portraitBytes))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = ms;
                        bitmapImage.EndInit();
                        bitmapImage.Freeze();
                    }
                }

                imageSource = (ImageSource)bitmapImage;
            }
            catch (Exception exc)
            {
                EnvironmentManager.Instance.Log(true, "AlarmPreview", $"Error while fetching portrait image: {exc}");
            }

            return imageSource;
        }

        /// <summary>
        /// Analytics Event Vendor Data.
        /// </summary>
        struct VendorData
        {
            public const string VendorName = "VisionLabsBV";

            public string referenceId;
            public string candidateId;
            public string age;
            public string gender;
            public string candidateUserData;
            public string listId;
            public string listUserData;
            public string emotion;
            //public string tokenId;
            //public string tokenUserData;
            public double similarity;
            public string apiEndpoint;
            public string uiEndpoint;
        };

        /// <summary>
        /// Information on viewed alarm and parent event.
        /// </summary>
        struct AlarmData
        {
            public string source;
            public string location;
            public string definition;
            public string description;
            public DateTime timestamp;
            public VendorData vendorData;
        };

        /// <summary>
        /// Parse vendor data.
        /// Throws ArgumentException if can not parse vendor data.
        /// </summary>
        private static VendorData LoadVendorData(VideoOS.Platform.Data.Vendor vendorData)
        {
            if (vendorData.Name == VendorData.VendorName)
            {
                EnvironmentManager.Instance.Log(false, "AlarmPreview", $"{VendorData.VendorName} custom data: {vendorData.CustomData}");
                VendorData ret;

                try
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(vendorData.CustomData);

                    var nodeData = doc.DocumentElement.SelectSingleNode("/Data");
                    var dataVersion = nodeData.Attributes["Version"]?.InnerText;

                    if (dataVersion != "1")
                        throw new ArgumentException($"Unsupported Event Data version {dataVersion}. Outdated viewer?");

                    var nodeReference = doc.DocumentElement.SelectSingleNode("/Data/Reference");
                    var nodeCandidate = doc.DocumentElement.SelectSingleNode("/Data/Candidate");
                    var nodeList = doc.DocumentElement.SelectSingleNode("/Data/List");
                    // var nodeAuth = doc.DocumentElement.SelectSingleNode("/Data/Auth");

                    ret.referenceId = nodeReference.Attributes["SampleId"]?.InnerText;

                    ret.candidateId = nodeCandidate.Attributes["DescId"]?.InnerText;
                    ret.age = nodeCandidate.Attributes["Age"]?.InnerText;
                    ret.emotion = nodeCandidate.Attributes["Emotions"]?.InnerText;
                    ret.gender = nodeCandidate.Attributes["Gender"]?.InnerText;
                    ret.candidateUserData = nodeCandidate.Attributes["UserData"]?.InnerText;

                    ret.similarity = double.Parse(nodeCandidate.Attributes["Similarity"]?.InnerText, CultureInfo.InvariantCulture);

                    ret.listId = nodeList.Attributes["ListId"]?.InnerText;
                    ret.listUserData = nodeList.Attributes["UserData"]?.InnerText;

                    var nodeAPIEndpoint = doc.DocumentElement.SelectSingleNode("/Data/Endpoints/API");
                    var nodeUIEndpoint = doc.DocumentElement.SelectSingleNode("/Data/Endpoints/UI");

                    ret.apiEndpoint = nodeAPIEndpoint.InnerText;
                    ret.uiEndpoint = nodeUIEndpoint.InnerText;
                }
                catch (Exception exc)
                {
                    EnvironmentManager.Instance.Log(true, "AlarmPreview", $"Error while parsing vendor data XML: {exc}");
                    throw exc;
                }

                return ret;
            }
            else
                throw new ArgumentException("Unsupported Vendor.");
        }

        /// <summary>
        /// Convert UTC timestamp to local time.
        /// </summary>
        private static DateTime FixupDateTime(DateTime utc)
        {
            return DateTime.SpecifyKind(utc, DateTimeKind.Utc).ToLocalTime();
        }

        /// <summary>
        /// Load vendor data from alarm or event.
        /// Throws ArgumentException if can not parse vendor data or alarm type is unsupported.
        /// </summary>
        private static AlarmData LoadAlarmData(object alarmOrBaseEvent)
        {
            AlarmData ad;

            Alarm alarm = alarmOrBaseEvent as Alarm;
            if (alarm != null)
            {
                ad.vendorData = LoadVendorData(alarm.Vendor);
                /*string filename = Luna.filename;
                Luna.camerasFqidList = File.ReadAllLines(filename).ToList();
                string fullValue = Luna.camerasFqidList.First(x => x.Contains(alarm.EventHeader.Source.Name));*/

                ad.source = alarm.EventHeader.Source.Name;
                ad.timestamp = alarm.EventHeader.Timestamp;
                ad.definition = alarm.RuleList != null && alarm.RuleList.Count > 0 ? alarm.RuleList[0].Name : "";
                ad.location = alarm.Location ?? "";
                ad.description = alarm.Description ?? "";
            }
            else
            {
                AnalyticsEvent analyticsEvent = alarmOrBaseEvent as AnalyticsEvent;
                if (analyticsEvent != null)
                {
                    ad.vendorData = LoadVendorData(analyticsEvent.Vendor);

                    ad.source = analyticsEvent.EventHeader.Source.Name;
                    ad.timestamp = analyticsEvent.EventHeader.Timestamp;
                    ad.definition = "";
                    ad.location = analyticsEvent.Location ?? "";
                    ad.description = analyticsEvent.Description ?? "";
                }
                else
                    throw new ArgumentException("Not an Alarm or AnalyticsEvent.");
            }

            ad.timestamp = FixupDateTime(ad.timestamp);

            return ad;
        }
    }
}
