using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace awsForm
{

    public partial class Form1 : Form, Iform
    {
        public delegate void setTextAWS(string message);
        public setTextAWS mySetTextAWS;

        public aws awsStream = null;

        public Form1()
        {
            Debug.WriteLine("HelloWorld");
            InitializeComponent();
            awsStream = new aws(this);
            mySetTextAWS = new setTextAWS(setText);
        }


        public void setText(string message)
        {
            textBox2.Text = message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            awsStream.sendMessage(@"{'message': 'publisher.form.message.defaultValue'}");

        }

        public string awsSubscripe
        {
            set => textBox1.Text = value;
        }

    }
}
