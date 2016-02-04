using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;


namespace TwitchBot.IRC
{
    class IRCManager
    {
        IRCSettings settings;
        TcpClient socket;
        Stream ircStream;
        StreamReader ircReader;
        StreamWriter ircWriter;
        Thread thread;

        public delegate void IncomingTextHandler(string text);
        public event IncomingTextHandler IncomingText;

        public IRCManager(IRCSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Joins to a channel using the IRCSettings given on initialisation
        /// </summary>
        /// <param name="channelName">The name of the twitch channel to connect to</param>
        /// <returns>True if connecting to the channel was successful, false if not</returns>
        public void JoinChannel(string channelName)
        {
            sendIRCMessage("JOIN #" + channelName);
        }

        public void Connect()
        {
            socket = new TcpClient(settings.IPAddress, settings.port);
            ircStream = socket.GetStream();
            ircReader = new StreamReader(ircStream);
            ircWriter = new StreamWriter(ircStream);

            
            ThreadStart CallToParseInput = new ThreadStart(ParseInput);
            thread = new Thread(CallToParseInput);
            thread.Start();
            


            sendIRCMessage("PASS " + settings.password);
            sendIRCMessage("NICK " + settings.userName);
        }

        public void Disconnect()
        {
            thread.Abort();
            socket.Close();
        }

        public void ParseInput()
        {
            string input;
            while (true)
            {
                input = ircReader.ReadLine();
                if (input == "PING")
                {
                    ircWriter.WriteLine("PONG");
                }
                else if (IncomingText != null)
                {
                    IncomingText(input);
                }
            }
        }

        void sendIRCMessage(string message)
        {
            Console.WriteLine(message);
            ircWriter.WriteLine(message);
            ircWriter.Flush();
        }

        public void sendMessage(string message)
        {

        }
    }
}
