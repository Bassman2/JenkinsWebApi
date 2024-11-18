using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {
        private const int udpPort = 33848;

        /// <summary>
        /// Get a list with all Jenkins servers in the local subnet.
        /// </summary>
        /// <param name="timeout">Timeout of the search.</param>
        /// <returns>List with available Jenkins servers.</returns>
        /// <remarks>
        /// Since 2.220 the feature has been completely removed.<br/>
        /// Since 2.219 und LTS 2.204.2 this feature is deactivated by default, <br/>
        /// but can be activated by setting the system property hudson.DNSMultiCast.disabled to false or hudson.udp to 33848.<br/>
        /// <see href="https://www.jenkins.io/security/advisory/2020-01-29/"/><br/>
        /// <see href="https://www.jenkins.io/doc/book/managing/system-properties/"/> 
        /// </remarks>
        [Obsolete("Feature removed in newer Jenkins versions!")]
        public static async Task<IEnumerable<JenkinsInstance>> GetJenkinsInstancesAsync(long timeout = 2000)
        {
            List<JenkinsInstance> list = null;
            using (UdpClient client = new UdpClient())
            {
                await client.SendAsync(new byte[0], 0, new IPEndPoint(IPAddress.Broadcast, udpPort));
                int start = Environment.TickCount;
                while (true)
                {
                    while (client.Available > 0)
                    {
                        UdpReceiveResult res = await client.ReceiveAsync();
                        string result = Encoding.ASCII.GetString(res.Buffer);
                        Trace.TraceInformation(result);
                        using (XmlTextReader reader = new XmlTextReader(new StringReader(result)))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(JenkinsInstance));
                            if (serializer.CanDeserialize(reader))
                            {
                                JenkinsInstance inst = (JenkinsInstance)serializer.Deserialize(reader);
                                inst.Address = res.RemoteEndPoint.Address;
                                (list ?? (list = new List<JenkinsInstance>())).Add(inst);
                            }
                            else
                            {
                                Trace.TraceError($"Unknown broadcast response: {result}");
                            }
                        }
                    }
                    if (Environment.TickCount > start + timeout)
                    {
                        break;
                    }
                    await Task.Delay(100);
                }
            }
            return list;
        }
    }
}
