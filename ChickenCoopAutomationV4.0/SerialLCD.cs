using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using GHIElectronics.NETMF.FEZ;

namespace ChickenCoopAutomation
{
    /// <summary>
    /// Not currently used
    /// </summary>
    public class SerialLCD
    {
        const byte DISP_ON = 0xC;    //Turn visible LCD on
        const byte CLR_DISP = 0x01;      //Clear display
        const byte CUR_HOME = 2;      //Move cursor home and clear screen memory
        const byte SET_CURSOR = 0x80;   //SET_CURSOR + X : Sets cursor position to X
        const byte Move_CURSOR_LEFT = 0x10;

        private SerialPort UART = new SerialPort("COM1", 2400);

        public SerialLCD(FEZ_Pin.Digital pin)
        {
            UART.Open();
        }

        public void Print(string s)
        {
            //string comanda = "$PRINT FEZ is easy\n\r";
            byte[] buffer = Encoding.UTF8.GetBytes(s);
            UART.Write(buffer, 0, buffer.Length); 
        }

        public void ClearScreen()
        {
            SendCommand(CLR_DISP);
        }

        public void CursorHome()
        {
            SendCommand(CUR_HOME);
        }

        public void SetCursor(byte row, byte col)
        {
            SendCommand((byte)(SET_CURSOR | row << 6 | col));
        }

        public void MoveLeft()
        {
            SendCommand(Move_CURSOR_LEFT);
        }

        private void SendCommand(byte cmd)
        {
            SendByte(0xFE);
            SendByte(cmd);
        }

        private void SendByte(byte cmd)
        {
            byte[] buffer = new byte[1] { cmd };
            UART.Write(buffer, 0, buffer.Length); 
        }
    }   
}
