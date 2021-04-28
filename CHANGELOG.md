# Versions

## v1.0.4
- UDP port scanning was fixed.(in the previous version the app shows that all UDP ports have Opened state)
- Scanning results cache was added. You can see your history in the "Scaning history" and LAN list shows how long you scanned any ports in the LAN. Cache stored in the .data\.cache file.
- LAN list shows IPs that belong to device, that launched Looto app. It shows (me) status near the host.

## v1.0.3
- Some bug fixes.
- LAN hosts scanner was improved - it scan more hosts based on local IP device.
- Result window was restyled.
- On result window filters appeared for on/off render ports with selected port states.

## v1.0.2
- Error handling. Now app can write logs and made bug report files in .data\_logs folder. If exception was catched app now shows window with error info.(in the previous version the app only crashed without warnings or consequenses)
- Not scanning hosts ports if host doesn't exists.(in the previous version the app shows all ports of not existed host as closed)
- Added host scanner. Now you can see the list of devices IP addresses, that are on the same local network.

## v1.0.1
- Now in host input user can type Domain name(in the previous version user can type only IP address)
- Stop button added. If you realized that you don't need to scan everything, but realized it too late, then you can complete the process. But the check of those ports that have started to be checked will not stop and you will have to wait for some time.
- Scanning has been significantly accelerated. For scanning each port uses all available processors cores. In my case with Intel i5-2300 (4 cores) the speed increased by 4 times. This means that speed of scanning depends on cores count of your processor.

## v1.0.0
- First release version.
- Very simple port scanning with inputs for separated ports or range of ports.
- Simplified results showing.