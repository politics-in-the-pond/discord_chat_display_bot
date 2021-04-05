using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DISCORD_CHAT_DISP
{
    public partial class Form1 : Form
    {
        DiscordSocketClient client;
        CommandService commands;
        Task task1;
        Boolean formborder = true;
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            task1 = Task.Run(() => BotMain());
        }
        public async Task BotMain()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose
            });
            commands = new CommandService(new CommandServiceConfig()
            {
                LogLevel = LogSeverity.Verbose
            });

            client.Log += OnClientLogReceived;
            commands.Log += OnClientLogReceived;

            await client.LoginAsync(TokenType.Bot, "Your Token");
            await client.StartAsync();

            client.MessageReceived += OnClientMessage;
            await Task.Delay(-1);
        }
        private async Task OnClientMessage(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message == null) return;

            if (message.Author.IsBot)
                return;

            var context = new SocketCommandContext(client, message);

            String Context = message.Content;
            String name = message.Author.Username;

            if (Context == "!@#")
            {
                form_border();
            }
            else
            {
                textbox_update(name, Context);
            }
        }

        private void textbox_update(String name, String Context)
        {
            if (richTextBox1.InvokeRequired)
                richTextBox1.Invoke(new Action(() =>
                {
                    richTextBox1.SelectionFont = new System.Drawing.Font("Noto Sans CJK KR Bold", 16, System.Drawing.FontStyle.Italic);//, System.Drawing.GraphicsUnit.Pixel);
                    richTextBox1.SelectionColor = pick_color(name);
                    richTextBox1.AppendText(name + " : ");
                    richTextBox1.SelectionFont = new System.Drawing.Font("Noto Sans CJK KR Bold", 14, System.Drawing.FontStyle.Regular);//, System.Drawing.GraphicsUnit.Pixel);
                    richTextBox1.SelectionColor = System.Drawing.Color.White;
                    richTextBox1.AppendText(Context + "\n");
                }));
            else { }
        }

        private void form_border()
        {
            if (this.InvokeRequired)
                richTextBox1.Invoke(new Action(() =>
                {
                    if (formborder == true)
                    {
                        FormBorderStyle = FormBorderStyle.None;
                        BackColor = System.Drawing.Color.Black;
                        TransparencyKey = System.Drawing.Color.Black;
                        formborder = false;
                    }
                    else
                    {
                        FormBorderStyle = FormBorderStyle.Sizable;
                        BackColor = System.Drawing.Color.Black;
                        TransparencyKey = System.Drawing.Color.Transparent;
                        formborder = true;
                    }
                }));
            else
            {
                if (formborder == true)
                {
                    FormBorderStyle = FormBorderStyle.None;
                    BackColor = System.Drawing.Color.Black;
                    TransparencyKey = System.Drawing.Color.Black;
                    formborder = false;
                }
                else
                {
                    FormBorderStyle = FormBorderStyle.Sizable;
                    BackColor = System.Drawing.Color.Black;
                    TransparencyKey = System.Drawing.Color.Transparent;
                    formborder = true;
                }
            }

        }
        private Task OnClientLogReceived(LogMessage msg)
        {
            return Task.CompletedTask;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.InvokeRequired)
                richTextBox1.Invoke(new Action(() =>
                {
                    this.richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    this.richTextBox1.ScrollToCaret();
                }));
            else
            {
                this.richTextBox1.SelectionStart = richTextBox1.Text.Length;
                this.richTextBox1.ScrollToCaret();
            }

        }

        public System.Drawing.Color pick_color(String s)
        { // 랜덤색상 생성
            int seed = 0;
            for (int i = 0; i < s.Length; i++)
            {
                seed += (int)s[i];
            }
            int Hue = MT19937(seed); // 0~359
            double saturation = 0.7;
            double value = 1;

            double c = value * saturation;
            double x = c * (1 - Math.Abs(((Hue * 1.0 / 60) % 2) - 1));
            double m = value - c;

            double R = 0;
            double G = 0;
            double B = 0;

            if (0 <= Hue && Hue < 60)
            {
                R = c;
                G = x;
                B = 0;
            }
            if (60 <= Hue && Hue < 120)
            {
                R = x;
                G = c;
                B = 0;
            }
            if (120 <= Hue && Hue < 180)
            {
                R = 0;
                G = c;
                B = x;
            }
            if (180 <= Hue && Hue < 240)
            {
                R = 0;
                G = x;
                B = c;
            }
            if (240 <= Hue && Hue < 300)
            {
                R = x;
                G = 0;
                B = c;
            }
            if (300 <= Hue && Hue < 360)
            {
                R = c;
                G = 0;
                B = x;
            }

            R = (R + m) * 255;
            G = (G + m) * 255;
            B = (B + m) * 255;

            return System.Drawing.Color.FromArgb((int)R, (int)G, (int)B);
        }

        /*
	    * Copyright (C) 1997 - 2002, Makoto Matsumoto and Takuji Nishimura, All rights
	    * reserved. Redistribution and use in source and binary forms, with or without
	    * modification, are permitted provided that the following conditions are met:
	    * 1. Redistributions of source code must retain the above copyright notice,
	    * this list of conditions and the following disclaimer. 2. Redistributions in
	    * binary form must reproduce the above copyright notice, this list of
	    * conditions and the following disclaimer in the documentation and/or other
	    * materials provided with the distribution. 3. The names of its contributors
	    * may not be used to endorse or promote products derived from this software
	    * without specific prior written permission. THIS SOFTWARE IS PROVIDED BY THE
	    * COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
	    * WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
	    * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
	    * EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
	    * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
	    * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
	    * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
	    * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
	    * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
	    * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
	    */

        readonly int N = 624; // MT19937_32 initialization
        readonly int M = 397;
        readonly long MATRIX_A = 0x9908b0dfL;
        readonly long UMASK = 0x80000000L;
        readonly long LMASK = 0x7fffffffL;

        private int MT19937(int s)
        {

            long[] mt = new long[N];
            int mti = N + 1;
            mt[0] = s & 0xFFFFFFFF;
            for (mti = 1; mti < N; mti++)
            {
                mt[mti] = (0x6C078965 * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + mti);
            }

            long y;
            long[] mag01 = new long[2];
            mag01[0] = 0x0L;
            mag01[1] = MATRIX_A;

            if (mti >= N)
            {
                int kk;

                for (kk = 0; kk < N - M; kk++)
                {
                    y = (mt[kk] & UMASK) | (mt[kk + 1] & LMASK);
                    mt[kk] = mt[kk + M] ^ (y >> 1) ^ mag01[(int)(y & 0x1L)];
                }

                for (; kk < N - 1; kk++)
                {
                    y = (mt[kk] & UMASK) | (mt[kk + 1] & LMASK);
                    mt[kk] = mt[kk + (M - N)] ^ (y >> 1) ^ mag01[(int)(y & 0x1L)];
                }

                y = (mt[N - 1] & UMASK) | (mt[0] & LMASK);
                mt[N - 1] = mt[M - 1] ^ (y >> 1) ^ mag01[(int)(y & 0x1L)];

                mti = 0;
            }

            y = mt[mti++];

            y ^= (y >> 11);
            y ^= (y >> 7) & 0x9D2C6780L;
            y ^= (y >> 15) & 0xEFC60000L;
            y ^= (y >> 18);
            s = (int)(y % 360);
            return s;
        }
    }
}
