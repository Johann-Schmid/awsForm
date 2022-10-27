using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Windows;
using System.Threading.Tasks;

namespace awsForm
{

    public class aws
    {
        MqttClient client = null;
        Form1 awsForm;
        public delegate void ShowData(string message);


        public aws(Form1 form)
        {
            awsForm = form;
            var broker = "avdz0tx0oxt1t-ats.iot.eu-central-1.amazonaws.com"; //<AWS-IoT-Endpoint>           
            var port = 8883; //port of AWS cloud
            var certPass = "<type here your pfx-certificate password";
            //var clientId = "awsLabjack";

            //certificates Path

            var certificatesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "certs");

            var caCertPath = Path.Combine(certificatesPath, "AmazonRootCA1.pem");

            var caCert = X509Certificate.CreateFromCertFile(caCertPath);

            var deviceCertPath = Path.Combine(certificatesPath, "certificateAWS.cert.pfx");
            var deviceCert = new X509Certificate2(deviceCertPath, certPass);

            //var clientCert = new X509Certificate2(@"D:\awsForm\awsForm\bin\Debug\certs\certificate.cert.pfx", certPass);
            //var caCert = X509Certificate.CreateFromSignedFile(@"D:\awsForm\awsForm\bin\Debug\certs\AmazonRootCA1.pem");

            // Create a new MQTT client.
            try
            {
                client = new MqttClient(broker, port, true, caCert, deviceCert, MqttSslProtocols.TLSv1_2);
                client.Connect(Guid.NewGuid().ToString());
                client.Publish("labjackValues", Encoding.UTF8.GetBytes("Hello"));

                client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                client.MqttMsgSubscribed += Client_MqttMsgSubscribed;

                //subscribig to topic
                string topic = "labjackValuesReturn";
                client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                writeInterface("Connection established");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            

        }

        private void writeInterface(string message)
        {
            awsForm.awsSubscripe = message;
        }

        private void dataReceived(string message)
        {
            Debug.WriteLine(message);
            //awsForm.awsSubscripe = message;
            awsForm.awsSubscripe = message;
        }

        public void connect(string clientID)
        {
            try
            {
                client.Connect(clientID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Connect MQTT");
            }
            client.Connect(clientID);
        }

        public void sendMessage(string strVale)
        {
            Task t3 = Task.Run(() =>
            {
                try
                {
                    client.Publish("labjackValues", Encoding.UTF8.GetBytes(strVale));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Send MQTT message");
                }
                
            });
            t3.Wait();
        }

        private static void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Debug.WriteLine($"Successfully subscribed to the AWS IoT topic.");
        }
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Debug.WriteLine("Message received: " + Encoding.UTF8.GetString(e.Message));
            awsForm.Invoke(awsForm.mySetTextAWS, new object[] { Encoding.UTF8.GetString(e.Message) });
            //ShowData handler = new ShowData(dataReceived);
            //handler(Encoding.UTF8.GetString(e.Message));
        }

        public void closeConnection()
        {
            try
            {
                client.Disconnect();
                Debug.WriteLine("Connection closed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Close MQTT connection");
            }
        }
    }
}


