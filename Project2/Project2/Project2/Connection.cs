using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace Project2
{
    public class Connection
    {
        public Connection()
        {
            
        }

        public string ConnectZip(TextBox txtBoxImage, TextBox txtBoxThreshold)
        {
            string imageResouce = txtBoxImage.Text;// Lầy file zip trong máy tính
            string Threshold = txtBoxThreshold.Text;// Lấy độ chính xác
            string url = "http://gateway-a.watsonplatform.net/visual-recognition/api/v3/classify/?api_key=72edd2ec0a46970e6ff1cc1cb42b17f6c3333716&version=2016-05-20&owners=me&threshold=" + Threshold;
            WebClient myWebClient = new WebClient();
            byte[] responseArray = myWebClient.UploadFile(url, imageResouce);// Upload file cần nhận biết lên server
            return System.Text.Encoding.ASCII.GetString(responseArray);// Lấy phản hồi và chuyển qua dạng string.

        }

        public string Connect(TextBox txtBoxImage, TextBox txtBoxThreshold)
        {
            string imageResouce =txtBoxImage.Text;
            string Threshold = txtBoxThreshold.Text;
            string url= "http://gateway-a.watsonplatform.net/visual-recognition/api/v3/classify/?api_key=72edd2ec0a46970e6ff1cc1cb42b17f6c3333716&version=2016-05-20&owners=me&threshold=";
            WebRequest request = WebRequest.Create(url+Threshold);// Tạo phương thức yêu cầu web tới địa chỉ url xác định
            // Tạo phương thức post và đóng gói thành dạng byte 
            request.Method = "POST";
            byte[] byteArray = File.ReadAllBytes(imageResouce);
            request.ContentType = "application/x-www-form-urlencoded/zip";
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseMessage = reader.ReadToEnd();
            return responseMessage;
            
        }


    }
}
