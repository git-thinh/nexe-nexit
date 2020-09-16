
TASKKILL /F /IM "node*"
TASKKILL /F /IM "nexe.exe*"

./redis-server.exe --service-install --service-name redis_5000 --port 5000 --bind 10.1.1.174
sc delete redis_5000

C:\ntest\nexe.exe "%systemdrive%\Program Files\nodejs\node.exe" "C:\ntest\app.js" --service-install --service-name node-6369

sc.exe create node-6369 binPath= "C:\ntest\nexe.exe"

sc delete node-6369