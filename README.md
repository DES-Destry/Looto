<img src="./Looto/Images/logo_main.png" style="display: block;margin-left: auto;margin-right: auto;">

<h1 style="text-align:center"><span style="color:#00C953">Looto</span> - port scanner</h1>

Looto - multi-thread port scanner with powerful user-friendly GUI. Port scanning can be the first step in a hacking or hacking prevention process, helping to identify potential targets for an attack. Scan your network to be sure you are safe. This software can use ONLY for educational purposes or for protection purposes! This software scanning needed ports via sockets, that sends pakcages to this ports. The adventage of this software is that it has a GUI and that can signficantly speed up the work. Speed of scanning depends on cores count of your processor.

<br/>
<br/>

## ⚡ Interface

### 💻 Main window
Main window contains all inputs for settings scanning. Theese are inputs such as host for scanning and various settings to select required ports. You can choose your ports separately or choose the range of scanning ports. You can enter ports separately for each protocol or for all protocols at once. From main window you have access to "Scanning history" and "Settings" windows.

While the scan is in progress, you can see the progress bar.
<img src="./README_RESOURCES/IM_1.png" style="display: block;margin-left: auto;margin-right: auto;">
<img src="./README_RESOURCES/IM_2.png" style="display: block;margin-left: auto;margin-right: auto;">
<img src="./README_RESOURCES/IM_3.png" style="display: block;margin-left: auto;margin-right: auto;">

---
<br/>

### ✔️ Scan result
After scanning you will see the scan result, turn on or turn off several port states to show. In "Settings" window you can configure sorting and render modes. 1 screenshot: Full render. 2 screenshot: Render as text.
<img src="./README_RESOURCES/IM_4.png" style="display: block;margin-left: auto;margin-right: auto;">
<img src="./README_RESOURCES/IM_5.png" style="display: block;margin-left: auto;margin-right: auto;">
<img src="./README_RESOURCES/IM_6.png" style="display: block;margin-left: auto;margin-right: auto;">

---
<br/>


### 🔍 LAN List
Scan your local network and get all devices IP in LAN. For convenience you can copy IP address from list to scan window with "Apply" button. Also it shows how long you scan any hosts.
<img src="./README_RESOURCES/IM_7.png" style="display: block;margin-left: auto;margin-right: auto;">

---
<br/>

### 🔧 Settings
Configure several aspects of working Looto. You can configure results window as you want. Configure processor working, and more...
<img src="./README_RESOURCES/IM_8.png" style="display: block;margin-left: auto;margin-right: auto;">
<img src="./README_RESOURCES/IM_9.png" style="display: block;margin-left: auto;margin-right: auto;">

---
<br/>

### 📜 Scanning history
You can see all your results of scanning that makes at last 3 days by default. In the "Settings" window you can change cache chuncks lifetime.
<img src="./README_RESOURCES/IM_10.png" style="display: block;margin-left: auto;margin-right: auto;">

---
<br/>

### ❌ Error handling
If error was occured in theory app will not crash. It will show window about crash, then will write logs entries with EXCP type with creating bug report. You can write new issue as bug report [here](https://github.com/DES-Destry/Looto/issues/new?assignees=DES-Destry&labels=bug&template=bug_report.md&title=Looto+have+a+bug%21). For more informative of the bug you can send ".data" folder content to agafonovandrej69@gmail.com. I'll not check your ".data" folder without created issue!
<img src="./README_RESOURCES/IM_11.png" style="display: block;margin-left: auto;margin-right: auto;">

---
<br/>
<br/>

## 📁 Install
Lastest stable version of application will be available in [Releases](https://github.com/DES-Destry/Looto/releases) of [GitHub repository](https://github.com/DES-Destry/Looto). For stable working of application you need only one executable file "Looto.exe", all "*.dll" files and ".data" folder. Other files not necessary for application working, but still important.
<img src="./README_RESOURCES/IM_12.png" style="display: block;margin-left: auto;margin-right: auto;">

<br/>
<br/>

## 🔜 Coming soon...
- At the moment this soft a little unfinished, but all mistakes will be fixed in the next versions of software.
- Optimize memory using in port results render. If ports count is 65534 in result, then app uses ~1.2GB of RAM.
- Allow the user to save the results to files of vaious types.
- More informative LAN scanner.
- Multiple hosts choose in the port scanning.
- Make application update system to ensure that the user always only has the latest version of app.
- Check UDP ports with ICMP protocol.