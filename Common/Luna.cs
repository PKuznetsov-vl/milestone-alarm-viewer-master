using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VLAlarmViewer.Common
{
    public class Service
    {
        protected static string FixupEndpoint(string endpoint)
        {
            var baseUrl = endpoint;

            if (!baseUrl.EndsWith("/"))
                baseUrl += "/";

            return baseUrl;
        }
    }

    /// <summary>
    /// LUNA constants.
    /// </summary>
    public class Luna : Service
    {
   
        public const int API = 6;
        public const int OldAPI = 5; 
        public const int MaxPageSize = 100;

        public class Credentials
        {
            public string Url { get; set; }
            public void AddBasicAuth(WebRequest request)
            {
                request.Headers.Add("Luna-Account-Id", "6d071cca-fda5-4a03-84d5-5bea65904480");
            }
        }

        public static string AppendPagination(string baseUrl, int page, int resultsPerPage = MaxPageSize)
        {
            if (resultsPerPage < 1 || resultsPerPage > MaxPageSize)
                throw new ArgumentOutOfRangeException($"Parameter resultsPerPage ({resultsPerPage}) is out of range [1; {MaxPageSize}]");

            if (page < 1)
                throw new ArgumentOutOfRangeException($"Parameter page ({page}) is out of range [1; 32768]");

            return baseUrl + "?page=" + page.ToString() + "&page_size=" + resultsPerPage.ToString();
        }

        public static void AddTokenAuth(WebRequest request)
        {
            request.Headers.Add("Luna-Account-Id", "6d071cca-fda5-4a03-84d5-5bea65904480");
        }

        public static string MakeHandlersURL(string endpoint)
        {
            // Due to LUNA actually being crap, trailing slash here will break all requests with query parameters...
            return MakeBaseURL(endpoint) + "/handlers";
        }
        public static string MakeHandlersCountURL(string endpoint)
        {
            // Due to LUNA actually being crap, trailing slash here will break all requests with query parameters...
            return MakeBaseURL(endpoint) + "/handlers/count";
        }
        //10.16.30.20 luna 5
        //10.16.30.19 luna 3
        public static string MakeTokensURL(string endpoint)
        {
            // Due to LUNA actually being crap, trailing slash here will break all requests with query parameters...
            return MakeBaseURL(endpoint) + "/account/tokens";
        }

        public static string MakePortraitURL(string endpoint, string faceId)
        {
            return MakeBaseURL(endpoint) + "/samples/" + faceId;
        }

        public static string MakeSearchURL(string endpoint, string listId)
        {
            return MakeBaseURL(endpoint) + "/handlers/" + listId + "/events";
        }

        private static string MakeBaseURL(string endpoint)
        {
            return FixupEndpoint(endpoint) + API.ToString();
        }
        private static string MakeFsBaseURL(string endpoint)
        {
            endpoint = endpoint.Replace("5000", "5130");
            return FixupEndpoint(endpoint) + OldAPI.ToString();
        }
    }

    /// <summary>
    /// FaceStream constants.
    /// </summary>
    public class FaceStream : Service
    {
        public const int API = 1;

        public class Credentials
        {
            public string Url { get; set; }
        }

        public static string MakeStreamPreviewURL(string endpoint, string streamId)
        {
            return MakeBaseURL(endpoint) + "/streams/preview/" + streamId;
        }

        public static string MakeStreamsURL(string endpoint)
        {
            return MakeBaseURL(endpoint) + "/streams";
        }

        private static string MakeBaseURL(string endpoint)
        {
            return FixupEndpoint(endpoint) + "api/" + API.ToString();
        }
    }

    public class Credentials
    {
        private Luna.Credentials _luna = new Luna.Credentials();

        public Luna.Credentials Luna
        {
            get { return _luna; }
            set { _luna = value; }
        }

        private FaceStream.Credentials _fs = new FaceStream.Credentials();

        public FaceStream.Credentials FaceStream
        {
            get { return _fs; }
            set { _fs = value; }
        }
    }

    /// <summary>
    /// LUNA UI constants.
    /// </summary>
    public class LunaUI : Service
    {
        /// <summary>
        /// Make a person UI page URL from enpoint and id.
        /// </summary>
        public static string MakePersonURL(string endpoint, string id)
        {
            return FixupEndpoint(endpoint) + "/admin/list-and-faces";
        }

        /// <summary>
        /// Make a face UI page URL from enpoint and id.
        /// </summary>
        public static string MakeFaceURL(string endpoint)
        {
            return FixupEndpoint(endpoint) + "/admin/events";
        }

        /// <summary>
        /// Make a list UI page URL from enpoint and id.
        /// </summary>
        public static string MakeListURL(string endpoint, string type, string id)
        {
            // `type` is either "person" or "face", UI expects "/persons" or "/faces"
            return FixupEndpoint(endpoint) + "admin/list-and-faces/" + id;
        }
    }

    public class Util
    {
        public static void DisplayError(string text)
        {
            MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static JArray ParseResponseJsonArray(HttpWebResponse response)
        {
            return (JArray)ParseResponseJsonGeneric(response);
        }
        public static JObject ParseResponseJson(HttpWebResponse response)
        {
            return (JObject)ParseResponseJsonGeneric(response);
        }

        public static JToken ParseResponseJsonGeneric(HttpWebResponse response)
        {
            var encoding = HandleResponseEncoding(response);
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream, encoding))
            {
                return JToken.ReadFrom(new JsonTextReader(reader));
            }
        }

        public static bool TryHandleError(System.Net.WebException exc, string message)
        {
            if (exc.Status == WebExceptionStatus.ProtocolError)
            {
                var response = exc.Response as HttpWebResponse;
                if (response != null)
                {
                    var o = ParseResponseJson(response);
                    var code = (int)o["error_code"];
                    var detail = (string)o["detail"];

                    DisplayError($"{message} due to error ({code}): {detail}");
                }
            }
            else
            {
                DisplayError($"{message} due to network error: {exc.Message}");
            }

            return true;
        }

        private static Encoding HandleResponseEncoding(HttpWebResponse response)
        {
            Encoding encoding = null;

            try
            {
                encoding = Encoding.GetEncoding(response.CharacterSet);
            }
            catch (Exception)
            {
                encoding = Encoding.UTF8;
            }

            return encoding;
        }

        /// <summary>
        /// Safely (exception-wise) start a browser process with a given URL.
        /// </summary>
        public static void OpenBrowser(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception)
            {
            }
        }
    }
}
