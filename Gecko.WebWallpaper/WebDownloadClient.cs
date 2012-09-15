using System;
using System.Net;


namespace Gecko.WebWallpaper
{
     public class WebDownloadClient: WebClient
        {
            private int _timeout;
            /// <summary>
            /// Time in milliseconds
            /// </summary>
            public int Timeout
            {
                get
                {
                    return _timeout;
                }
                set
                {
                    _timeout = value;
                }
            }

            public WebDownloadClient()
            {
                this._timeout = 60000;
            }

            public WebDownloadClient(int timeout)
            {
                this._timeout = timeout;
            }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var result = base.GetWebRequest(address);
                result.Timeout = this._timeout;
                return result;
            }
        }

}

