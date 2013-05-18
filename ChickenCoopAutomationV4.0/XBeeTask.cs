using System;
using Microsoft.SPOT;
using System.IO.Ports;
using System.Threading;
using System.Text;
namespace ChickenCoopAutomation
{
    public class XBeeTask : Task
    {
        private static SerialPort port;
        private static SerialBuffer buffer;

        protected override void DoWork()
        {
            buffer = new SerialBuffer(100);

            port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            port.Open();
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            while (true) 
            { 
                // Write some statistics to the port every now and then
                //port.Write()
                Write(DateTime.Now + ": No status currently...");    
                Thread.Sleep(5000);
            }
        }

        static void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Take what's on the port and load into the buffer. We might not have all the data yet. 
            buffer.LoadSerial(port);

            // Try to read a line. We may get null, which means that we haven't received all the data yet
            // If we haven't seen all the data yet, the DataReceived event will get called multiple times
            // When we see the newline '\n' character, a valid string is returned on StringBuffer.ReadLine().

            string s = buffer.ReadLine();

            // Full command received, blink the LED and clear the 
            // buffer for next time
            if (s != null)
            {
                switch(s)
                {
                    case "Hi":
                        Write("Hi there");
                        break;
                    default:
                        Write(s);       // echo back
                        break;
                }
                // process the command, if needed, and echo back
                if (s == "Hi")
                {
                    Write("Hi there");
                }
                else
                {
                    Write(s);
                }

                buffer = new SerialBuffer(100);
            }

            Debug.Print("Data Received: " + s);
        }

        private static void Write(string s)
        {
            s += "\n";      // add the newline character so that the other side knows when we are complete with a message
            byte[] buffer = Encoding.UTF8.GetBytes(s);
            port.Write(buffer, 0, buffer.Length);
        }

    }
}
