using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bingo
{
    public partial class Form1 : Form
    {
        int playerTurn, currentTurn;
        TcpClient tcpClient = default(TcpClient);
        NetworkStream networkStream;
        Button[] buttons;
        String result;
        int[] board = new int[25];
        int[] buttonValues;
        public Form1()
        {
            InitializeComponent();
        }
        public delegate void MyDelegate();
        private void buttonStart_Click(object sender, EventArgs e)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(textBoxIP.Text, Convert.ToInt16(textBoxPort.Text));
            networkStream = tcpClient.GetStream();
            GenerateBoard();
            playerTurn = getIntFromServer(networkStream);
            disableClick();
            Thread t = new Thread(new ThreadStart(playGame));
            t.Start();
        }
        
        
        private void playGame()
        {
            MyDelegate dDeclareResultWon = new MyDelegate(DeclareResultWon);
            MyDelegate dLabelPlayerTurn = new MyDelegate(DeclarePlayerTurn);
            MyDelegate dLabelCurrentTurn = new MyDelegate(DeclareCurrentTurn);
            MyDelegate dLabelTurn = new MyDelegate(DeclareMyTurn);
            MyDelegate dEnableClick = new MyDelegate(enableClick);
            MyDelegate dDisableClick = new MyDelegate(disableClick);

            this.Invoke(dLabelPlayerTurn);
            
            while (true) {
                if ((currentTurn = getIntFromServer(networkStream)) == playerTurn)
                {
                    this.Invoke(dLabelCurrentTurn);
                    this.Invoke(dLabelTurn);
                    this.Invoke(dEnableClick);
                }
                int clickedNumber = getIntFromServer(networkStream);
                this.Invoke(dDisableClick);
                board[clickedNumber] = 1;
                if (playerWon() == 1)
                    sendToServer('Y');
                else
                    sendToServer('N');
                result = readCommandFromServer(networkStream);
                if (result.Split(':')[0].CompareTo("WON") == 0)
                {
                    this.Invoke(dDeclareResultWon);
                    break;
                }
            }
        }

        private void DeclareMyTurn()
        {
            labelTurn.Text = "It's Your Turn.";
        }

        private void DeclareCurrentTurn()
        {
            labelCurrentTurn.Text = Convert.ToString(currentTurn);
        }

        private void DeclarePlayerTurn()
        {
            labelPlayerTurn.Text = Convert.ToString(playerTurn);
        }

        private void DeclareResultWon() {
            labelResult.Text = result;
        }

        private void enableClick()
        {
            for (int i = 0; i < 25; i++)
                if (board[buttonValues[i]] == 0)
                    buttons[i].Enabled = true;
        }

        private void disableClick()
        {
            for (int i = 0; i < 25; i++)
                    buttons[i].Enabled = false;
        }

        private void GenerateBoard()
        {
            ushort[] temp = new ushort[25];
            Random random = new Random();
            Button[] b = { button1, button2, button3, button4, button5,
                                 button6, button7, button8, button9, button10,
                                 button11, button12, button13, button14, button15,
                                 button16, button17, button18, button19, button20,
                                 button21, button22, button23, button24, button25
                                };
            buttons = b;
            buttonValues = new int[25];
            for (short i = 0; i < 5; i++)
                temp[i] = 0;
            for (short i = 0; i < 25; i++)
            {
                short a;
                while (temp[(a = (short)(random.Next() % 25))] != 0) ;
                temp[a] = 1;
                b[i].Text = Convert.ToString(a + 1);
                buttonValues[i] = a;
            }
            for (int i = 0; i < 25; i++)
                board[i] = 0;
        }

        private int playerWon()
        {
            int count=0;
            for (int i=0;i<25;i++) {
                if (i % 5 == 0)
                    count = 0;
                if (board[buttonValues[i]] == 1)
                    count++;
                if (count == 5)
                    return 1;
            }
            for (int i=0;i<5;i++) {
                for (int j=0;j<5;j++) {
                    if (j == 0)
                        count = 0;
                    if (board[buttonValues[j*5 + i]] == 1)
                        count++;
                    if (count == 5)
                        return 1;
                }
            }
            return 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[0]);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[1]);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[2]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[3]);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[4]);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[5]);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[6]);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[7]);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[8]);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[9]);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[10]);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[11]);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[12]);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[13]);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[14]);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[15]);
        }
        private void button17_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[16]);
        }
        private void button18_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[17]);
        }
        private void button19_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[18]);
        }
        private void button20_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[19]);
        }
        private void button21_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[20]);
        }
        private void button22_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[21]);
        }
        private void button23_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[22]);
        }
        private void button24_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[23]);
        }
        private void button25_Click(object sender, EventArgs e)
        {
            sendToServer(buttonValues[24]);
        }
        private void sendToServer(int v)
        {
            Byte[] bytes = new Byte[1];
            bytes[0] = Convert.ToByte(v);
            networkStream.Flush();
            networkStream.Write(bytes, 0, 1);
        }
        private string readCommandFromServer(NetworkStream networkStream)
        {
            Byte[] bytes = new Byte[1024];
            int c;
            StringBuilder sb = new StringBuilder();
            while ((c = networkStream.ReadByte()) != '\n') {
                sb.Append((char)c);
            }
            return sb.ToString();
        }
        private int getIntFromServer(NetworkStream networkStream)
        {
            String turn = readCommandFromServer(networkStream);
            turn = turn.Split(':')[1];
            return int.Parse(turn);
        }
    }
}
