# awsForm
Sample Project send and receive message from the aws iot cloud. Beispiel-Projekt zum Senden und Empfangen von Nachrichten mit Hilfe der Amazon AWS IoT Cloud<br><br>
put your pfx-certificate in the folder awsForm/bin/Debug/certs<br><br>
<h3>Resources:</h3>
<ul>
<li>openssl pkcs12 -export -in certificate.pem.crt -inkey private.pem.key -out certificate.cert.pfx -certfile AmazonRootCA1.pem</li>
<li>{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Action": [
        "iot:Publish",
        "iot:Subscribe",
        "iot:Connect",
        "iot:Receive"
      ],
      "Effect": "Allow",
      "Resource": [
        "*"
      ]
    }
  ]
}</li>
<li>https://www.heise.de/download/product/win32-openssl-47316</li>
</ul>
