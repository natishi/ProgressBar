# ProgressBar

## How to use progress bar app?
### 1.	Put the next 2 files under the main application folder(where you have the exe file)

       a.  ProgressBar.exe
       
       b. ProgressBar.exe.config

### 2.  In your application add the bellow code

        Please put this code where you want to activate progress bar:
        
        RunProgressBar($"{i}/88:({x},{y})");
      
        private void RunProgressBar(string status)
        {
            try
            {
                string progressApp = "ProgressBar";
                var processes = Process.GetProcesses().Where(pr => pr.ProcessName == progressApp).FirstOrDefault();
                if (processes == null)
                {
                    string dir = Directory.GetCurrentDirectory() + "\\";
                    string exeFile = Path.Combine(dir, progressApp + ".exe");
                    Process.Start(exeFile);
                }
                using (NamedPipeClientStream namedPipeClient = new NamedPipeClientStream(ConfigurationManager.AppSettings["NamedPipe"]))
                {
                    namedPipeClient.Connect();
                    byte[] messageBytes = Encoding.UTF8.GetBytes(status);
                    namedPipeClient.Write(messageBytes, 0, messageBytes.Length);
                }
            }
            catch(Exception e)
            {
                Trace.WriteLine($"Error in RunProgressBar.{e.Message}");
            }
        }
        
        e.g
        for (int i = 1; i <= 150; i++)
        {
            int x = 200 + i * 10;
            int y = 200 + i * 10;
            RunProgressBar($"{i}/150:({x},{y})");
            Thread.Sleep(50);
        }
